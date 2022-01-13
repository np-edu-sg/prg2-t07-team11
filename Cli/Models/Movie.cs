using System;
using System.Collections.Generic;

namespace Cli.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Classification { get; set; }
        public DateTime OpeningDate { get; set; }
        public List<string> GenreList { get; set; }
        public List<string> ScreeningList { get; set; }
        
        public Movie(){}

        public Movie(string title, int duration, string classification, DateTime openingDate, List<string> screeningList)
        {
            Title = title;
            Duration = duration;
            Classification = classification;
            OpeningDate = openingDate;
            ScreeningList = screeningList;
        }

        public List<string> GetGenreList()
        {
            return GenreList;
        }
    }
}