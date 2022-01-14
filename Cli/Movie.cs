using System;
using Core.Repository;

namespace Cli
{
    public class Movie
    {
        private readonly IMovie _movie;

        public Movie(IMovie movie)
        {
            _movie = movie;
        }

        public void ListAllMovies()
        {
            foreach (var movie in _movie.Find()) Console.WriteLine(movie);
        }
    }
}