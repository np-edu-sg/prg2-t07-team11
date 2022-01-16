using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Repository.Csv
{
    public class Screening : IScreening
    {
        private readonly List<Models.Screening> _screenings = new();

        public Screening(string path, IMovie movie, ICinema cinema)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new Exception("Bad path");

            try
            {
                var csvLines = File.ReadAllLines(path);
                for (var i = 1; i < csvLines.Length; i++)
                {
                    var data = csvLines[i].Split(",");
                    _screenings.Add(new Models.Screening(
                        i,
                        DateTime.Parse(data[0]),
                        data[1],
                        cinema.FindOneByNameAndHallNo(data[2], int.Parse(data[3])),
                        movie.FindOneByTitle(data[4])
                    ));
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
    }
}