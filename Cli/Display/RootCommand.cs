using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cli.Display
{
    public class RootCommand
    {
        public string Name { get; set; }
        public List<Command> Commands { get; set; } = new();

        public RootCommand(string name, params List<Command>[] commands)
        {
            Name = name;
            foreach (var command in commands)
            {
                Commands.AddRange(command);
            }
        }

        public void Add(Command command) => Commands.Add(command);
    }
}