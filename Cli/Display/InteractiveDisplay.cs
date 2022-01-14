using System;

namespace Cli.Display
{
    public class InteractiveDisplay : IDisplay
    {
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public void Text(string s)
        {
            Console.WriteLine(s);
        }
    }
}