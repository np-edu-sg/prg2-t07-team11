//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Movie
    {
        public static readonly string Header =
            $"{"Title",-30}{"Duration",-10}{"Classification",-18}{"Opening Date",-25}";

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

        public string Title { get; set; }
        public int Duration { get; set; }
        public string Classification { get; set; }
        public DateTime OpeningDate { get; set; }
        public List<Screening> ScreeningList { get; set; } = new();
        public List<string> GenreList { get; set; } = new();

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
            return $"{Title,-30}{Duration,-10}{Classification,-18}{OpeningDate,-25}";
        }
    }
}