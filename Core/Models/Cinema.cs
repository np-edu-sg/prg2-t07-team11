//============================================================
// Student Number : S10219526, S10227463
// Student Name : Qin Guan, Richard Paul Pamintuan
// Module Group : T07
//============================================================


namespace Core.Models
{
    public class Cinema
    {
        public static readonly string Header = $"{"Name",-15}{"Hall No",-10}{"Capacity",-10}";

        public Cinema()
        {
        }

        public Cinema(string name, int hallNo, int capacity)
        {
            Name = name;
            HallNo = hallNo;
            Capacity = capacity;
        }

        public string Name { get; set; }
        public int HallNo { get; set; }
        public int Capacity { get; set; }

        public override string ToString()
        {
            return $"{Name,-15}{HallNo,-10}{Capacity,-10}";
        }
    }
}