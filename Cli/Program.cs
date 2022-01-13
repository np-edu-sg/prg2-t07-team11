using System;
using System.Collections.Generic;
using System.IO;
using Cli.Models;

namespace Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public void ListAllMovies(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                Console.WriteLine(movie);
            }
        }
    }
}
