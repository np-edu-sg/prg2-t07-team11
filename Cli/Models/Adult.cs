namespace Cli.Models
{
    public class Adult : Ticket
    {
        public bool PopcornOffer { get; set; }

        public Adult()
        {
        }

        public Adult(Screening screening, bool popcornOffer) : base(screening)
        {
            PopcornOffer = popcornOffer;
        }
    }
}