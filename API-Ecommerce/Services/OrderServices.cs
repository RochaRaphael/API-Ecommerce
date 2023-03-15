using API_Ecommerce.Models;
using API_Ecommerce.Repositories;
using API_Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Services
{
    public class OrderServices
    {
        private readonly OrderRepositories orderRepositories;
        private readonly UserRepositories userRepositories;
        private readonly ItemOrderServices itemOrderServices;
        public OrderServices(OrderRepositories OrderRepositories, UserRepositories userRepositories, ItemOrderServices itemOrderServices)
        {
            this.orderRepositories = OrderRepositories;
            this.userRepositories = userRepositories;
            this.itemOrderServices = itemOrderServices;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            try
            {
                var order = await orderRepositories.GetByIdAsync(id);
                if (order == null)
                    return null;

                return order;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResultViewModel<bool>> RegisterOrderAsync(NewOrderViewModel model)
        {
            try
            {
                var user = await userRepositories.GetByIdAsync(model.UserId);
                var newOrder = new Order
                {
                    User = user
                };

                await orderRepositories.RegisterOrderAsync(newOrder);
                var order = await orderRepositories.GetLastOrderAsync();

                await itemOrderServices.RegisterItemOrderListAsync(model.Items, order);
                return new ResultViewModel<bool>(true);
                
            }
            catch (DbUpdateException)
            {
                return new ResultViewModel<bool>("27X15 - Server failure");
            }
        }

       
    }
}
