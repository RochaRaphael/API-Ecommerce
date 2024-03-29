﻿using API_Ecommerce.Data;
using API_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Repositories
{
    public class ItemOrderRepositories
    {
        private readonly DataContext context;

        public ItemOrderRepositories(DataContext context)
        {
            this.context = context;
        }

        public async Task<ItemOrder> GetByIdAsync(int id)
        {
            return await context
                .ItemOrders
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RegisterItemOrderAsync(ItemOrder newItemOrder)
        {
            await context.ItemOrders.AddAsync(newItemOrder);
            await context.SaveChangesAsync();
        }
    }
}
