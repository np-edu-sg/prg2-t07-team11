﻿using System.Collections.Generic;
using Core.Models;

namespace Core.Repository
{
    public interface IMovieReader
    {
        public List<Movie> Find();
        public Movie FindByTitle(string movieTitle);
    }

    public interface IMovieWriter
    {
    }

    public interface IMovie : IMovieReader, IMovieWriter
    {
    }
}