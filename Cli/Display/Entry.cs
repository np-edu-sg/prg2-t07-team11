using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Cli.Display
{
    public class Entry : IHostedService
    {
        private readonly IDisplay _display;
        private readonly Movie _movie;
        private readonly Cinema _cinema;
        private readonly Screening _screening;

        public Entry(IDisplay display, Movie movie, Cinema cinema, Screening screening) =>
            (_display, _movie, _cinema, _screening) = (display, movie, cinema, screening);

        public Task StartAsync(CancellationToken cancellationToken)
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
                "Recommend movies",
                "Display available seats of screening session",
                "Start Web API",
            };

            // _display.Clear();
            _display.Header("Welcome to Singa Cineplexes");

            while (true)
            {
                Console.WriteLine();
                var option = _display.Menu(rootMenu, "Enter your option: ", "Please enter a valid option");
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        _movie.LoadData();
                        _cinema.LoadData();
                        break;
                    case 2:
                        _screening.LoadData();
                        break;
                    case 3:
                        _movie.ListAllMovies();
                        break;
                    case 4:
                        _screening.ListAllScreenings();
                        break;
                    case 5:
                        _screening.AddScreening();
                        break;
                    case 6:
                        _screening.RemoveScreening();
                        break;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}