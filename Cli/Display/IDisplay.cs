using System;
using System.Collections.Generic;

namespace Cli.Display
{
    public interface IDisplay
    {
        public ConsoleKeyInfo ReadKey();
        public T Input<T>(string message, string error, Predicate<string> validator);
        public T Input<T>(string message);
        public void Text(string s);
        public void Text(object s);
        public void Render(Screen screen);
        public void NavigateTo(Screen screen);
        public void Run(RootCommand rootCommand);
    }
}