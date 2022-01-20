using Core.Repository;

namespace Cli.Display
{
    public class Cinema
    {
        private readonly Core.UseCase.Cinema _cinema;
        private readonly IDisplay _display;

        public Cinema(Core.UseCase.Cinema cinema, IDisplay display)
        {
            _cinema = cinema;
            _display = display;
        }

        public void LoadData() => _cinema.LoadData();

        public void ListAllCinemas()
        {
            foreach (var cinema in _cinema.Find()) _display.Text(cinema);
        }
    }
}