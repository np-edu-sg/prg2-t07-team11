using System;
using System.Collections.Generic;

namespace Cli.Display
{
    public interface IDisplay
    {
        public void Line();
        public ConsoleKeyInfo ReadKey();
        public T Input<T>(string message, string error, Predicate<string> validator);
        public T Input<T>(string message);
        public void Text(string s);
        public void Text(object s);
        public void Header(string s);
        public void Mount(Screen screen);
        [Obsolete]
        public void Run(RootCommand rootCommand);
    }
}