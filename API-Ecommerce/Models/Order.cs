namespace API_Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }

        public IList<Product> Products { get; set; }

    }
}
