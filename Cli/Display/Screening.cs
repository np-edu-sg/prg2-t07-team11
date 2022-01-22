using System;
using System.Globalization;
using Cli.Display;
using Core.Repository;

namespace Cli.Display
{
    public class Screening
    {
        private readonly IDisplay _display;
        private readonly Core.UseCases.Screening _screening;
        private readonly Core.UseCases.Movie _movie;

        public Screening(Core.UseCases.Screening screening, IDisplay display, Core.UseCases.Movie movie)
        {
            _screening = screening;
            _display = display;
            _movie = movie;
        }


        public void LoadData()
        {
            _screening.LoadData();
            _display.Text("Loaded screening data!");
        }

        public void ListAllScreenings()
        {
            foreach (var screening in _screening.Find()) _display.Text(screening);
        }

        public void AddScreening()
        {
            var movies = _movie.Find();

            var movieIdxInput = _display.InteractiveTableSelect(movies, Core.Models.Movie.Header);

            var screenTypeInput = _display.Input<string>("Enter Screening Type [2D/3D]: ", "Wrong Screen Type",
                s => s is "2D" or "3D");
            var screeningDateTimeInput = _display.Input<DateTime>("Enter Screening Date And Time: ", "Input Is Not In DateTime format", s => DateTime.TryParse(s, out _));
            var cinemaNameInput = _display.Input<string>("Enter Cinema Name: ");
            var cinemaHallNoInput = _display.Input<int>("Enter Cinema Hall Number: ", "Input is not a integer", s => int.TryParse(s, out _));

            _screening.Add(screeningDateTimeInput, screenTypeInput, cinemaNameInput, cinemaHallNoInput, movies[movieIdxInput].Title);

            _display.Text("Successfully Added Screening Session");
        }

        public void RemoveScreening()
        {
            var screenings = _screening.FindAllWithoutTickets();
            var screeningIdxInput = _display.InteractiveTableSelect(screenings, Core.Models.Screening.Header);

            _screening.Remove(screenings[screeningIdxInput]);
        }
    }
}