using System;
using System.Collections.Generic;
using Core.Models;
using Core.Repository;

namespace Core.UseCases
{
    public class Order
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IOrder _orderRepository;
        private readonly IScreening _screeningRepository;

        public Order(IDateTimeProvider dateTimeProvider, IOrder orderRepository, IScreening screeningRepository) =>
            (_dateTimeProvider, _orderRepository, _screeningRepository) =
            (dateTimeProvider, orderRepository, screeningRepository);

        public Core.Models.Order FindByNo(int no) => _orderRepository.FindByNo(no);
        public List<Core.Models.Order> FindAll() => _orderRepository.FindAll();

        public Core.Models.Order Add(List<Ticket> tickets)
        {
            if (tickets.Count == 0) throw new Exception("List of tickets cannot be empty");

            var order = new Core.Models.Order(_orderRepository.FindAll().Count + 1, _dateTimeProvider.Now())
            {
                TicketList = tickets,
                Status = "Unpaid",
            };

            var screeningNo = -1;
            foreach (var ticket in tickets)
            {
                if (ticket.Screening is null) continue;

                screeningNo = ticket.Screening.ScreeningNo;
                break;
            }

            if (screeningNo == -1) throw new Exception("No screening found for tickets, something is seriously wrong");

            var screening = _screeningRepository.FindByNo(screeningNo);
            _screeningRepository.UpdateSeatsRemaining(screeningNo, screening.SeatsRemaining - tickets.Count);

            _orderRepository.Add(order);

            return order;
        }

        public Core.Models.Order Pay(int orderNo)
        {
            var order = _orderRepository.FindByNo(orderNo);
            if (order is null) throw new Exception("Order does not exist");

            var payable = 0.0;
            foreach (var ticket in order.TicketList) payable += ticket.CalculatePrice();

            _orderRepository.UpdateAmount(orderNo, payable);
            _orderRepository.UpdateStatus(orderNo, "Paid");

            return order;
        }

        public void Cancel(int orderNo)
        {
            var order = _orderRepository.FindByNo(orderNo);
            if (order is null) throw new Exception("Order does not exist");
            if (order.TicketList.Count < 1) throw new Exception("No tickets in order");
            if (order.TicketList[0].Screening is null) throw new Exception("Screening object in ticket is null");

            if (_dateTimeProvider.Now() > order.TicketList[0].Screening.ScreeningDateTime)
                throw new Exception("Screening has passed. You may not cancel your order.");

            _screeningRepository.UpdateSeatsRemaining(order.TicketList[0].Screening.ScreeningNo,
                order.TicketList[0].Screening.SeatsRemaining + order.TicketList.Count);
            _orderRepository.UpdateStatus(orderNo, "Cancelled");
        }
    }
}