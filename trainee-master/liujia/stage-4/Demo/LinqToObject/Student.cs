namespace LinqToObject
{
    public class Student
    {
        public string Id { get; set; }
        public double English { get; set; }
        public double Computer { get; set; }
        public double Math { get; set; }
        public double Total => English + Computer + Math;
        public double Average => Total / 3;
    }
}