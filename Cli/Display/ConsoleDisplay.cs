using System;
using System.Collections.Generic;

namespace Cli.Display
{
    public class ConsoleDisplay : IDisplay
    {
        private readonly Window _window;

        public ConsoleDisplay(Window window)
        {
            _window = window;
        }

        public void Clear() => Console.Clear();
        public void Text(object s) => Console.WriteLine(s);

        public void Error(object s)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(s);
            Console.ResetColor();
        }

        public void Header(object s)
        {
            if (s is null) return;

            Console.ForegroundColor = ConsoleColor.Yellow;
            var line = "";
            for (var idx = 0; idx < s.ToString()!.Length + 2; idx++) line += "-";

            Console.WriteLine($"/{line}\\");
            Console.WriteLine($"| {s.ToString()} |");
            Console.WriteLine($"\\{line}/");
            Console.WriteLine();

            Console.ResetColor();
        }

        public void Table<T>(List<T> list, string header)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(header);

            Console.ResetColor();

            foreach (var t in list)
            {
                Console.WriteLine(t);
                Console.ResetColor();
            }

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.ResetColor();
        }

        public int InteractiveTableInput<T>(List<T> list, string header)
        {
            var width = _window.Width;
            var height = -_window.Height;

            var selected = 0;
            Console.Clear();

            while (true)
            {
                if (_window.Width != width || _window.Height != height)
                {
                    (width, height) = (_window.Width, _window.Height);
                    Console.Clear();
                }

                Console.SetCursorPosition(0, 0);

                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine(header);

                Console.ResetColor();

                for (var idx = 0; idx < list.Count; idx++)
                {
                    if (idx == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine(list[idx]);
                    Console.ResetColor();
                }

                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Press ESC or F4 to escape input");

                Console.ResetColor();

                ConsoleKeyInfo key;
                while (true)
                {
                    key = Console.ReadKey();
                    if (key.Key is ConsoleKey.UpArrow or ConsoleKey.DownArrow or ConsoleKey.F4
                        or ConsoleKey.Enter) break;
                }

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected > 0) selected--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected < list.Count - 1) selected++;
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.F4:
                        return -1;
                    case ConsoleKey.Enter:
                        return selected;
                    default:
                        throw new Exception("What?");
                }
            }
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

        public int MenuInput(List<string> items, string message, string error)
        {
            for (var idx = 0; idx < items.Count; idx++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"[{idx + 1}] ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(items[idx]);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("[0] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Exit");

            Console.ResetColor();
            Console.WriteLine();

            int input;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(message);
                Console.ForegroundColor = ConsoleColor.White;

                if (int.TryParse(Console.ReadLine(), out input) && -1 < input && input <= items.Count) break;

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(error);
                Console.ResetColor();
                Console.WriteLine();
            }

            Console.ResetColor();

            return input - 1;
        }
    }
}