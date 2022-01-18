using System;
using System.Collections.Generic;

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

        public void AddGenre(string genre)
        {
            GenreList.Add(genre);
        }

        public void AddScreening(Screening screening)
        {
            ScreeningList.Add(screening);
        }

        public override string ToString()
        {
            return $"{Title,-30}{Duration,-10}{Classification,-10}{OpeningDate,-15}{ScreeningList,-12}";
        }
    }
}