using Core.Repository;
using Xunit;
using Moq;

namespace Core.Tests.UseCases
{
    public class Movie
    {
        public Mock<IMovie> MovieRepositoryMock { get; set; }

        public Movie()
        {
            MovieRepositoryMock = new Mock<IMovie>();
        }

        [Fact]
        public void Constructor_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.UseCases.Movie(MovieRepositoryMock.Object)));
        }

        [Fact]
        public void LoadData_CallsRepositoryInit()
        {
            var movie = new Core.UseCases.Movie(MovieRepositoryMock.Object);
            movie.LoadData();

            MovieRepositoryMock.Verify((c) => c.Init(), Times.Once());
        }

        [Fact]
        public void FindAll_CallsRepositoryFindAll()
        {
            var movie = new Core.UseCases.Movie(MovieRepositoryMock.Object);
            movie.FindAll();

            MovieRepositoryMock.Verify(c => c.FindAll(), Times.Once());
        }
    }
}