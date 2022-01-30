using System;
using System.Collections.Generic;
using Core.Repository;
using Microsoft.Extensions.Options;

namespace Cli.Display
{
    public struct MovieLeaderboard
    {
        public int Pos { get; set; }
        public string Name { get; set; }
        public int TicketsSold { get; set; }

        public static string Header = $"{"Pos",-5}{"Name",-15}{"Tickets sold",-4}";

        public MovieLeaderboard(int pos, string name, int ticketsSold) =>
            (Pos, Name, TicketsSold) = (pos, name, ticketsSold);

        public override string ToString() => $"{Pos,-5}{Name,-15}{TicketsSold,-4}";
    }

    public class Movie
    {
        private readonly IDisplay _display;
        private readonly Core.UseCases.Movie _movie;

        public Movie(Core.UseCases.Movie movie, IDisplay display)
        {
            _movie = movie;
            _display = display;
        }

        public void LoadData()
        {
            _movie.LoadData();
            _display.Text("Loaded movie data!");
        }

        public void ListAllMovies()
        {
            _display.Text(Core.Models.Movie.Header);
            foreach (var movie in _movie.FindAll()) _display.Text(movie);
        }

        public void RecommendTop3Movies()
        {
            var top = _movie.Top3Movies();

            if (top.Count == 0)
            {
                _display.Text("No recommendations yet, order a ticket first.");
                return;
            }

            var (m, idx) = (new List<MovieLeaderboard>(), 1);
            foreach (var (key, value) in top)
            {
                m.Add(new MovieLeaderboard(idx, key, value));
                idx++;
            }

            _display.Table(m, MovieLeaderboard.Header);
        }
    }
}