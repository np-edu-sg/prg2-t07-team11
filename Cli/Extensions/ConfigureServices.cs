//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using Core.Repository;
using Core.Repository.Csv;
using Core.Repository.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddSingleton(typeof(IMovie), _ => new Movie("./Assets/Movie.csv"))
                .AddSingleton(typeof(ICinema), _ => new Cinema("./Assets/Cinema.csv"))
                .AddSingleton(typeof(IScreening),
                    s => new Screening("./Assets/Screening.csv",
                        s.GetRequiredService<IMovie>(), s.GetRequiredService<ICinema>()))
                .AddSingleton<IOrder, Order>();
        }

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            return services
                .AddSingleton<Core.UseCases.Movie>()
                .AddSingleton<Core.UseCases.Cinema>()
                .AddSingleton<Core.UseCases.Screening>()
                .AddSingleton<Core.UseCases.Order>();
        }
    }
}