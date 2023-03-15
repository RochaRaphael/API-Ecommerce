namespace API_Ecommerce.Models
{
    public class ItemOrder
    {
        public int Id { get; set; }
        public Order order { get; set; }
        public Product product { get; set; }
        public int Quantity { get; set; }
    }
}
