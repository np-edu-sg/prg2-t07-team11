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

        private void CommandChild(List<Command> children, int level)
        {
            for (var idx = 0; idx < children.Count; idx++)
            {
                var pre = "";
                for (var i = 0; i < level; i++) pre += "\t";
                Console.WriteLine(pre + $"[{idx + 1}] {children[idx].Name}");

                if (children[idx].Children is not null) CommandChild(children[idx].Children, level + 1);
            }
        }

        public void RootCommand(RootCommand command)
        {
            Console.Clear();

            var line = "";
            for (var idx = 0; idx < command.Name.Length + 2; idx++) line += "-";

            Console.WriteLine($"/{line}\\");
            Console.WriteLine($"| {command.Name} |");
            Console.WriteLine($"\\{line}/");
            Console.WriteLine();

            foreach (var child in command.Commands)
            {
                Console.WriteLine(child.Name);
                if (child.Children is not null) CommandChild(child.Children, 1);
            }
        }

        public void Command(Command command)
        {
            throw new NotImplementedException();
        }
    }
}