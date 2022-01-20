using System.Collections.Generic;
using Core.Models;

namespace Core.Repository
{
    public interface IScreeningReader
    {
        public void Init();
        public List<Screening> Find();
    }

    public interface IScreeningWriter
    {
    }

    public interface IScreening : IScreeningReader, IScreeningWriter
    {
    }
}