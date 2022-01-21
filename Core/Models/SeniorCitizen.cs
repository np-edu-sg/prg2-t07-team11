namespace Core.Models
{
    public class SeniorCitizen : Ticket
    {
        public int YearOfBirth { get; set; }

        public SeniorCitizen()
        {
        }

        public SeniorCitizen(Screening screening, int yearOfBirth) : base(screening)
        {
            YearOfBirth = yearOfBirth;
        }

        public override double CalculatePrice()
        {
            var day = Screening.ScreeningDateTime.GetDayOfWeek();

            if (Screening.ScreeningDateTime - Screening.Movie.OpeningDate <= new System.TimeSpan(7, 0, 0, 0))
            {
                if (Screening.ScreeningType == "3D")
                {
                    switch (day)
                    {
                        case DayOfWeek.MonToThurs:
                            return 11;
                        case DayOfWeek.FriToSun:
                            return 14;
                    }
                }

                if (Screening.ScreeningType == "2D")
                {
                    switch (day)
                    {
                        case DayOfWeek.MonToThurs:
                            return 8.50;
                        case DayOfWeek.FriToSun:
                            return 12.50;
                    }
                }
            }

            if (Screening.ScreeningType == "3D")
            {
                switch (day)
                {
                    case DayOfWeek.MonToThurs:
                        return 5;
                    case DayOfWeek.FriToSun:
                        return 14;
                }
            }

            if (Screening.ScreeningType == "2D")
            {
                switch (day)
                {
                    case DayOfWeek.MonToThurs:
                        return 7;
                    case DayOfWeek.FriToSun:
                        return 12.50;
                }
            }

            throw new System.Exception("You should not be here");
        }
    }
}