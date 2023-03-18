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

        public async Task<User> GetByIdAsync(int id)
        {
            return await context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<User> GetByLoginAsync(string login)
        {
            return await context
                 .Users
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<User> GetByIdToRegisterAsync(int id)
        {
            return await context
                .Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == id);

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
            return await context
                 .Users
                 .Include(x => x.Roles)
                 .FirstOrDefaultAsync(x => x.Login == model.Login && x.Password == model.Password);
        }

        public async Task InsertLastTokenAsync(User user)
        {
                context.Users.Update(user);
                await context.SaveChangesAsync();
        }
        public async Task UpdateUserAsync(NewUserViewModel updateUser, int userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Name = updateUser.Name;
                user.Email = updateUser.Email;

                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
        }
        public async Task<bool> DeleteUserAsync(User user)
        {
            try
            {
                user.Deleted = true;
                user.Active = false;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        

    }
}
