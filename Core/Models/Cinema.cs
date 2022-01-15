namespace Core.Models
{
    public class Cinema
    {
        public string Name { get; set; }
        public int HallNo { get; set; }
        public int Capacity { get; set; }

        public Cinema()
        {
        }

        public Cinema(string name, int hallNo, int capacity)
        {
            Name = name;
            HallNo = hallNo;
            Capacity = capacity;
        }

        public override string ToString()
        {
            return $"{Name,-15}{HallNo,-5}{Capacity,-5}";
        }
    }
}