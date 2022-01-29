using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Models;
using Xunit;

namespace Core.Tests.Models
{
    public class Adult
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
            Assert.Null(Record.Exception(() => new Core.Models.Adult()));
        }

        [Fact]
        public void Constructor_Overload_WithPopcorn_Valid()
        {
            var adult = new Core.Models.Adult(new Screening(), true);
            Assert.NotNull(adult);
            Assert.True(adult.PopcornOffer);
        }

        [Fact]
        public void Constructor_Overload_NoPopcorn_Valid()
        {
            var adult = new Core.Models.Adult(new Screening(), false);
            Assert.NotNull(adult);
            Assert.False(adult.PopcornOffer);
        }

        [Theory, MemberData(nameof(CalculatePrice_MonThurs))]
        public void CalculatePrice_2D_MonThurs_NoPopcorn_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "2D"
            };

            var adult = new Core.Models.Adult(screening, false);

            Assert.Equal(8.5, adult.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_MonThurs))]
        public void CalculatePrice_2D_MonThurs_WithPopcorn_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "2D"
            };

            var adult = new Core.Models.Adult(screening, true);

            Assert.Equal(11.5, adult.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_FriSun))]
        public void CalculatePrice_2D_FriSun_NoPopcorn_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "2D"
            };

            var adult = new Core.Models.Adult(screening, false);

            Assert.Equal(12.5, adult.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_FriSun))]
        public void CalculatePrice_2D_FriSun_WithPopcorn_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "2D"
            };

            var adult = new Core.Models.Adult(screening, true);

            Assert.Equal(15.5, adult.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_MonThurs))]
        public void CalculatePrice_3D_MonThurs_NoPopcorn_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "3D"
            };

            var adult = new Core.Models.Adult(screening, false);

            Assert.Equal(11, adult.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_MonThurs))]
        public void CalculatePrice_3D_MonThurs_WithPopcorn_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "3D"
            };

            var adult = new Core.Models.Adult(screening, true);

            Assert.Equal(14, adult.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_FriSun))]
        public void CalculatePrice_3D_FriSun_NoPopcorn_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "3D"
            };

            var adult = new Core.Models.Adult(screening, false);

            Assert.Equal(14, adult.CalculatePrice());
        }

        [Theory, MemberData(nameof(CalculatePrice_FriSun))]
        public void CalculatePrice_3D_FriSun_WithPopcorn_Valid(DateTime dateTime)
        {
            var screening = new Screening
            {
                ScreeningDateTime = dateTime,
                ScreeningType = "3D"
            };

            var adult = new Core.Models.Adult(screening, true);

            Assert.Equal(17, adult.CalculatePrice());
        }

        [Fact]
        public void CalculatePrice_InvalidScreeningType()
        {
            var screening = new Screening
            {
                ScreeningDateTime = DateTime.Now,
                ScreeningType = "abc"
            };

            var adult = new Core.Models.Adult(screening, true);

            Assert.NotNull(Record.Exception(() => adult.CalculatePrice()));
        }
    }
}