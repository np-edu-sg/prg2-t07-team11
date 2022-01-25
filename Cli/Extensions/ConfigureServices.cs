using Core.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddSingleton(typeof(IMovie), _ => new Core.Repository.Csv.Movie("./Assets/Movie.csv"))
                .AddSingleton(typeof(ICinema), _ => new Core.Repository.Csv.Cinema("./Assets/Cinema.csv"))
                .AddSingleton(typeof(IScreening),
                    s => new Core.Repository.Csv.Screening("./Assets/Screening.csv",
                        s.GetRequiredService<IMovie>(), s.GetRequiredService<ICinema>()))
                .AddSingleton<IOrder, Core.Repository.InMemory.Order>();

        public static IServiceCollection AddUseCases(this IServiceCollection services) =>
            services
                .AddSingleton<Core.UseCases.Movie>()
                .AddSingleton<Core.UseCases.Cinema>()
                .AddSingleton<Core.UseCases.Screening>()
                .AddSingleton<Core.UseCases.Order>();
    }
}