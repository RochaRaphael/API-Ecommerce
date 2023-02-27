using API_Ecommerce.Data;
using API_Ecommerce.Models;

namespace API_Ecommerce.Repositories
{
    public class UserRepositories
    {
        private readonly DataContext context;

        public UserRepositories(DataContext context)
        {
            this.context = context;
        }

        public async Task RegisterUserAsync(User newUser)
        {
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
        }
    }
}
