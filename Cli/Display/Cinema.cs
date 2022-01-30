//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


namespace Cli.Display
{
    public class Cinema
    {
        private readonly Core.UseCases.Cinema _cinema;
        private readonly IDisplay _display;

        public Cinema(Core.UseCases.Cinema cinema, IDisplay display)
        {
            _cinema = cinema;
            _display = display;
        }

        public void LoadData()
        {
            _cinema.LoadData();
            _display.Text("Loaded cinema data!");
        }

        public void ListAllCinemas()
        {
            foreach (var cinema in _cinema.FindAll()) _display.Text(cinema);
        }
    }
}