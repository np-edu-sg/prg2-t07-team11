//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


namespace Core.Models
{
    public abstract class Ticket
    {
        public Ticket()
        {
        }

        public Ticket(Screening screening)
        {
            Screening = screening;
        }

        public Screening Screening { get; set; }

        public abstract double CalculatePrice();
    }
}