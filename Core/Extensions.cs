//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;

namespace Core
{
    public enum DayOfWeek
    {
        MonToThurs,
        FriToSun
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