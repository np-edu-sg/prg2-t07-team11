using System;
using System.Collections.Generic;

namespace Cli.Display
{
    public class Command
    {
        public string Name { get; set; }
        public Action Handler { get; set; }

        public Command(string name) => Name = name;

        public Command(string name, Action handler) => (Name, Handler) = (name, handler);

    }
}