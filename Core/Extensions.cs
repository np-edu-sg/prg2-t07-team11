using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum DayOfWeek
    {
        MonToThurs,
        FriToSun,
    }

    public static class Extension
    {
        public static DayOfWeek GetDayOfWeek(this DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case System.DayOfWeek.Monday:
                case System.DayOfWeek.Tuesday:
                case System.DayOfWeek.Wednesday:
                case System.DayOfWeek.Thursday:
                    return DayOfWeek.MonToThurs;
                case System.DayOfWeek.Friday:
                case System.DayOfWeek.Saturday:
                case System.DayOfWeek.Sunday:
                    return DayOfWeek.FriToSun;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}