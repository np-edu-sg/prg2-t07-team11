using System;
using System.Collections.Generic;

using Cli.Display;
using Core.Repository;

namespace Cli
{
    public class Movie
    {
        private readonly IDisplay _display;
        private readonly IMovie _movie;
        public List<LegacyCommand> Commands { get; set; } = new();

        public Movie(IMovie movie, IDisplay display)
        {
            _movie = movie;
            _display = display;

            Commands.Add(new LegacyCommand("List all movies", ListAllMovies));
        }

        public void ListAllMovies()
        {
            foreach (var movie in _movie.Find()) _display.Text(movie);
        }
    }
}