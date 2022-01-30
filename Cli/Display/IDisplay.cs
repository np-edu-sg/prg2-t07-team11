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
        public void Table<T>(List<T> list, string header);


        public T Input<T>(string message, string error, Predicate<string> validator);
        public T Input<T>(string message);

        /// <summary>
        /// MenuInput should show a menu with option 0 as exit.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="message"></param>
        /// <param name="error"></param>
        /// <returns>An integer representing the selected index of <param name="items"></param></returns>
        public int MenuInput(List<string> items, string message, string error);

        public int InteractiveTableInput<T>(List<T> list, string header);
    }
}