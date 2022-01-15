namespace Core.Models
{
    public class Ticket
    {
        public Ticket()
        {
        }

        public Ticket(Screening screening)
        {
            Screening = screening;
        }

        public Screening Screening { get; set; }

        public double CalculatePrice()
        {
            return 0;
        }
    }
}