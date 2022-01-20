using Core.Repository;

namespace Cli.Display
{
    public class Screening
    {
        private readonly IDisplay _display;
        private readonly Core.UseCase.Screening _screening;

        public Screening(Core.UseCase.Screening screening, IDisplay display)
        {
            _screening = screening;
            _display = display;
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
    }
}