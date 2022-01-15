using System.Collections.Generic;
using Cli.Display;
using Core.Repository;

namespace Cli
{
    public class Screening
    {
        private readonly IScreening _screening;
        private readonly IDisplay _display;

        public Screening(IScreening screening, IDisplay display)
        {
            _screening = screening;
            _display = display;
        }

        public void ListAllScreenings()
        {
            foreach (var screening in _screening.Find()) _display.Text(screening);
        }
    }
}