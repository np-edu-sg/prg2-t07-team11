namespace Core.Models
{
    public abstract class Ticket
    {
        public Screening Screening { get; set; }

        public Ticket()
        {
        }

        public Ticket(Screening screening)
        {
            Screening = screening;
        }

        public abstract double CalculatePrice();

    }
}