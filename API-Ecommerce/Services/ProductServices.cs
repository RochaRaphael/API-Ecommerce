using API_Ecommerce.Models;
using API_Ecommerce.Repositories;
using API_Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Services
{
    public class ProductServices
    {
        private readonly ProductRepositories productRepositories;
        private readonly CategoryRepositories categoryRepositories;
        private readonly CategoryServices categoryServices;
        public ProductServices(ProductRepositories productRepositories, CategoryRepositories categoryRepositories, CategoryServices categoryServices)
        {
            this.productRepositories = productRepositories;
            this.categoryRepositories = categoryRepositories;
            this.categoryServices = categoryServices;
        }



        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                var user = await productRepositories.GetByIdAsync(id);
                if (user == null)
                    return null;

                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResultViewModel<NewProductViewModel>> RegisterProductAsync(NewProductViewModel product)
        {
            try
            {
                if(await productRepositories.ProductExistsAsync(product.Name) == false)
                    return new ResultViewModel<NewProductViewModel>("This product already exists");

                var productCategory = await categoryRepositories.GetByNameAsync(product.Category);
                if (productCategory == null)
                {
                    await categoryServices.RegisterCategoryAsync(product.Category);
                    productCategory = await categoryRepositories.GetByNameAsync(product.Category);
                }
                    
                var newProduct = new Product
                {
                    Name = product.Name,
                    Url = $"/product/{product.Name}/",
                    Quantity = product.Quantity,
                    Active = true,
                    Deleted = false
                };
                await productRepositories.RegisterProductAsync(newProduct);
                return new ResultViewModel<NewProductViewModel>(product);
            }
            catch (DbUpdateException)
            {
                return new ResultViewModel<NewProductViewModel>("00X53 - Server failure");
            }
        }
    }
}
