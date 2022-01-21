namespace Core.Models
{
    public class Student : Ticket
    {
        public string LevelOfStudy { get; set; }

        public Student()
        {
        }

        public Student(Screening screening, string levelOfStudy) : base(screening)
        {
            LevelOfStudy = levelOfStudy;
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
                        return 8;
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