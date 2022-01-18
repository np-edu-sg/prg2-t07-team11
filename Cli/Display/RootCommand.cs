using System;
using System.Collections.Generic;

namespace Cli.Display
{
    [Obsolete("Switch to Cli.Display.Screen")]
    public class RootCommand
    {
        public string Name { get; set; }
        public List<LegacyCommand> Commands { get; set; } = new();

        public RootCommand(string name, params List<LegacyCommand>[] commands)
        {
            Name = name;
            foreach (var command in commands)
            {
                Commands.AddRange(command);
            }
        }

        public void Add(LegacyCommand legacyCommand) => Commands.Add(legacyCommand);
    }
}