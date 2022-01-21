using Core.Repository;
using Core.Repository.Csv;
using Core.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddSingleton(typeof(IMovie), _ => new Movie("./Assets/Movie.csv"))
                .AddSingleton(typeof(ICinema), _ => new Cinema("./Assets/Cinema.csv"))
                .AddSingleton(typeof(IScreening),
                    s => new Screening("./Assets/Screening.csv",
                        s.GetRequiredService<IMovie>(), s.GetRequiredService<ICinema>()));

        public static IServiceCollection AddUseCases(this IServiceCollection services) =>
            services
                .AddSingleton<Core.UseCases.Movie>()
                .AddSingleton<Core.UseCases.Cinema>()
                .AddSingleton<Core.UseCases.Screening>();
    }
}