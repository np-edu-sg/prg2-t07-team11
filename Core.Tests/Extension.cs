//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace Core.Tests
{
    public class Extension
    {
        public static IEnumerable<object[]> MonThursDayOfWeek = new[]
        {
            new object[] {DateTime.Parse("24/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("25/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("26/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("27/1/2022", new CultureInfo("en-SG"))}
        };

        public static IEnumerable<object[]> FriSunDayOfWeek = new[]
        {
            new object[] {DateTime.Parse("28/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("29/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("30/1/2022", new CultureInfo("en-SG"))}
        };

        [Theory]
        [MemberData(nameof(MonThursDayOfWeek))]
        public void GetDayOfWeek_MonThurs_Valid(DateTime dateTime)
        {
            Assert.Equal(DayOfWeek.MonToThurs, dateTime.GetDayOfWeek());
        }

        [Theory]
        [MemberData(nameof(FriSunDayOfWeek))]
        public void GetDayOfWeek_FriSun_Valid(DateTime dateTime)
        {
            Assert.Equal(DayOfWeek.FriToSun, dateTime.GetDayOfWeek());
        }
    }
}