using System;

namespace Cli.Display
{
    public class InteractiveDisplay : IDisplay
    {
        public InteractiveDisplay(Window window)
        {
            window.ResizeEvent += OnResize;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            Console.Clear();
        }

        private void OnResize(object caller, ResizeEventArgs resizeEventArgs)
        {
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

        public void Header(string s)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            var pad = " ";
            for (var i = 0; i < Console.WindowWidth / 2 - s.Length / 2; i++) pad += " ";

            Console.WriteLine(pad + s);
        }

        public void Run(RootCommand rootCommand)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();

            Console.WriteLine();
            Header(rootCommand.Name);
            Console.WriteLine();

            Console.SetCursorPosition(0, 3);
            Console.BackgroundColor = ConsoleColor.White;
            for (var i = 0; i < Console.WindowHeight; i++) Console.WriteLine();
        }
    }
}