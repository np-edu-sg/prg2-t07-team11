using System;

namespace Cli.Models
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

        public Screening(int screeningNo, DateTime screeningDateTime, string screeningType, int seatsRemaining,
            Cinema cinema, Movie movie)
        {
            ScreeningNo = screeningNo;
            ScreeningDateTime = screeningDateTime;
            ScreeningType = screeningType;
            SeatsRemaining = seatsRemaining;
            Cinema = cinema;
            Movie = movie;
        }
    }
}