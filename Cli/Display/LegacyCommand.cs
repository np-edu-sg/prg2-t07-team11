using System;

namespace Cli.Display
{
    [Obsolete("Switch to Cli.Display.Screen")]
    public class LegacyCommand
    {
        public string Name { get; set; }
        public Action Handler { get; set; }

        public LegacyCommand(string name) => Name = name;

        public LegacyCommand(string name, Action handler) => (Name, Handler) = (name, handler);

    }
}