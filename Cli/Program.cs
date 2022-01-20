using System;
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
        [Obsolete]
        private static async Task Main(string[] args)
        {
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

            var movie = host.Services.GetRequiredService<Movie>();
            movie.LoadData();
            var cinema = host.Services.GetRequiredService<Cinema>();
            cinema.LoadData();
            var screening = host.Services.GetRequiredService<Screening>();
            screening.LoadData();

            movie.ListAllMovies();
            cinema.ListAllCinemas();
            screening.ListAllScreenings();

            await host.RunAsync();
        }
    }
}