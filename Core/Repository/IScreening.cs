using System.Collections.Generic;
using Core.Models;

namespace Core.Repository
{
    public interface IScreeningReader
    {
        public List<Screening> Find();

        public List<Screening> FindByCinema(Cinema cinema);
        public List<Screening> FindByTicketCount(int count);
    }

    public interface IScreeningWriter
    {
        public void Add(Screening screening);
        public void AddTicket(Ticket ticket);
    }

    public interface IScreening : IScreeningReader, IScreeningWriter
    {
        public void Init();
    }
}