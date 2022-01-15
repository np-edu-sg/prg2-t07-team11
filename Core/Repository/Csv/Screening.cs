using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Repository.Csv
{
    public class Screening : IScreening
    {
        private readonly List<Core.Models.Screening> _screenings = new();

        public Screening(string path, IMovie movie, ICinema cinema) 
        {
            if (string.IsNullOrWhiteSpace(path)) throw new Exception("Bad path");

            try
            {
                string[] csvLines = File.ReadAllLines(path);
                for (int i = 1; i < csvLines.Length; i++)
                {
                    string[] data = csvLines[i].Split(",");
                    _screenings.Add(new Core.Models.Screening(
                        i,
                        DateTime.Parse(data[0]),
                        data[1],
                        cinema.FindByNameAndHallNo(data[2], int.Parse(data[3])),
                        movie.FindByTitle(data[4])
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