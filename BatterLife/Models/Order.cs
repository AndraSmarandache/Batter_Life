namespace BatterLife.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
    }
}
