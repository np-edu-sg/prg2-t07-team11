using System;
using System.Threading.Tasks;
using Cli.Display;
using Core.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Cli.Display;
using Cli.Extensions;
using Core.Models;
using Core.Repository;

namespace Cli
{
    internal class Program
    {
        [Obsolete]
        private static async Task Main(string[] args)
        {
            var interactive = false;
            while (true)
            {
                Console.Write("[B]asic or [I]nteractive mode: ");
                if (Console.ReadLine()?.ToLower() == "i") interactive = true;
                break;
            }

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<Window>();

                    if (interactive) services.AddSingleton<IDisplay, InteractiveDisplay>();
                    else services.AddSingleton<IDisplay, BasicDisplay>();

                    services
                        .AddLogging(builder => { builder.ClearProviders(); })
                        .AddHostedService(s => s.GetRequiredService<Window>())
                        .AddScreens()
                        .AddRepositories()
                        .AddUseCases()
                        .AddSingleton<Movie>()
                        .AddSingleton<Cinema>()
                        .AddSingleton<Screening>();
                })
                .Build();

            var display = host.Services.GetRequiredService<IDisplay>();
            var movie = host.Services.GetRequiredService<Movie>();
            var cinema = host.Services.GetRequiredService<Cinema>();
            var screening = host.Services.GetRequiredService<Screening>();

            movie.ListAllMovies();
            cinema.ListAllCinemas();
            screening.ListAllScreenings();

            var root = new RootCommand(
                "Programming 2 Assignment",
                cinema.Commands,
                movie.Commands
            );

            // Uncomment to get interface
            // display.Run(root);

            await host.RunAsync();
        }
    }
}