using Xunit;

namespace Core.Tests.Models
{
    public class Cinema
    {
        [Fact]
        public void Constructor_Default_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.Models.Cinema()));
        }

        [Fact]
        public void Constructor_Overload_DoesNotThrow()
        {
            Assert.Null(Record.Exception(() => new Core.Models.Cinema("GV Bishan", 1, 100)));
        }

        [Fact]
        public void Constructor_Overload_Values_Valid()
        {
            var cinema = new Core.Models.Cinema("GV Bishan", 1, 100);
            Assert.Equal("GV Bishan", cinema.Name);
            Assert.Equal(1, cinema.HallNo);
            Assert.Equal(100, cinema.Capacity);
        }
    }
}