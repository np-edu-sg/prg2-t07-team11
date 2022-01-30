//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;

namespace Core.Models
{
    public class Adult : Ticket
    {
        public Adult()
        {
        }

        public Adult(Screening screening, bool popcornOffer) : base(screening)
        {
            PopcornOffer = popcornOffer;
        }

        public bool PopcornOffer { get; set; }

        public override double CalculatePrice()
        {
            var popcorn = PopcornOffer ? 3 : 0;
            var day = Screening.ScreeningDateTime.GetDayOfWeek();

            if (Screening.ScreeningType == "3D")
                switch (day)
                {
                    case DayOfWeek.MonToThurs:
                        return 11 + popcorn;
                    case DayOfWeek.FriToSun:
                        return 14 + popcorn;
                }

            if (Screening.ScreeningType == "2D")
                switch (day)
                {
                    case DayOfWeek.MonToThurs:
                        return 8.50 + popcorn;
                    case DayOfWeek.FriToSun:
                        return 12.50 + popcorn;
                }

            throw new Exception("You should not be here");
        }
    }
}