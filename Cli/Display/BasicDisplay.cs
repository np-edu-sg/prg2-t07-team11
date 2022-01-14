﻿using System;

namespace Cli.Display
{
    public class BasicDisplay : IDisplay
    {
        public BasicDisplay()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            Console.Clear();
        }

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