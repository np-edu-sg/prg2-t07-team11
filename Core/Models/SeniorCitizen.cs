namespace Core.Models
{
    public class SeniorCitizen : Ticket
    {
        public SeniorCitizen()
        {
        }

        public SeniorCitizen(Screening screening, int yearOfBirth) : base(screening)
        {
            YearOfBirth = yearOfBirth;
        }

        public int YearOfBirth { get; set; }
    }
}