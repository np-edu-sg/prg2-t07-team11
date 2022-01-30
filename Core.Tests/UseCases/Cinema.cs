//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using Core.Repository;
using Moq;
using Xunit;

namespace Core.Tests.UseCases
{
    public class Cinema
    {
        public Cinema()
        {
            CinemaRepositoryMock = new Mock<ICinema>();
        }

        public Mock<ICinema> CinemaRepositoryMock { get; set; }

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

            CinemaRepositoryMock.Verify(c => c.Init(), Times.Once());
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