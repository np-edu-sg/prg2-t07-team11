using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Repository.Csv
{
    public class Cinema : ICinema
    {
        private readonly List<Core.Models.Cinema> _cinemas = new();

        public Cinema(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new Exception("Bad path");

            try
            {
                string[] csvLines = File.ReadAllLines(path);
                foreach (var line in csvLines[1..])
                {
                    string[] data = line.Split(",");
                    _cinemas.Add(new Core.Models.Cinema(
                        Convert.ToString(data[0]),
                        Convert.ToInt32(data[1]),
                        Convert.ToInt32(data[2])));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to read file", ex);
            }
        }

        public List<Models.Cinema> Find()
        {
            return _cinemas;
        }

        public Models.Cinema FindByNameAndHallNo(string cinemaName, int hallNo)
        {
            foreach (var cinema in _cinemas)
            {
                if (cinema.Name == cinemaName & cinema.HallNo == hallNo)
                {
                    return cinema;
                }
            }
            return null;
        }
    }
}