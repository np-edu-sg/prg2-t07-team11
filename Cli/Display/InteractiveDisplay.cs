using System;

namespace Cli.Display
{
    public class InteractiveDisplay : IDisplay
    {
        private const ConsoleColor BackgroundColor = ConsoleColor.Black;
        private const ConsoleColor ForegroundColor = ConsoleColor.White;

        private Screen _current;

        public InteractiveDisplay(Window window)
        {
            window.ResizeEvent += OnResize;
            Console.Clear();
        }

        private void OnResize(object caller, ResizeEventArgs resizeEventArgs)
        {
            if (_current is not null) Mount(_current);
        }

        public void Line()
        {
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public T Input<T>(string message, string error, Predicate<string> validator)
        {
            string input;
            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (validator(input)) break;
                Console.WriteLine(error);
            }

            return (T)Convert.ChangeType(input, typeof(T));
        }

        public T Input<T>(string message)
        {
            Console.Write(message);
            return (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
        }

        public void Text(string s)
        {
            Console.WriteLine(s);
        }

        public void Text(object s)
        {
            Text(s.ToString());
        }

        public void Mount(Screen screen)
        {
            if (screen.GetType() != _current?.GetType()) _current = screen;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            screen.Mount();
        }

        public void Header(string s)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            Line();
            var pad = "";
            for (var i = 0; i < Console.WindowWidth / 2 - s.Length / 2; i++) pad += " ";

            var right = "";
            for (var i = 0; i < Console.WindowWidth - pad.Length - s.Length; i++) right += " ";
            
            Console.Write(pad + s + right);
            Line();

            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
        }

        [Obsolete]
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