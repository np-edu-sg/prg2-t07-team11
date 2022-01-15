using System.Collections.Generic;
using Core.Models;

namespace Core.Repository
{
    public interface ICinemaReader
    {
        public List<Cinema> Find();

        public Cinema FindByNameAndHallNo(string cinemaName, int hallNo);
    }

    public interface ICinemaWriter
    {
    }

    public interface ICinema : ICinemaReader, ICinemaWriter
    {
    }
}