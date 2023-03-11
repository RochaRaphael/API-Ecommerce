namespace API_Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }


        public IList<Order> Orders { get; set; }
    }
}
