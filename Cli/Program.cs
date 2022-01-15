using System;
using System.Threading.Tasks;
using Cli.Display;
using Core.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

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
                if (Console.ReadLine() == "I") interactive = true;
                break;
            }

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    if (interactive) services.AddSingleton<IDisplay, InteractiveDisplay>();
                    else services.AddSingleton<IDisplay, BasicDisplay>();

                    services
                        .AddLogging(builder => { builder.ClearProviders(); })
                        .AddSingleton(typeof(IMovie), _ => new Core.Repository.Csv.Movie("./Assets/Movie.csv"))
                        .AddSingleton<Core.UseCase.Movie>()
                        .AddSingleton<Movie>();
                })
                .Build();

            var movie = host.Services.GetRequiredService<Movie>();
            movie.ListAllMovies();

            await host.RunAsync();
        }
    }
}