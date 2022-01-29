using System;
using System.Collections.Generic;
using System.Data;
using FluentAssertions;
using Xunit;
using Moq;
using Core.Models;
using Core.Repository;

namespace Core.Tests.UseCases
{
    public class Order
    {
        public DateTime Now = DateTime.Now;
        public Mock<IOrder> OrderRepositoryMock { get; set; }
        public Mock<IScreening> ScreeningRepositoryMock { get; set; }
        public Mock<IDateTimeProvider> DateTimeProviderMock { get; set; }

        public Order()
        {
            OrderRepositoryMock = new Mock<IOrder>();
            ScreeningRepositoryMock = new Mock<IScreening>();
            DateTimeProviderMock = new Mock<IDateTimeProvider>();
            DateTimeProviderMock.Setup(d => d.Now()).Returns(Now);
        }

        [Fact]
        public void Constructor_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() =>
            {
                _ = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                    ScreeningRepositoryMock.Object);
            }));
        }

        [Fact]
        public void FindByNo_CallsRepositoryFindByNo()
        {
            var order = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                ScreeningRepositoryMock.Object);
            order.FindByNo(234);

            OrderRepositoryMock.Verify(o => o.FindByNo(234), Times.Once());
        }

        [Fact]
        public void FindAll_CallsRepositoryFindAll()
        {
            var order = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                ScreeningRepositoryMock.Object);
            order.FindAll();

            OrderRepositoryMock.Verify(o => o.FindAll(), Times.Once());
        }

        [Fact]
        public void Add_EmptyTicketList_Throws()
        {
            var order = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                ScreeningRepositoryMock.Object);
            Assert.NotNull(Record.Exception(() => { _ = order.Add(new List<Ticket>()); }));
        }

        [Fact]
        public void Add_NullScreeningInTicketList_Throws()
        {
            var order = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                ScreeningRepositoryMock.Object);
            OrderRepositoryMock.Setup(o => o.FindAll())
                .Returns(Fixtures.Orders);

            Assert.NotNull(Record.Exception(() =>
            {
                _ = order.Add(new List<Ticket>
                {
                    new Adult
                    {
                        PopcornOffer = true,
                        Screening = null
                    }
                });
            }));
        }

        [Fact]
        public void Add_Valid()
        {
            var tickets = Fixtures.Tickets;
            var screening = tickets[0].Screening;

            OrderRepositoryMock.Setup(o => o.FindAll())
                .Returns(Fixtures.Orders);
            ScreeningRepositoryMock.Setup(s => s.FindAll())
                .Returns(Fixtures.Screenings);
            ScreeningRepositoryMock.Setup(s =>
                    s.FindByNo(screening.ScreeningNo))
                .Returns(screening);

            var order = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                ScreeningRepositoryMock.Object);
            var output = order.Add(tickets);

            OrderRepositoryMock.Verify(o => o.FindAll(), Times.Once());
            ScreeningRepositoryMock.Verify(s => s.FindByNo(screening.ScreeningNo), Times.Once());

            var validOrder = new Core.Models.Order(Fixtures.Orders.Count + 1, Now)
            {
                TicketList = tickets,
                Status = "Unpaid"
            };

            ScreeningRepositoryMock.Verify(s =>
                s.UpdateSeatsRemaining(screening.ScreeningNo, screening.SeatsRemaining - tickets.Count));
            OrderRepositoryMock.Verify(o => o.Add(It.IsAny<Core.Models.Order>()), Times.Once());
            validOrder.Should().BeEquivalentTo(output);
        }

        [Fact]
        public void Pay_CallsRepositoryFindByNo_Invalid()
        {
            OrderRepositoryMock.Setup(o => o.FindByNo(1)).Returns(() => null);

            var order = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                ScreeningRepositoryMock.Object);

            Assert.NotNull(Record.Exception(() => { order.Pay(1); }));

            OrderRepositoryMock.Verify(o => o.FindByNo(1), Times.Once());
        }

        [Fact]
        public void Pay_Valid()
        {
            OrderRepositoryMock.Setup(o => o.FindByNo(1)).Returns(Fixtures.Orders[0]);

            var order = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                ScreeningRepositoryMock.Object);

            Assert.Null(Record.Exception(() => { order.Pay(1); }));

            OrderRepositoryMock.Verify(o => o.FindByNo(1), Times.Once());

            // This is okay since there are tests for .CalculatePrice()
            // We assume it is accurate, and check Pay() has the same implementation
            var payable = 0.0;
            foreach (var ticket in Fixtures.Orders[0].TicketList) payable += ticket.CalculatePrice();

            OrderRepositoryMock.Verify(o => o.UpdateAmount(1, payable), Times.Once());
            OrderRepositoryMock.Verify(o => o.UpdateStatus(1, "Paid"), Times.Once());
            OrderRepositoryMock.Verify(o => o.FindByNo(1), Times.Once());
        }

        [Fact]
        public void Cancel_ScreeningPassed_Throws()
        {
            // I'm not that old yet
            DateTimeProviderMock.Setup(d => d.Now()).Returns(DateTime.MaxValue);
            OrderRepositoryMock.Setup(o => o.FindByNo(1)).Returns(Fixtures.Orders[0]);

            var order = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                ScreeningRepositoryMock.Object);

            Assert.NotNull(Record.Exception(() => { order.Cancel(1); }));
        }

        [Fact]
        public void Cancel_Valid()
        {
            OrderRepositoryMock.Setup(o => o.FindByNo(1)).Returns(Fixtures.Orders[0]);

            var order = new Core.UseCases.Order(DateTimeProviderMock.Object, OrderRepositoryMock.Object,
                ScreeningRepositoryMock.Object);

            Assert.Null(Record.Exception(() => { order.Cancel(1); }));

            ScreeningRepositoryMock.Verify(s =>
                s.UpdateSeatsRemaining(Fixtures.Orders[0].TicketList[0].Screening.ScreeningNo,
                    Fixtures.Orders[0].TicketList[0].Screening.SeatsRemaining + Fixtures.Orders[0].TicketList.Count));
            OrderRepositoryMock.Verify(o => o.UpdateStatus(1, "Cancelled"));
        }
    }
}