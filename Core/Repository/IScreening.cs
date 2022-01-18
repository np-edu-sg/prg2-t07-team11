using System.Collections.Generic;
using Core.Models;

namespace Core.Repository
{
    public interface IScreeningReader
    {
        public void Init();
        public List<Screening> Find();

        public List<Screening> FindByCinema(Cinema cinema);
    }

    public interface IScreeningWriter
    {
        public void Add(Screening screening);
    }

    public interface IScreening : IScreeningReader, IScreeningWriter
    {
    }
}