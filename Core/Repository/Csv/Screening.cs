//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Core.Models;

namespace Core.Repository.Csv
{
    public class Screening : IScreening
    {
        private readonly ICinema _cinema;
        private readonly IMovie _movie;
        private readonly string _path;

        private readonly List<Models.Screening> _screenings = new();

        // For convenience; The tickets will be stored by reference, so it should be alright
        private readonly List<Ticket> _tickets = new();

        public Screening(string path, IMovie movie, ICinema cinema)
        {
            (_path, _movie, _cinema) = (path, movie, cinema);
        }

        public void Init()
        {
            if (_screenings.Count != 0) return;
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
                    )
                    {
                        SeatsRemaining = c.Capacity
                    };

                    m.AddScreening(screening);
                    _screenings.Add(screening);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to read file", ex);
            }
        }

        public Models.Screening FindByNo(int no)
        {
            return _screenings.Find(s => s.ScreeningNo == no);
        }

        public List<Models.Screening> FindAll()
        {
            return _screenings;
        }

        public List<Models.Screening> FindAllByMovieTitle(string title)
        {
            return _screenings.FindAll(s => s.Movie.Title == title);
        }

        public List<Models.Screening> FindAllByCinema(Models.Cinema cinema)
        {
            return _screenings.FindAll(s => s.Cinema == cinema);
        }

        public void Add(Models.Screening screening)
        {
            _screenings.Add(screening);
        }

        public void UpdateSeatsRemaining(int no, int seatsRemaining)
        {
            var screening = _screenings.Find(s => s.ScreeningNo == no);
            if (screening is null) throw new Exception($"Screening not found for no: {no}");

            screening.SeatsRemaining = seatsRemaining;
        }

        public void Remove(Models.Screening screening)
        {
            _screenings.Remove(screening);
        }

        public List<Models.Screening> FindAllWithoutTickets()
        {
            var screenings = new List<Models.Screening>();
            foreach (var screening in _screenings)
            {
                if (FindTicketsByScreeningNo(screening.ScreeningNo).Count > 0) continue;

                screenings.Add(screening);
            }

            return screenings;
        }

        public void AddTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
        }


        private List<Ticket> FindTicketsByScreeningNo(int no)
        {
            return _tickets.FindAll(t => t.Screening.ScreeningNo == no);
        }
    }
}