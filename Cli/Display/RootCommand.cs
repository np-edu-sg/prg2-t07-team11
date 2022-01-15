using System.Collections.Generic;

namespace Cli.Display
{
    public class RootCommand
    {
        public string Name { get; set; }
        public List<Command> Commands { get; set; } = new();

        public RootCommand(string name, params Command[] commands)
        {
            Name = name;
            foreach (var command in commands)
            {
                Commands.Add(command);
            }
        }

        public void Add(Command command) => Commands.Add(command);
    }
}