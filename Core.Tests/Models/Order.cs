//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;
using System.Collections.Generic;
using Core.Models;
using Xunit;

namespace Core.Tests.Models
{
    public class Order
    {
        [Fact]
        public void Constructor_Default_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.Models.Order()));
        }

        [Fact]
        public void Constructor_Overload_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.Models.Order(0, DateTime.Now)));
        }

        [Fact]
        public void Constructor_Overload_Values_Valid()
        {
            var dateTime = DateTime.Now;
            var order = new Core.Models.Order(101, dateTime);

            Assert.Equal(dateTime, order.OrderDateTime);
            Assert.Equal(101, order.OrderNo);
        }

        [Fact]
        public void AddTicket_Valid()
        {
            var order = new Core.Models.Order();

            var screening = new Screening(1, DateTime.Now, "3D", new Core.Models.Cinema(),
                new Core.Models.Movie());
            var ticket = new Core.Models.Adult(screening, true);

            order.AddTicket(ticket);

            Assert.Equal(new List<Ticket> { ticket }, order.TicketList);
        }
    }
}