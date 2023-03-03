using API_Ecommerce.Models;
using API_Ecommerce.Repositories;
using API_Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Services
{
    public class CategoryServices
    {
        private readonly CategoryRepositories categoryRepositories;
        public CategoryServices(CategoryRepositories categoryRepositories)
        {
            this.categoryRepositories = categoryRepositories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            try
            {
                var category = await categoryRepositories.GetByIdAsync(id);
                if (category == null)
                    return null;

                return category;
            }
            catch
            {
                return null;
            }
        }


        public async Task<ResponseViewModel> RegisterCategoryAsync(string name)
        {
            try
            {
                if (await categoryRepositories.CategoryExistsAsync(name))
                    return new ResponseViewModel { Success = false, Message = "This category already exists" };

                var newCategory = new Category
                {
                    Name = name,
                    Url = $"/category/{name}/",
                    Active = true,
                    Deleted = false
                };
                await categoryRepositories.RegisterCategoryAsync(newCategory);
                return new ResponseViewModel { Success = true, Message = "User successfully created" };
            }
            catch (DbUpdateException)
            {
                return new ResponseViewModel { Success = false, Message = "44X85 - Server failure" };
            }
        }
    }
}
