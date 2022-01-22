using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cli.Display;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Cli.Extensions;

namespace Cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Loading...");

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services
                        .AddLogging(builder => { builder.ClearProviders(); })
                        .AddRepositories()
                        .AddUseCases()
                        .AddSingleton<IDisplay, ConsoleDisplay>()
                        .AddSingleton<Movie>()
                        .AddSingleton<Cinema>()
                        .AddSingleton<Screening>();
                })
                .Build();

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

            var display = host.Services.GetRequiredService<IDisplay>();
            var movie = host.Services.GetRequiredService<Movie>();
            var cinema = host.Services.GetRequiredService<Cinema>();
            var screening = host.Services.GetRequiredService<Screening>();

            display.Clear();
            display.Header("Welcome to Singa Cineplexes");

            while (true)
            {
                Console.WriteLine();
                var option = display.Menu(rootMenu, "Enter your option: ", "Please enter a valid option");
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        movie.LoadData();
                        cinema.LoadData();
                        break;
                    case 2:
                        screening.LoadData();
                        break;
                    case 3:
                        movie.ListAllMovies();
                        break;
                    case 4:
                        screening.ListAllScreenings();
                        break;
                    case 5:
                        screening.AddScreening();
                        break;
                    case 6:
                        screening.RemoveScreening();
                        break;
                }
            }
        }
    }
}