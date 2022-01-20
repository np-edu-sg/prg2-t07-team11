using System;
using System.Collections.Generic;

namespace Cli
{
    public interface IDisplay
    {
        public void Text(object s);
        public T Input<T>(string message, string error, Predicate<string> validator);
        public T Input<T>(string message);
        public int Menu(List<string> items, string message, string error);
    }
}