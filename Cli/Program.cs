using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Cli.Display;
using Cli.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Cli
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Loading...");

            await Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(kestrel => { kestrel.ListenAnyIP(8080); });
                    webBuilder.ConfigureServices(services => { services.AddControllers(); });
                    webBuilder.Configure((app) =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints => endpoints.MapControllers());
                    });
                    webBuilder.UseShutdownTimeout(new TimeSpan(0, 0, 1));
                })
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