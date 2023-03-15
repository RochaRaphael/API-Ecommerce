using API_Ecommerce.Data;
using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Repositories
{
    public class OrderRepositories
    {
        private readonly DataContext context;

        public OrderRepositories(DataContext context)
        {
            this.context = context;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await context
                .Orders
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RegisterOrderAsync(Order newOrder)
        {
            await context.Orders.AddAsync(newOrder);
            await context.SaveChangesAsync();
        }
    }
}
