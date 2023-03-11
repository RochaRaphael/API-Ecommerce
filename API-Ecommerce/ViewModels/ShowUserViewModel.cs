using API_Ecommerce.Models;

namespace API_Ecommerce.ViewModels
{
    public class ShowUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public List<string>? Roles { get; set; }
        public bool Deleted { get; set; }
    }
}
