using System;
using System.Threading.Tasks;

namespace Cli.Display
{
    public class InteractiveDisplay : IDisplay
    {
        public InteractiveDisplay()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            Console.Clear();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public void Text(string s)
        {
            Console.WriteLine(s);
        }

        public void Text(object s)
        {
            Text(s.ToString());
        }

        public void RootCommand(RootCommand command)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(0, 3);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public void Command(Command command)
        {
            throw new NotImplementedException();
        }
    }
}