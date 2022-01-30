//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;

namespace Core.Models
{
    public class SeniorCitizen : Ticket
    {
        public SeniorCitizen()
        {
        }

        public SeniorCitizen(Screening screening, int yearOfBirth) : base(screening)
        {
            YearOfBirth = yearOfBirth;
        }

        public int YearOfBirth { get; set; }

        public override double CalculatePrice()
        {
            var day = Screening.ScreeningDateTime.GetDayOfWeek();

            if (Screening.ScreeningDateTime - Screening.Movie.OpeningDate <= new TimeSpan(7, 0, 0, 0))
            {
                if (Screening.ScreeningType == "3D")
                    switch (day)
                    {
                        case DayOfWeek.MonToThurs:
                            return 11;
                        case DayOfWeek.FriToSun:
                            return 14;
                    }

                if (Screening.ScreeningType == "2D")
                    switch (day)
                    {
                        case DayOfWeek.MonToThurs:
                            return 8.50;
                        case DayOfWeek.FriToSun:
                            return 12.50;
                    }
            }

            if (Screening.ScreeningType == "3D")
                switch (day)
                {
                    case DayOfWeek.MonToThurs:
                        return 5;
                    case DayOfWeek.FriToSun:
                        return 14;
                }

            if (Screening.ScreeningType == "2D")
                switch (day)
                {
                    case DayOfWeek.MonToThurs:
                        return 7;
                    case DayOfWeek.FriToSun:
                        return 12.50;
                }

            throw new Exception("You should not be here");
        }
    }
}