﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Core.Repository.Csv
{
    public class Movie : IMovie
    {
        private readonly List<Core.Models.Movie> _movies = new();

        public Movie(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new Exception("Bad path");

            try
            {
                var file = File.ReadAllLines(path);
                foreach (var line in file[1..])
                {
                    var split = line.Split(",");
                    var genres = new List<string>();

                    foreach (var genre in split[2].Split("/")) genres.Add(genre);

                    _movies.Add(new Core.Models.Movie(
                        split[0],
                        int.Parse(split[1]),
                        split[3],
                        DateTime.Parse(split[4], new CultureInfo("en-SG")),
                        genres
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to read file", ex);
            }
        }

        public List<Models.Movie> Find()
        {
            return _movies;
        }
    }
}