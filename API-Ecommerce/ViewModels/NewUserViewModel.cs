using System.ComponentModel.DataAnnotations;

namespace API_Ecommerce.ViewModels
{
    public class NewUserViewModel
    {
        [Required(ErrorMessage = "Enter the name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Enter the email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter the senha")]
        public string Password { get; set; }

        public string? Salt { get; set; }
    }
}
