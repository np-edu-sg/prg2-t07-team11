namespace Core.Models
{
    public class Student : Ticket
    {
        public string LevelOfStudy { get; set; }
        public Student()
        {
        }

        public Student(Screening screening, string levelOfStudy) : base(screening)
        {
            LevelOfStudy = levelOfStudy;
        }
    }
}