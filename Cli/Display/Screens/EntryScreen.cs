using System;

namespace Cli.Display.Screens
{
    public class EntryScreen : Screen
    {
        public EntryScreen(IDisplay display) : base(display)
        {
        }

        public override void Mount()
        {
            Display.Header("Movie Booking");
        }

        public override void Unmount()
        {
        }
    }
}