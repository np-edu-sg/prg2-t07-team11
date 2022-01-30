//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Core.Repository.Csv
{
    public class Movie : IMovie
    {
        private readonly List<Models.Movie> _movies = new();
        private readonly string _path;

        public Movie(string path)
        {
            _path = path;
        }

        public void Init()
        {
            if (string.IsNullOrWhiteSpace(_path)) throw new Exception("Bad path");

            try
            {
                var file = File.ReadAllLines(_path);
                foreach (var line in file[1..])
                {
                    var split = line.Split(",");
                    var genres = new List<string>();

                    foreach (var genre in split[2].Split("/")) genres.Add(genre);

                    _movies.Add(new Models.Movie(
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

        public List<Models.Movie> FindAll()
        {
            return _movies;
        }

        public Models.Movie FindOneByTitle(string title)
        {
            return _movies.Find(m => m.Title == title);
        }
    }
}