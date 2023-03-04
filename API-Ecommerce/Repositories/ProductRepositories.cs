using API_Ecommerce.Data;
using API_Ecommerce.Models;
using API_Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Repositories
{
    public class ProductRepositories
    {
        private readonly DataContext context;

        public ProductRepositories(DataContext context)
        {
            this.context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await context
                .Products
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetProductByNameAsync(string name)
        {
            var product = await context
                .Products
                .FirstOrDefaultAsync(x => x.Name == name);

            return product.Id;
        }
        public async Task RegisterProductAsync(Product newProduct)
        {
            await context.Products.AddAsync(newProduct);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ProductExistsAsync(string name)
        {
            return await Task.FromResult(context.Products.Any(x => x.Name == name));
        }

        public async Task UpdateProductAsync(NewProductViewModel updateProduct, int productId)
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
