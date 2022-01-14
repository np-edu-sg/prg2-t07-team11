using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Core.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Classification { get; set; }
        public DateTime OpeningDate { get; set; }
        public List<Screening> ScreeningList { get; set; }
        public List<string> GenreList { get; set; }

        public Movie()
        {
        }

        public Movie(
            string title,
            int duration,
            string classification,
            DateTime openingDate,
            List<string> genreList
        )
        {
            Title = title;
            Duration = duration;
            Classification = classification;
            OpeningDate = openingDate;
            GenreList = genreList;
        }

        public List<string> GetGenreList()
        {
            return GenreList;
        }

        public void AddScreening(Screening screening)
        {
            ScreeningList.Add(screening);
        }

        public List<Screening> GetScreeningList()
        {
            return ScreeningList;
        }

        public override string ToString()
        {
            return $"{Title,-20}{Duration,-10}{Classification,-20}{OpeningDate,-15}{ScreeningList,-12}";
        }
    }
}