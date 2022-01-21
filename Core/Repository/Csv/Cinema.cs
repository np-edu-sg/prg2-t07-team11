using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Repository.Csv
{
    public class Cinema : ICinema
    {
        private readonly string _path;
        private readonly List<Models.Cinema> _cinemas = new();

        public Cinema(string path)
        {
            _path = path;
        }

        public void Init()
        {
            if (string.IsNullOrWhiteSpace(_path)) throw new Exception("Bad path");

            try
            {
                var csvLines = File.ReadAllLines(_path);
                foreach (var line in csvLines[1..])
                {
                    var data = line.Split(",");
                    _cinemas.Add(new Models.Cinema(
                        Convert.ToString(data[0]),
                        Convert.ToInt32(data[1]),
                        Convert.ToInt32(data[2])
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to read file", ex);
            }
        }

        public List<Models.Cinema> FindAll()
        {
            return _cinemas;
        }

        public Models.Cinema FindOneByNameAndHallNo(string cinemaName, int hallNo)
        {
            return _cinemas.Find(c => c.Name == cinemaName && c.HallNo == hallNo);
        }
    }
}