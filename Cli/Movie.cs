using System;
using Cli.Display;
using Core.Repository;

namespace Cli
{
    public class Movie
    {
        private readonly IMovie _movie;
        private readonly IDisplay _display;

        public Movie(IMovie movie, IDisplay display)
        {
            _movie = movie;
            _display = display;
        }

        public void ListAllMovies()
        {
            foreach (var movie in _movie.Find()) _display.Text(movie);
        }
    }
}