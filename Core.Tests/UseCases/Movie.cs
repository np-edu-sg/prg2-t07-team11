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
    public class Movie
    {
        public Movie()
        {
            MovieRepositoryMock = new Mock<IMovie>();
            OrderRepositoryMock = new Mock<IOrder>();
        }

        public Mock<IMovie> MovieRepositoryMock { get; set; }
        public Mock<IOrder> OrderRepositoryMock { get; set; }

        [Fact]
        public void Constructor_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() =>
                new Core.UseCases.Movie(MovieRepositoryMock.Object, OrderRepositoryMock.Object)));
        }

        [Fact]
        public void LoadData_CallsRepositoryInit()
        {
            var movie = new Core.UseCases.Movie(MovieRepositoryMock.Object, OrderRepositoryMock.Object);
            movie.LoadData();

            MovieRepositoryMock.Verify(c => c.Init(), Times.Once());
        }

        [Fact]
        public void FindAll_CallsRepositoryFindAll()
        {
            var movie = new Core.UseCases.Movie(MovieRepositoryMock.Object, OrderRepositoryMock.Object);
            movie.FindAll();

            MovieRepositoryMock.Verify(c => c.FindAll(), Times.Once());
        }
    }
}