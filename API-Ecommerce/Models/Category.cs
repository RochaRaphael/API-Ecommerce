namespace API_Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public List<Product> Products { get; set; }
    }
}
