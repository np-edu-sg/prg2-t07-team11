namespace Core.Models
{
    public class Adult : Ticket
    {
        public Adult()
        {
        }

        public Adult(Screening screening, bool popcornOffer) : base(screening)
        {
            PopcornOffer = popcornOffer;
        }

        public bool PopcornOffer { get; set; }
    }
}