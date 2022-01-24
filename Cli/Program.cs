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
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Loading...");

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<Window>();

                    services
                        .AddLogging(builder => { builder.ClearProviders(); })
                        .AddHostedService(s => s.GetRequiredService<Window>())
                        .AddHostedService<Entry>()
                        .AddRepositories()
                        .AddUseCases()
                        .AddSingleton<IDisplay, ConsoleDisplay>()
                        .AddSingleton<Movie>()
                        .AddSingleton<Cinema>()
                        .AddSingleton<Screening>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}