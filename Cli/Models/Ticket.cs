namespace Cli.Models
{
    public class Ticket
    {
        public Screening Screening { get; set; }

        public Ticket()
        {
        }

        public Ticket(Screening screening)
        {
            Screening = screening;
        }

        public double CalculatePrice()
        {
            return 0;
        }
    }
}