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
        public OrderServices(OrderRepositories orderRepositories, UserRepositories userRepositories, ItemOrderServices itemOrderServices)
        {
            this.orderRepositories = orderRepositories;
            this.userRepositories = userRepositories;
            this.itemOrderServices = itemOrderServices;
        }

        public async Task<ResultViewModel<List<ShowOrderViewModel>>> GetListByUserIdAsync(int id)
        {
            try
            {
                List<ShowOrderViewModel> orderList = new List<ShowOrderViewModel>();
                var orders = await orderRepositories.GetListByUserAsync(id, 1, 25);
                foreach (var item in orders)
                {
                    var order = new ShowOrderViewModel
                    {
                        OrderDate = item.OrderDate,
                        ItemOrders = item.ItemOrders,
                    };
                    orderList.Add(order);
                }

                return new ResultViewModel<List<ShowOrderViewModel>>(orderList);
            }
            catch
            {
                return new ResultViewModel<List<ShowOrderViewModel>>("17X10 - Server failure");
            }
        }

        public async Task<ResultViewModel<bool>> RegisterOrderAsync(List<NewItemOrderViewModel> model, int userId)
        {
            try
            {
                var user = await userRepositories.GetByIdToRegisterAsync(userId);
                var newOrder = new Order
                {
                    User = user
                };

                await orderRepositories.RegisterOrderAsync(newOrder);
                var order = await orderRepositories.GetLastOrderAsync();

                await itemOrderServices.RegisterItemOrderListAsync(model, order);
                return new ResultViewModel<bool>(true);
                
            }
            catch (DbUpdateException)
            {
                return new ResultViewModel<bool>(false);
            }
        }

       
    }
}
