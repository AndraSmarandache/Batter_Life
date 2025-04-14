namespace BatterLife.Models
{
    namespace BatterLife.Models
    {
        public class Cart
        {
            public int Id { get; set; }
            public string SessionId { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        }
    }
}
