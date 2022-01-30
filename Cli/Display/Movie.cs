using System;
using System.Collections.Generic;
using Core.Repository;

namespace Cli.Display
{
    public class Movie
    {
        private readonly IDisplay _display;
        private readonly Core.UseCases.Movie _movie;

        public Movie(Core.UseCases.Movie movie, IDisplay display)
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
            foreach (var movie in _movie.FindAll()) _display.Text(movie);
        }

        public void RecommendTop3Movie()
        {
            var top = _movie.RecommendedMovieBasedInTickets();
            var idx = 1;
            Console.WriteLine("Top 3 movies based on tickets sold");
            foreach (var kvp in top)
            {
                Console.WriteLine($"{idx,-5}{kvp.Key}");
                idx++;
            }
        }
    }
}