using System.Collections.Generic;
using Core.Models;

namespace Core.Repository
{
    public interface ICinemaReader
    {
        public void Init();
        public List<Cinema> FindAll();

        public Cinema FindOneByNameAndHallNo(string cinemaName, int hallNo);
    }

    public interface ICinemaWriter
    {
    }

    public interface ICinema : ICinemaReader, ICinemaWriter
    {
    }
}