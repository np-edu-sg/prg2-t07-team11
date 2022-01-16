using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Cli.Display;
using Core.Repository;

namespace Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
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
                        .AddSingleton(typeof(IMovie), _ => new Core.Repository.Csv.Movie("./Assets/Movie.csv"))
                        .AddSingleton(typeof(ICinema), _ => new Core.Repository.Csv.Cinema("./Assets/Cinema.csv"))
                        .AddSingleton<Core.UseCase.Movie>()
                        .AddSingleton<Core.UseCase.Cinema>()
                        .AddSingleton<Cinema>()
                        .AddSingleton<Movie>();
                })
                .Build();

            var display = host.Services.GetRequiredService<IDisplay>();
            var movie = host.Services.GetRequiredService<Movie>();
            var cinema = host.Services.GetRequiredService<Cinema>();

            var root = new RootCommand(
                "Programming 2 Assignment",
                cinema.Commands,
                movie.Commands
            );

            display.Run(root);

            await host.RunAsync();
        }
    }
}