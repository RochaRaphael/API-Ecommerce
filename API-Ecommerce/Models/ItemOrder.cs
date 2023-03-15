namespace API_Ecommerce.Models
{
    public class ItemOrder
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
