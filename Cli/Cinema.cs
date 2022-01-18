using System.Collections.Generic;
using Cli.Display;
using Core.Repository;

namespace Cli
{
    public class Cinema
    {
        private readonly ICinema _cinema;
        private readonly IDisplay _display;
        public List<LegacyCommand> Commands { get; set; } = new();

        public Cinema(ICinema cinema, IDisplay display)
        {
            _cinema = cinema;
            _display = display;

            Commands.Add(new LegacyCommand("List all cinemas", ListAllCinemas));
        }

        public void ListAllCinemas()
        {
            foreach (var cinema in _cinema.Find()) _display.Text(cinema);
        }
    }
}