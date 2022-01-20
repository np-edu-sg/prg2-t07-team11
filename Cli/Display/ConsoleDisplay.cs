using System;
using System.Collections.Generic;

namespace Cli.Display
{
    public class ConsoleDisplay : IDisplay
    {
        public void Text(object s)
        {
            Console.WriteLine(s);
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

        public int Menu(List<string> items, string message, string error)
        {
            for (var idx = 0; idx < items.Count; idx++) Console.WriteLine($"[{idx + 1}] {items[idx]}");

            int input;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out input) && -1 < input && input < items.Count) break;
                Console.WriteLine(error);
            }

            return input;
        }
    }
}