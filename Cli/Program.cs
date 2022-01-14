using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services
                        .AddLogging(builder =>
                        {
                            builder.ClearProviders();
                        })
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