using Cli.Display;
using Core.Repository;

namespace Cli
{
    public class Cinema
    {
        private readonly ICinema _cinema;
        private readonly IDisplay _display;

        public Cinema(ICinema cinema, IDisplay display)
        {
            _cinema = cinema;
            _display = display;
        }

        public void ListAllCinemas()
        {
            foreach (var cinema in _cinema.Find()) _display.Text(cinema);
        }
    }
}