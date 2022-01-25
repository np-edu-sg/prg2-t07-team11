﻿using System;
using System.Collections.Generic;
using Core.Models;

namespace Cli.Display
{
    public class Screening
    {
        private readonly Dictionary<string, int> _classifications = new()
        {
            {"PG13", 13},
            {"NC16", 16},
            {"M18", 18},
            {"R21", 21}
        };

        private readonly List<string> _study = new() {"Primary", "Secondary", "Tertiary"};
        private readonly List<string> _ticketType = new() {"Student", "Senior Citizen", "Adult"};

        private readonly IDisplay _display;
        private readonly Core.UseCases.Screening _screening;
        private readonly Core.UseCases.Movie _movie;
        private readonly Core.UseCases.Order _order;

        public Screening(IDisplay display, Core.UseCases.Screening screening, Core.UseCases.Movie movie,
            Core.UseCases.Order order)
        {
            _display = display;
            _screening = screening;
            _display = display;
            _movie = movie;
            _order = order;
        }

        public void LoadData()
        {
            _screening.LoadData();
            _display.Text("Loaded screening data!");
        }

        public void ListAllScreenings()
        {
            foreach (var screening in _screening.Find()) _display.Text(screening);
        }

        public void AddScreening()
        {
            var movies = _movie.Find();

            var movieIdxInput = _display.InteractiveTableInput(movies, Core.Models.Movie.Header);
            if (movieIdxInput == -1) return;


            var screenTypeInput = _display.Input<string>("Enter Screening Type [2D/3D]: ", "Wrong Screen Type",
                s => s is "2D" or "3D");
            var screeningDateTimeInput = _display.Input<DateTime>("Enter Screening Date And Time: ",
                "Input Is Not In DateTime format", s => DateTime.TryParse(s, out _));
            var cinemaNameInput = _display.Input<string>("Enter Cinema Name: ");
            var cinemaHallNoInput = _display.Input<int>("Enter Cinema Hall Number: ", "Input is not a integer",
                s => int.TryParse(s, out _));

            _screening.Add(screeningDateTimeInput, screenTypeInput, cinemaNameInput, cinemaHallNoInput,
                movies[movieIdxInput].Title);

            _display.Text("Successfully Added Screening Session");
        }

        public void RemoveScreening()
        {
            var screenings = _screening.FindAllWithoutTickets();
            var screeningIdxInput = _display.InteractiveTableInput(screenings, Core.Models.Screening.Header);
            if (screeningIdxInput == -1) return;

            _screening.Remove(screenings[screeningIdxInput]);
        }

        public void OrderTickets()
        {
            var movies = _movie.Find();
            var movieIdx = _display.InteractiveTableInput(movies, Core.Models.Movie.Header);
            if (movieIdx == -1) return;

            var screenings = _screening.FindAllByMovieTitle(movies[movieIdx].Title);
            if (screenings.Count == 0)
            {
                _display.Error("There are no screenings available for this movie D:");
                return;
            }

            var screeningIdx = _display.InteractiveTableInput(screenings, Core.Models.Screening.Header);
            if (screeningIdx == -1) return;

            _display.Text(screenings[screeningIdx].SeatsRemaining);

            var noTickets = _display.Input<int>("Enter number of tickets: ",
                "Either enter a valid number, or there are not enough seats remaining",
                s => int.TryParse(s, out var s2) && s2 <= screenings[screeningIdx].SeatsRemaining);

            var payable = 0.0;
            var tickets = new List<Ticket>();

            for (var idx = 0; idx < noTickets; idx++)
            {
                if (
                    movies[movieIdx].Classification != "G" &&
                    _display.Input<string>(
                        $"Is ticket holder {idx + 1} aged {_classifications[movies[movieIdx].Classification]} and above [Y/n]: ",
                        "Input is not valid",
                        s => s.ToLower() is "y" or "n") == "n"
                )
                {
                    _display.Error(
                        $"You must be aged {_classifications[movies[movieIdx].Classification]} and above to watch this movie D:");
                    return;
                }

                ;

                var ticket = default(Ticket);
                while (true)
                {
                    var type = _display.InteractiveTableInput(
                        _ticketType,
                        $"[Ticket {idx + 1}/{noTickets}] Please select type: "
                    );
                    if (type == -1) return;

                    if (type == 0)
                    {
                        var idx2 = _display.InteractiveTableInput(
                            _study,
                            $"[Ticket {idx + 1}/{noTickets}] Please enter level of study: "
                        );
                        if (idx2 == -1) return;

                        ticket = new Student(screenings[screeningIdx], _study[idx2]);

                        break;
                    }

                    if (type == 1)
                    {
                        var year = _display.Input<int>(
                            $"[Ticket {idx + 1}/{noTickets}] Please enter year of birth: ",
                            "Invalid year",
                            s => int.TryParse(s, out _)
                        );

                        if (DateTime.Now.Year - year < 55)
                        {
                            _display.Error("This ticket holder is not eligible for the Senior Citizen ticket");
                            return;
                        }

                        ticket = new SeniorCitizen(screenings[screeningIdx], year);

                        break;
                    }

                    if (type == 2)
                    {
                        var popcorn = _display.Input<string>(
                            $"[Ticket {idx + 1}/{noTickets}] Would you like popcorn for $3? [Y/n]: ",
                            "Invalid option",
                            s => s.ToLower() is "y" or "n"
                        ) == "y";

                        ticket = new Adult(screenings[screeningIdx], popcorn);
                        break;
                    }
                }

                tickets.Add(ticket);

                // This should be in the use case but I'll change it later
                if (ticket != default(Ticket)) payable += ticket.CalculatePrice();
            }

            var order = _order.Add(tickets);

            _display.Text($"Confirm order:");
            _display.Text($"Movie title: {movies[movieIdx].Title}");
            _display.Text($"Screening date time: {screenings[screeningIdx].ScreeningDateTime}");
            _display.Text($"Cinema: {screenings[screeningIdx].Cinema.Name}");

            _display.Text("");

            _display.Text($"No. of tickets: {noTickets}");
            _display.Text($"Payable amount: {payable}");
            _display.Input<string>("Press any key to make payment...");

            _order.Pay(order.OrderNo);

            _display.Text("You've successfully made payment, thank you!");
        }
    }
}