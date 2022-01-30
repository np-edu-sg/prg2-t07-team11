//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


using System.Collections.Generic;
using Core.Models;

namespace Core.Repository
{
    public interface IScreeningReader
    {
        public Screening FindByNo(int no);
        public List<Screening> FindAll();
        public List<Screening> FindAllByMovieTitle(string name);

        public List<Screening> FindAllByCinema(Cinema cinema);
        public List<Screening> FindAllWithoutTickets();
    }

    public interface IScreeningWriter
    {
        public void Add(Screening screening);
        public void UpdateSeatsRemaining(int no, int seatsRemaining);
        public void Remove(Screening screening);

        public void AddTicket(Ticket ticket);
    }

    public interface IScreening : IScreeningReader, IScreeningWriter
    {
        public void Init();
    }
}