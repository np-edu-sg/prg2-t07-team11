//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.Tests
{
    public static class Fixtures
    {
        public static Cinema Cinema = new("Bishan GV", 1, 100);
        public static Movie Movie = new("Yay", 1, "G", DateTime.Now, new List<string> { "Happy" });

        public static List<Screening> Screenings = new()
        {
            new Screening(
                1001,
                DateTime.Now,
                "3D",
                Cinema,
                Movie
            ),
            new Screening(
                1002,
                DateTime.Now,
                "2D",
                Cinema,
                Movie
            ),
            new Screening(
                1003,
                DateTime.Now,
                "3D",
                Cinema,
                Movie
            )
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
            }
        };
    }
}