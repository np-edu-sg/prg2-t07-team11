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
            if (dateTime.Day <= 4) return DayOfWeek.MonToThurs;
            return DayOfWeek.FriToSun;
        }
    }
}
