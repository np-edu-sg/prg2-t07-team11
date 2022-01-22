using System;

namespace Core.Models
{
    public class Screening
    {
        public int ScreeningNo { get; set; }
        public DateTime ScreeningDateTime { get; set; }
        public string ScreeningType { get; set; }
        public int SeatsRemaining { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }

        public static string Header = $"{"Movie Title",-30}{"Cinema",-15}{"Screening Type",-10}{"Date and Time",-25}";

        public Screening()
        {
        }

        public Screening(int screeningNo, DateTime screeningDateTime, string screeningType, Cinema cinema, Movie movie)
        {
            ScreeningNo = screeningNo;
            ScreeningDateTime = screeningDateTime;
            ScreeningType = screeningType;
            Cinema = cinema;
            Movie = movie;
        }

        public override string ToString()
        {
            return $"{Movie.Title, -30}{Cinema.Name, -15}{ScreeningType, -10}{ScreeningDateTime, -25}";
        }
    }
}