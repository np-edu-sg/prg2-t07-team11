using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Core.Repository.Csv
{
    public class Screening : IScreening
    {
        private readonly string _path;
        private readonly IMovie _movie;
        private readonly ICinema _cinema;
        private readonly List<Models.Screening> _screenings = new();
        public Screening(string path, IMovie movie, ICinema cinema) => (_path, _movie, _cinema) = (path, movie, cinema);

        public void Init()
        {
            if (string.IsNullOrWhiteSpace(_path)) throw new Exception("Bad path");

            try
            {
                var csvLines = File.ReadAllLines(_path);
                for (var i = 1; i < csvLines.Length; i++)
                {
                    var data = csvLines[i].Split(",");

                    var m = _movie.FindOneByTitle(data[4]);
                    if (m is null) throw new Exception("Movie not found");

                    var c = _cinema.FindOneByNameAndHallNo(data[2], int.Parse(data[3]));
                    if (c is null) throw new Exception("Cinema not found");

                    var screening = new Models.Screening(
                        i,
                        DateTime.Parse(data[0], new CultureInfo("en-SG")),
                        data[1],
                        c,
                        m
                    );

                    m.AddScreening(screening);
                    _screenings.Add(screening);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to read file", ex);
            }
        }

        public List<Models.Screening> Find()
        {
            return _screenings;
        }

        public List<Models.Screening> FindByCinema(Models.Cinema cinema)
        {
            return _screenings.FindAll(s => s.Cinema == cinema);
        }

        public void Add(Models.Screening screening)
        {
            _screenings.Add(new Models.Screening(_screenings.Count + 1, screening.ScreeningDateTime,
                screening.ScreeningType, screening.Cinema, screening.Movie));
        }
    }
}