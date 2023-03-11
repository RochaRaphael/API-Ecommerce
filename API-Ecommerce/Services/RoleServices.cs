using API_Ecommerce.Models;
using API_Ecommerce.Repositories;
using API_Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Services
{
    public class RoleServices
    {
        private readonly RoleRepositories roleRepositories;
        public RoleServices(RoleRepositories roleRepositories)
        {
            this.roleRepositories = roleRepositories;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            try
            {
                var role = await roleRepositories.GetByIdAsync(id);
                if (role == null)
                    return null;

                return role;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResultViewModel<bool>> RegisterRoleListAsync(List<string> roles)
        {
            try
            {
                List<Role> rolesList = new List<Role>();
                foreach (var name in roles)
                {
                    var role = new Role
                    {
                        Name = name,
                        Slug = name.ToLower().Replace(" ", "")
                    };
                    if (await roleRepositories.RoleExistsAsync(name) == false)
                        rolesList.Add(role);
                }
                
                await roleRepositories.RegisterRoleListAsync(rolesList);
                return new ResultViewModel<bool>(true);

            }
            catch (DbUpdateException)
            {
                return new ResultViewModel<bool>("77X81 - Server failure");
            }
        }
    }
}
