using API_Ecommerce.Data;
using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Repositories
{
    public class CategoryRepositories
    {
        private readonly DataContext context;

        public CategoryRepositories(DataContext context)
        {
            this.context = context;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RegisterCategoryAsync(Category newCategory)
        {
            await context.Categories.AddAsync(newCategory);
            await context.SaveChangesAsync();
        }
        public async Task<bool> CategoryExistsAsync(string name)
        {
            return await Task.FromResult(context.Categories.Any(x => x.Name == name));
        }
        public async Task<Category> GetByNameAsync(string name)
        {
            return await context
                .Categories
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task UpdateCategoryAsync( updatePr, int productId)
        {
            var product = await context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Name = updateProduct.Name;
                product.Quantity = updateProduct.Quantity;

                context.Products.Update(product);
                await context.SaveChangesAsync();
            }
        }
    }
}
