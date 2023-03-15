using API_Ecommerce.Models;

namespace API_Ecommerce.ViewModels
{
    public class ShowOrderViewModel
    {
        public DateTime OrderDate { get; set; }

        public IList<ItemOrder> ItemOrders { get; set; }
    }
}
