using System;
using System.Collections.Generic;

namespace Cli.Display
{
    public class Command
    {
        public string Name { get; set; }
        public Action Handler { get; set; }
        public List<Command> Children { get; set; } = new();

        public Command(string name) => Name = name;

        public Command(string name, Action handler) => (Name, Handler) = (name, handler);

        public void AddCommand(params Command[] children) => Children.AddRange(children);
    }
}