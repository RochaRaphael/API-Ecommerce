namespace API_Ecommerce.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? VerificationKey { get; set; }
        public string? LastToken { get; set; }
        public bool? IsChecked { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string Salt { get; set; }

        public List<Order> Orders { get; set; }
    }
}
