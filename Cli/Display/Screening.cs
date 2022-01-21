﻿using System;
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
            Console.WriteLine("{0,-5}{1,-30}{2,-10}{3,-15}{4,-15}", "S/N", "Movie Title", "Duration", "Classification", "Opening Date");
            var movies = _movie.Find();
            for (var idx = 0; idx < movies.Count; idx++) Console.WriteLine($"{idx + 1,-5}{movies[idx]}");
            var movieIdInput = _display.Input<int>("Select a Movie: ", "Input is not a integer", s => int.TryParse(s, out _));
            var screenTypeInput = _display.Input<string>("Enter Screening Type [2D/3D]: ", "Wrong Screen Type",
                s => s is "2D" or "3D");
            var screeningDateTimeInput = _display.Input<DateTime>("Enter Screening Date And Time: ", "Input Is Not In DateTime format", s => DateTime.TryParse(s, out _));
            var cinemaNameInput = _display.Input<string>("Enter Cinema Name: ");
            var cinemaHallNoInput = _display.Input<int>("Enter Cinema Hall Number: ", "Input is not a integer", s => int.TryParse(s, out _));
            _screening.Add(screeningDateTimeInput, screenTypeInput, cinemaNameInput, cinemaHallNoInput, movies[movieIdInput - 1].Title);
            _display.Text("Successfully Added Screening Session");
        }

        public void RemoveScreening()
        {
            Console.WriteLine("{0,-5}{1,-30}{2,-20}{3,-15}{4}", "S/N", "Date and Time of Screening", "Screening Type", "Hall Name", "Movie Title");
            var screenings = _screening.Find();
            for (var idx = 0; idx < screenings.Count; idx++) Console.WriteLine($"{screenings[idx]}");
            var screeningIdInput = _display.Input<int>("Select a Screening: ", "Input is not a integer", s => int.TryParse(s, out _));
            _screening.Remove(screenings[screeningIdInput-1]);
        }
    }
}