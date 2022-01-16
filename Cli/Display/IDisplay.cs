using System;

namespace Cli.Display
{
    public interface IDisplay
    {
        public ConsoleKeyInfo ReadKey();
        public void Text(string s);
        public void Text(object s);
        public void Run(RootCommand rootCommand);
    }
}