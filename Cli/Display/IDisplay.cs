using System;

namespace Cli.Display
{
    public interface IDisplay
    {
        public ConsoleKeyInfo ReadKey();
        public void Text(string s);
    }
}