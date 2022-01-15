namespace Core.Models
{
    public class Student : Ticket
    {
        public Student()
        {
        }

        public Student(Screening screening, string levelOfStudy) : base(screening)
        {
            LevelOfStudy = levelOfStudy;
        }

        public string LevelOfStudy { get; set; }
    }
}