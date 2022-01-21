﻿namespace Core.Models
{
    public class Adult : Ticket
    {
        public bool PopcornOffer { get; set; }

        public Adult()
        {
        }

        public Adult(Screening screening, bool popcornOffer) : base(screening)
        {
            PopcornOffer = popcornOffer;
        }

        public override double CalculatePrice()
        {
            var day = Screening.ScreeningDateTime.GetDayOfWeek();

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

            throw new System.Exception("You should not be here");
        }
    }
}