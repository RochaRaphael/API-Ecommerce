using API_Ecommerce.Data;
using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Repositories
{
    public class RoleRepositories
    {
        private readonly DataContext context;

        public RoleRepositories(DataContext context)
        {
            this.context = context;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await context
                .Roles
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RegisterRoleAsync(Role newRole)
        {
            await context.Roles.AddAsync(newRole);
            await context.SaveChangesAsync();
        }

        public async Task RegisterRoleListAsync(List<Role> roles)
        {
            foreach (Role role in roles)
            {
                await context.Roles.AddAsync(role);
            }
            await context.SaveChangesAsync();
        }
        public async Task<bool> RoleExistsAsync(string name)
        {
            return await Task.FromResult(context.Roles.Any(x => x.Name == name));
        }
        public async Task<List<Role>> GetListByNamesAsync(List<string> name)
        {
            return await context
                    .Roles
                    .Where(x => name.Contains(x.Name))
                    .ToListAsync();
        }

    }
}
