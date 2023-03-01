using API_Ecommerce.Data;
using API_Ecommerce.Models;
using API_Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> UserExistsAsync(string login)
        {
            return await Task.FromResult(context.Users.Any(x => x.Login == login));
        }

        public async Task<User> CorrectLoginAsync(LoginViewModel model)
        {
            var login = await context
                 .Users
                 .FirstOrDefaultAsync(x => x.Login == model.Login && x.Password == model.Password);

            return login;
        }

        public async Task<bool> InsertLasTokenAsync(User login)
        {
            try
            {
                //context.Users.Update(login);
                //await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
