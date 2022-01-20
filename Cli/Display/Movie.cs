using Core.Repository;

namespace Cli.Display
{
    public class Movie
    {
        private readonly IDisplay _display;
        private readonly Core.UseCase.Movie _movie;

        public Movie(Core.UseCase.Movie movie, IDisplay display)
        {
            _movie = movie;
            _display = display;
        }

        public void LoadData()
        {
            _movie.LoadData();
            _display.Text("Loaded movie data!");
        }

        public void ListAllMovies()
        {
            _display.Text(Core.Models.Movie.Header);
            foreach (var movie in _movie.Find()) _display.Text(movie);
        }
    }
}