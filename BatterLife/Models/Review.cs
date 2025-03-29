namespace BatterLife.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}