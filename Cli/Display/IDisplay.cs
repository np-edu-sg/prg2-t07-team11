using System;

namespace Cli.Display
{
    public interface IDisplay
    {
        public ConsoleKeyInfo ReadKey();
        public void Text(string s);
        public void Text(object s);
        public void RootCommand(RootCommand command);
        public void Command(Command command);
    }
}