using System;

namespace Cli.Display
{
    public class BasicDisplay : IDisplay
    {
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public void Text(string s)
        {
            Console.WriteLine(s);
        }

        public void Text(object s)
        {
            Console.WriteLine(s);
        }
    }
}