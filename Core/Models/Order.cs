using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Order
    {
        public Order()
        {
        }

        public Order(int orderNo, DateTime orderDateTime)
        {
            OrderNo = orderNo;
            OrderDateTime = orderDateTime;
        }

        public int OrderNo { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public List<Ticket> TicketList { get; set; }

        public void AddTicket(Ticket ticket)
        {
            TicketList.Add(ticket);
        }

        public List<Ticket> GetTicketList()
        {
            return TicketList;
        }
    }
}