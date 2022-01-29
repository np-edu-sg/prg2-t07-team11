using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.Tests
{
    public static class Fixtures
    {
        public static List<Screening> Screenings = new()
        {
            new Screening(
                21,
                DateTime.Now,
                "3D",
                new Cinema(),
                new Movie()
            ),
            new Screening(
                22,
                DateTime.Now,
                "2D",
                new Cinema(),
                new Movie()
            ),
            new Screening(
                23,
                DateTime.Now,
                "3D",
                new Cinema(),
                new Movie()
            ),
        };

        public static List<Ticket> Tickets = new()
        {
            new Adult(Screenings[0], true),
            new Student(Screenings[1], "Year 1"),
            new SeniorCitizen(Screenings[2], 2004)
        };

        public static List<Order> Orders = new()
        {
            new Order(1, DateTime.Now)
            {
                TicketList = Tickets
            },
            new Order(2, DateTime.Now)
            {
                TicketList = Tickets
            },
            new Order(3, DateTime.Now)
            {
                TicketList = Tickets
            },
            new Order(4, DateTime.Now)
            {
                TicketList = Tickets
            },
        };
    }
}