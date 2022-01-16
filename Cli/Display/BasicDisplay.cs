using System;
using System.Collections.Generic;

namespace Cli.Display
{
    public class BasicDisplay : IDisplay
    {
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

        public void Run(RootCommand rootCommand)
        {
            Console.Clear();

            var line = "";
            for (var idx = 0; idx < rootCommand.Name.Length + 2; idx++) line += "-";

            Console.WriteLine($"/{line}\\");
            Console.WriteLine($"| {rootCommand.Name} |");
            Console.WriteLine($"\\{line}/");
            Console.WriteLine();

            for (var idx = 0; idx < rootCommand.Commands.Count; idx++)
            {
                Console.WriteLine($"[{idx + 1}] {rootCommand.Commands[idx].Name}");
            }
        }
    }
}