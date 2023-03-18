namespace API_Ecommerce.ViewModels
{
    public class NewOrderViewModel
    {
        public int UserID { get; set; }
        public List<NewItemOrderViewModel> Items { get; set; }
    }
}
