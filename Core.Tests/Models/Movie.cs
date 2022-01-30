//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System;
using System.Collections.Generic;
using Core.Models;
using Xunit;

namespace Core.Tests.Models
{
    public class Movie
    {
        [Fact]
        public void Constructor_Default_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.Models.Movie()));
        }

        [Fact]
        public void Constructor_Overload_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.Models.Movie()));
        }

        [Fact]
        public void Constructor_Overload_Values_Valid()
        {
            var dateTime = DateTime.Now;
            var movie = new Core.Models.Movie("Test", 100, "3D", dateTime, new List<string> { "genre" });
            Assert.Equal("Test", movie.Title);
            Assert.Equal(100, movie.Duration);
            Assert.Equal("3D", movie.Classification);
            Assert.Equal(dateTime, movie.OpeningDate);
            Assert.Equal(new List<string> { "genre" }, movie.GenreList);
            Assert.Equal(new List<Screening>(), movie.ScreeningList);
        }

        [Fact]
        public void AddGenre_Valid()
        {
            var movie = new Core.Models.Movie();

            var genre = "TEST :D";
            movie.AddGenre(genre);

            Assert.Equal(new List<string> { genre }, movie.GenreList);
        }

        [Fact]
        public void AddScreening_Valid()
        {
            var movie = new Core.Models.Movie();

            var screening = new Screening(1, DateTime.Now, "3D", new Core.Models.Cinema(),
                new Core.Models.Movie());
            movie.AddScreening(screening);

            Assert.Equal(new List<Screening> { screening }, movie.ScreeningList);
        }
    }
}