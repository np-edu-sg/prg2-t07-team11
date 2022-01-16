namespace Core.Models
{
    public class SeniorCitizen : Ticket
    {
        public int YearOfBirth { get; set; }
        public SeniorCitizen()
        {
        }

        public SeniorCitizen(Screening screening, int yearOfBirth) : base(screening)
        {
            YearOfBirth = yearOfBirth;
        }
    }
}