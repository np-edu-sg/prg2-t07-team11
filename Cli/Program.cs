using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Cli.Display;
using Cli.Extensions;

namespace Cli
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Loading...");

            await Host.CreateDefaultBuilder(args)
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
                .RunConsoleAsync();
        }
    }
}