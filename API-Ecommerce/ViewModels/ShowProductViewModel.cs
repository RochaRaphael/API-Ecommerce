using API_Ecommerce.Models;

namespace API_Ecommerce.ViewModels
{
    public class ShowProductViewModel
    {
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Quantity { get; set; }
    }
}
