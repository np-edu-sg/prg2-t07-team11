using System;
using System.Collections;
using System.Collections.Generic;
using Core.Repository;
using Moq;
using Xunit;

namespace Core.Tests.UseCases
{
    public class Screening
    {
        public Screening()
        {
            MovieRepositoryMock = new Mock<IMovie>();
            CinemaRepositoryMock = new Mock<ICinema>();
            ScreeningRepositoryMock = new Mock<IScreening>();
        }

        public Mock<IMovie> MovieRepositoryMock { get; set; }
        public Mock<ICinema> CinemaRepositoryMock { get; set; }
        public Mock<IScreening> ScreeningRepositoryMock { get; set; }

        [Fact]
        public void Constructor_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() =>
                new Core.UseCases.Screening(ScreeningRepositoryMock.Object, CinemaRepositoryMock.Object,
                    MovieRepositoryMock.Object)));
        }

        [Fact]
        public void LoadData_CallsRepositoryInit()
        {
            var screening = new Core.UseCases.Screening(ScreeningRepositoryMock.Object, CinemaRepositoryMock.Object,
                MovieRepositoryMock.Object);
            screening.LoadData();

            ScreeningRepositoryMock.Verify(c => c.Init(), Times.Once());
        }

        [Fact]
        public void FindAllByMovieTitle_CallsRepositoryFindAllByMovieTitle()
        {
            const string title = "movieeeeeeeeeee";
            var screening = new Core.UseCases.Screening(ScreeningRepositoryMock.Object, CinemaRepositoryMock.Object,
                MovieRepositoryMock.Object);
            screening.FindAllByMovieTitle(title);

            ScreeningRepositoryMock.Verify(c => c.FindAllByMovieTitle(title), Times.Once());
        }

        [Theory]
        [InlineData("1d")]
        [InlineData("2d")]
        [InlineData("3d")]
        [InlineData("4d")]
        [InlineData("5d")]
        public void Add_InvalidScreeningType_Throws(string type)
        {
            Assert.NotNull(Record.Exception(() =>
            {
                var screening = new Core.UseCases.Screening(ScreeningRepositoryMock.Object, CinemaRepositoryMock.Object,
                    MovieRepositoryMock.Object);
                screening.Add(DateTime.Now, type, "", 0, "");
            }));
        }

        [Fact]
        public void Add_InvalidCinema_Throws()
        {
            var usecase = new Core.UseCases.Screening(ScreeningRepositoryMock.Object, CinemaRepositoryMock.Object,
                MovieRepositoryMock.Object);

            var screening = Fixtures.Screenings[0];
            Assert.NotNull(Record.Exception(() =>
            {
                usecase.Add(screening.ScreeningDateTime, screening.ScreeningType, screening.Cinema.Name,
                    screening.Cinema.HallNo, screening.Movie.Title);
            }));
        }

        [Fact]
        public void FindAllWithoutTickets_CallsRepositoryFindAllWithoutTickets()
        {
            var usecase = new Core.UseCases.Screening(ScreeningRepositoryMock.Object, CinemaRepositoryMock.Object,
                MovieRepositoryMock.Object);
            usecase.FindAllWithoutTickets();
            ScreeningRepositoryMock.Verify(s => s.FindAllWithoutTickets(), Times.Once());
        }
        
        [Fact]
        public void Remove_CallsRepositoryRemove()
        {
            var usecase = new Core.UseCases.Screening(ScreeningRepositoryMock.Object, CinemaRepositoryMock.Object,
                MovieRepositoryMock.Object);
            usecase.Remove(Fixtures.Screenings[0]);
            
            ScreeningRepositoryMock.Verify(s => s.Remove(Fixtures.Screenings[0]), Times.Once());
        }
    }
}