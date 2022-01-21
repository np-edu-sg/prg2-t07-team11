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
            return $"{ScreeningNo,-5}{ScreeningDateTime,-30}{ScreeningType,-20}{Cinema.Name,-15}{Movie.Title}";
        }
    }
}