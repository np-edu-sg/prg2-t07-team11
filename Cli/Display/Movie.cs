//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System.Collections.Generic;

namespace Cli.Display
{
    public struct MovieLeaderboardItem
    {
        public int Pos { get; set; }
        public string Title { get; set; }
        public int TicketsSold { get; set; }

        public static string Header = $"{"Pos",-5}{"Movie Title",-30}{"Tickets sold",-4}";

        public MovieLeaderboardItem(int pos, string name, int ticketsSold)
        {
            (Pos, Title, TicketsSold) = (pos, name, ticketsSold);
        }

        public override string ToString()
        {
            return $"{Pos,-5}{Title,-30}{TicketsSold,-4}";
        }
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
            var movies = _movie.FindAll();
            if (movies.Count == 0)
            {
                _display.Text("There are no movies");
                return;
            }

            _display.Table(movies, Core.Models.Movie.Header);
        }

        public void RecommendTop3Movies()
        {
            var top = _movie.Top3Movies();

            if (top.Count == 0)
            {
                _display.Text("No recommendations yet, order a ticket first.");
                return;
            }

            var (m, idx) = (new List<MovieLeaderboardItem>(), 1);
            foreach (var (key, value) in top)
            {
                m.Add(new MovieLeaderboardItem(idx, key, value));
                idx++;
            }

            _display.Table(m, MovieLeaderboardItem.Header);
        }
    }
}