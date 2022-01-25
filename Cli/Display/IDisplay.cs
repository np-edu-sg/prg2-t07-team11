using System;
using System.Collections.Generic;

namespace Cli.Display
{
    public interface IDisplay
    {
        public void Clear();
        public void Text(object s);
        public void Error(object s);
        public void Header(object s);


        public T Input<T>(string message, string error, Predicate<string> validator);
        public T Input<T>(string message);
        public int MenuInput(List<string> items, string message, string error);

        public int InteractiveTableInput<T>(List<T> list, string header);
    }
}