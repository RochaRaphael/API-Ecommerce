namespace API_Ecommerce.ViewModels
{
    public class NewOrderViewModel
    {
        public int UserId { get; set; }
        public List<NewItemOrderViewModel> Items { get; set; }
    }
}
