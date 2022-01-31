//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;
using System.Collections.Generic;
using Core.Models;
using Order = Core.UseCases.Order;

namespace Cli.Display
{
    /// <summary>
    /// This is a class to show a custom header for Screening to be used with IDisplay.Table
    /// </summary>
    public class CustomScreening : Core.Models.Screening
    {
        public static new string Header =
            $"{"Movie Title",-30}{"Cinema",-15}{"Hall No",-10}{"Screening Type",-16}{"Date and Time",-25}{"Seats Remaining",-17}";

        public CustomScreening(Core.Models.Screening screening)
        {
            ScreeningNo = screening.ScreeningNo;
            ScreeningDateTime = screening.ScreeningDateTime;
            ScreeningType = screening.ScreeningType;
            Cinema = screening.Cinema;
            Movie = screening.Movie;
            SeatsRemaining = screening.SeatsRemaining;
        }

        public override string ToString()
        {
            return
                $"{Movie.Title,-30}{Cinema.Name,-15}{Cinema.HallNo,-10}{ScreeningType,-16}{ScreeningDateTime,-25}{SeatsRemaining,-17}";
        }
    }

    public class Screening
    {
        private readonly Core.UseCases.Cinema _cinema;

        private readonly Dictionary<string, int> _classifications = new()
        {
            {"PG13", 13},
            {"NC16", 16},
            {"M18", 18},
            {"R21", 21}
        };

        private readonly IDisplay _display;
        private readonly Core.UseCases.Movie _movie;
        private readonly Order _order;
        private readonly Core.UseCases.Screening _screening;

        private readonly List<string> _study = new() {"Primary", "Secondary", "Tertiary"};
        private readonly List<string> _ticketType = new() {"Student", "Senior Citizen", "Adult"};

        public Screening(
            IDisplay display,
            Core.UseCases.Screening screening,
            Core.UseCases.Movie movie,
            Order order,
            Core.UseCases.Cinema cinema
        )
        {
            _display = display;
            _screening = screening;
            _display = display;
            _movie = movie;
            _order = order;
            _cinema = cinema;
        }

        public void LoadData()
        {
            _screening.LoadData();
            _display.Text("Loaded screening data!");
        }

        public void AddScreening()
        {
            var movies = _movie.FindAll();

            var movieIdxInput = _display.InteractiveTableInput(movies, Core.Models.Movie.Header, "Choose a screening");
            if (movieIdxInput == -1) return;

            var screenTypeInput = _display.Input<string>("Enter Screening Type [2D/3D]: ", "Wrong Screen Type",
                s => s is "2D" or "3D");
            var screeningDateTimeInput = _display.Input<DateTime>("Enter Screening Date And Time: ",
                "Input Is Not In DateTime format", s => DateTime.TryParse(s, out _));

            var cinemas = _cinema.FindAll();
            var cinemaIdx = _display.InteractiveTableInput(cinemas, Core.Models.Cinema.Header, "Choose a cinema");

            try
            {
                _screening.Add(
                    screeningDateTimeInput,
                    screenTypeInput,
                    cinemas[cinemaIdx].Name,
                    cinemas[cinemaIdx].HallNo,
                    movies[movieIdxInput].Title
                );
            }
            catch (Exception ex)
            {
                _display.Error(ex.Message);
                return;
            }


            _display.Text("Successfully Added Screening Session");
        }

        // Richard did this
        public void DisplayScreeningSessionsMovie()
        {
            var movies = _movie.FindAll();
            if (movies.Count == 0)
            {
                _display.Text("There are no movies");
                return;
            }

            var movieIdxInput = _display.InteractiveTableInput(movies, Core.Models.Movie.Header, "Choose a movie");
            if (movieIdxInput == -1) return;

            var screenings = _screening.FindAllByMovieTitle(movies[movieIdxInput].Title);

            if (screenings.Count == 0)
            {
                _display.Text($"There are no screenings for {movies[movieIdxInput].Title}");
                return;
            }

            var customScreenings = new List<CustomScreening>();
            foreach (var s in screenings) customScreenings.Add(new CustomScreening(s));

            _display.Table(customScreenings, CustomScreening.Header);
        }

        public void RemoveScreening()
        {
            var screenings = _screening.FindAllWithoutTickets();
            if (screenings.Count == 0)
            {
                _display.Text("There are no screenings to delete");
                return;
            }

            var screeningIdxInput = _display.InteractiveTableInput(screenings, Core.Models.Screening.Header, "Choose a screening");
            if (screeningIdxInput == -1) return;

            _screening.Remove(screenings[screeningIdxInput]);
        }

        public void OrderTickets()
        {
            var movies = _movie.FindAll();
            var movieIdx = _display.InteractiveTableInput(movies, Core.Models.Movie.Header, "Choose a movie");
            if (movieIdx == -1) return;

            var screenings = _screening.FindAllByMovieTitle(movies[movieIdx].Title);
            if (screenings.Count == 0)
            {
                _display.Error("There are no screenings available for this movie D:");
                return;
            }

            var screeningIdx = _display.InteractiveTableInput(screenings, Core.Models.Screening.Header, "Choose a screening");
            if (screeningIdx == -1) return;

            _display.Text($"Number of tickets left: {screenings[screeningIdx].SeatsRemaining}");
            var noTickets = _display.Input<int>("Enter number of tickets: ",
                "There are either not enough seats remaining, or you have entered an invalid number.\nHint: Please enter a non-zero integer.\n",
                s => int.TryParse(s, out var s2) && 0 < s2 && s2 <= screenings[screeningIdx].SeatsRemaining);

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

                Ticket ticket;
                while (true)
                {
                    var type = _display.InteractiveTableInput(
                        _ticketType,
                        $"[Ticket {idx + 1}/{noTickets}] Please select type: ",
                        "Choose a ticket type"
                    );
                    if (type == -1) return;

                    if (type == 0)
                    {
                        var idx2 = _display.InteractiveTableInput(
                            _study,
                            $"[Ticket {idx + 1}/{noTickets}] Please enter level of study: ",
                            "Choose your level of study"
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

                payable += ticket.CalculatePrice();
            }

            var order = _order.Add(tickets);

            _display.Text("Confirm order:");
            _display.Text($"Movie title: {movies[movieIdx].Title}");
            _display.Text($"Movie type: {screenings[screeningIdx].ScreeningType}");
            _display.Text($"Screening date time: {screenings[screeningIdx].ScreeningDateTime}");
            _display.Text($"Cinema: {screenings[screeningIdx].Cinema.Name}");

            _display.Text("");

            _display.Text($"No. of tickets: {noTickets}");
            _display.Text($"Payable amount: ${payable}");
            _display.Input<string>("Press any key to make payment...");

            _order.Pay(order.OrderNo);

            _display.Text("You have successfully made payment, thank you!");
            _display.Text($"Order number: {order.OrderNo}");
        }

        public void CancelOrder()
        {
            if (_order.FindAll().Count == 0)
            {
                _display.Text("There are no orders to cancel");
                return;
            }

            var orderNo = _display.Input<int>("Enter your order number: ", "Invalid order number",
                s => int.TryParse(s, out var s2) && _order.FindByNo(s2) is not null);
            var order = _order.FindByNo(orderNo);

            try
            {
                _order.Cancel(orderNo);
                _display.Text($"You have been refunded ${order.Amount} for order no {orderNo}");
                _display.Text($"You have successfully cancelled order no {orderNo}");
            }
            catch (Exception ex)
            {
                _display.Error(ex.Message);
                _display.Error($"Failed to cancel order no {orderNo}");
            }
        }
    }
}