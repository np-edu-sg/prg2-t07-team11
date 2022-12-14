//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Cli.Display
{
    public class Entry : BackgroundService
    {
        private readonly Cinema _cinema;
        private readonly IDisplay _display;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly Movie _movie;
        private readonly Screening _screening;

        public Entry(IHostApplicationLifetime hostApplicationLifetime, IDisplay display, Movie movie, Cinema cinema,
            Screening screening)
        {
            (_hostApplicationLifetime, _display, _movie, _cinema, _screening) =
                (hostApplicationLifetime, display, movie, cinema, screening);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rootMenu = new List<string>
            {
                "Load Movie and Cinema Data",
                "Load Screening Data",
                "List all movies",
                "List movie screenings",
                "Add a movie screening session",
                "Delete a movie screening session",
                "Order movie tickets",
                "Cancel order of ticket",
                "Recommend movies"
            };

            _display.Clear();
            _display.Header("Welcome to Singa Cineplexes");

            // Due to the generic host, SIGTERM will only issue a cancellation request.
            // If the user is still in the loop, the program may not exit.
            // Use option 0 to always exit successfully.
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine();
                Console.WriteLine();
                var option = _display.MenuInput(rootMenu, "Enter your option: ", "Please enter a valid option");
                Console.WriteLine();

                try
                {
                    switch (option)
                    {
                        case -1:
                            _hostApplicationLifetime.StopApplication();
                            return Task.CompletedTask;
                        case 0:
                            _movie.LoadData();
                            _cinema.LoadData();
                            break;
                        case 1:
                            _screening.LoadData();
                            break;
                        case 2:
                            _movie.ListAllMovies();
                            break;
                        case 3:
                            _screening.DisplayScreeningSessionsMovie();
                            break;
                        case 4:
                            _screening.AddScreening();
                            break;
                        case 5:
                            _screening.RemoveScreening();
                            break;
                        case 6:
                            _screening.OrderTickets();
                            break;
                        case 7:
                            _screening.CancelOrder();
                            break;
                        case 8:
                            _movie.RecommendTop3Movies();
                            break;
                    }
                }
                catch (Exception)
                {
                    // ignored as exceptions will be from user input cancellation
                }
            }

            return Task.CompletedTask;
        }
    }
}