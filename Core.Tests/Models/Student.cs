using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Models;
using Xunit;

namespace Core.Tests.Models
{
    public class Student
    {
        public static IEnumerable<object[]> CalculatePrice_MonThurs = new[]
        {
            new object[] {DateTime.Parse("24/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("25/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("26/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("27/1/2022", new CultureInfo("en-SG"))},
        };

        public static IEnumerable<object[]> CalculatePrice_FriSun = new[]
        {
            new object[] {DateTime.Parse("28/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("29/1/2022", new CultureInfo("en-SG"))},
            new object[] {DateTime.Parse("30/1/2022", new CultureInfo("en-SG"))},
        };


        [Fact]
        public void Constructor_Default_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.Models.Student()));
        }

        [Fact]
        public void Constructor_Overload_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.Models.Student(new Screening(), "")));
        }

        [Theory, MemberData(nameof(CalculatePrice_MonThurs))]
        public void CalculatePrice_2D_MonThurs_Within7Days_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "2D",
                Movie = new Core.Models.Movie
                {
                    OpeningDate = dateTime
                }
            };

            var student = new Core.Models.Student(screening, "");

            Assert.Equal(8.5, student.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_MonThurs))]
        public void CalculatePrice_2D_MonThurs_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime.AddDays(14),
                ScreeningType = "2D",
                Movie = new Core.Models.Movie
                {
                    OpeningDate = dateTime
                }
            };

            var student = new Core.Models.Student(screening, "");

            Assert.Equal(7, student.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_FriSun))]
        public void CalculatePrice_2D_FriSun_Within7Days_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "2D",
                Movie = new Core.Models.Movie
                {
                    OpeningDate = dateTime
                }
            };

            var student = new Core.Models.Student(screening, "");

            Assert.Equal(12.5, student.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_FriSun))]
        public void CalculatePrice_2D_FriSun_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime.AddDays(14),
                ScreeningType = "2D",
                Movie = new Core.Models.Movie
                {
                    OpeningDate = dateTime
                }
            };

            var student = new Core.Models.Student(screening, "");

            Assert.Equal(12.5, student.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_MonThurs))]
        public void CalculatePrice_3D_MonThurs_Within7Days_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "3D",
                Movie = new Core.Models.Movie
                {
                    OpeningDate = dateTime
                }
            };

            var student = new Core.Models.Student(screening, "");

            Assert.Equal(11, student.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_MonThurs))]
        public void CalculatePrice_3D_MonThurs_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime.AddDays(14),
                ScreeningType = "3D",
                Movie = new Core.Models.Movie
                {
                    OpeningDate = dateTime
                }
            };

            var student = new Core.Models.Student(screening, "");

            Assert.Equal(8, student.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_FriSun))]
        public void CalculatePrice_3D_FriSun_Within7Days_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "3D",
                Movie = new Core.Models.Movie
                {
                    OpeningDate = dateTime
                }
            };

            var student = new Core.Models.Student(screening, "");

            Assert.Equal(14, student.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_FriSun))]
        public void CalculatePrice_3D_FriSun_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "3D",
                Movie = new Core.Models.Movie
                {
                    OpeningDate = dateTime
                }
            };

            var student = new Core.Models.Student(screening, "");

            Assert.Equal(14, student.CalculatePrice());
        }

        [Fact]
        public void CalculatePrice_InvalidScreeningType()
        {
            var screening = new Screening
            {
                ScreeningDateTime = DateTime.Now,
                ScreeningType = "abc"
            };

            var student = new Core.Models.Student(screening, "");

            Assert.NotNull(Record.Exception(() => student.CalculatePrice()));
        }
    }
}