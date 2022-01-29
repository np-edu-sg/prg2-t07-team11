﻿using Core.Repository;
using Xunit;
using Moq;

namespace Core.Tests.UseCases
{
    public class Cinema
    {
        public Mock<ICinema> CinemaRepositoryMock { get; set; }

        public Cinema()
        {
            CinemaRepositoryMock = new Mock<ICinema>();
        }

        [Fact]
        public void Constructor_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.UseCases.Cinema(CinemaRepositoryMock.Object)));
        }

        [Fact]
        public void LoadData_CallsRepositoryInit()
        {
            var cinema = new Core.UseCases.Cinema(CinemaRepositoryMock.Object);
            cinema.LoadData();

            CinemaRepositoryMock.Verify((c) => c.Init(), Times.Once());
        }

        [Fact]
        public void FindAll_CallsRepositoryFindAll()
        {
            var cinema = new Core.UseCases.Cinema(CinemaRepositoryMock.Object);
            cinema.FindAll();

            CinemaRepositoryMock.Verify(c => c.FindAll(), Times.Once());
        }
    }
}