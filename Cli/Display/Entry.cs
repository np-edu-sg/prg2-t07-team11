using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.Extensions.Hosting;

namespace Cli.Display
{
    public class Entry : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IDisplay _display;
        private readonly Movie _movie;
        private readonly Cinema _cinema;
        private readonly Screening _screening;

        public Entry(IHostApplicationLifetime hostApplicationLifetime, IDisplay display, Movie movie, Cinema cinema,
            Screening screening) =>
            (_hostApplicationLifetime, _display, _movie, _cinema, _screening) =
            (hostApplicationLifetime, display, movie, cinema, screening);

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
                "Recommend movies",
            };

            _display.Clear();
            _display.Header("Welcome to Singa Cineplexes");
            _display.Text(
                "Press CTRL-C or SIGTERM to request stop. The program may not honor your request. Use the Exit function instead to always exit successfully.");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine();
                Console.WriteLine();
                var option = _display.MenuInput(rootMenu, "Enter your option: ", "Please enter a valid option");
                Console.WriteLine();

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
                        _screening.ListAllScreenings();
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

            return Task.CompletedTask;
        }
    }
}