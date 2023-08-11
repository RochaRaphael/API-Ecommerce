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


        public async Task<ResultViewModel<Category>> RegisterCategoryAsync(string name)
        {
            try
            {
                if (await categoryRepositories.CategoryExistsAsync(name))
                    return new ResultViewModel<Category>("This category already exists");

                var newCategory = new Category
                {
                    Name = name,
                    Url = $"/category/{name}/",
                    Active = true,
                    Deleted = false
                };
                await categoryRepositories.RegisterCategoryAsync(newCategory);

                return new ResultViewModel<Category>(newCategory);

            }
            catch (DbUpdateException)
            {
                return new ResultViewModel<Category>("44X85 - Server failure");
            }
        }

        public async Task<ResultViewModel<Category>> UpdateCategoryAsync(string categoryName, int id)
        {
            try
            {
                var category = await categoryRepositories.GetByIdAsync(id);
                if (category == null)
                    return new ResultViewModel<Category>("90X88 - Product not found");

                await categoryRepositories.UpdateCategoryAsync(categoryName, id);
                return new ResultViewModel<Category>(categoryName);
            }
            catch
            {
                return new ResultViewModel<Category>("21X61 - Server failure");
            }
        }

        public async Task<ResultViewModel<bool>> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await categoryRepositories.GetByIdAsync(id);
                if (category == null)
                    return new ResultViewModel<bool>(false, new List<string> { "Category not found" });

                var categoryDeleted = await categoryRepositories.DeleteCategoryAsync(category);
                if (categoryDeleted)
                    return new ResultViewModel<bool>(true);
                else
                    return new ResultViewModel<bool>(false, new List<string> { "44X73 - Server failure" });
            }
            catch
            {
                return new ResultViewModel<bool>(false, new List<string> { "20X10 - Error" });
            }
        }
    }
}
