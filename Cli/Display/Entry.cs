﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
                "View Web API",
            };

            _display.Clear();
            _display.Header("Welcome to Singa Cineplexes");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine();
                var option = _display.MenuInput(rootMenu, "Enter your option: ", "Please enter a valid option");
                Console.WriteLine();

                switch (option)
                {
                    case 0:
                        _hostApplicationLifetime.StopApplication();
                        return;
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
                    case 7:
                        _screening.OrderTickets();
                        break;
                }
            }
        }
    }
}