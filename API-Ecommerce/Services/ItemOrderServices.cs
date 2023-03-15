using API_Ecommerce.Models;
using API_Ecommerce.Repositories;
using API_Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API_Ecommerce.Services
{
    public class ItemOrderServices
    {
        private readonly ItemOrderRepositories itemOrderRepositories;
        private readonly ProductRepositories productRepositories;
        private readonly OrderServices orderServices;
        
        public ItemOrderServices(ItemOrderRepositories itemOrderRepositories, OrderServices orderRServices, ProductRepositories productRepositorie)
        {
            this.itemOrderRepositories = itemOrderRepositories;
            this.orderServices = orderServices;
            this.productRepositories = productRepositorie;

        }

        public async Task<ItemOrder> GetByIdAsync(int id)
        {
            try
            {
                var itemOrder = await itemOrderRepositories.GetByIdAsync(id);
                if (itemOrder == null)
                    return null;

                return itemOrder;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResultViewModel<List<ItemOrder>>> RegisterItemOrderListAsync(List<NewItemOrderViewModel> itemList, Order order)
        {
            try
            {
                List<ItemOrder> listToSave = new List<ItemOrder>();
                foreach (var item in itemList)
                {
                    var product = await productRepositories.GetByNameAsync(item.ProductName);
                    if (product == null)
                        return new ResultViewModel<List<ItemOrder>>("The product " + item.ProductName + " does not exist");

                    var newItemOrder = new ItemOrder
                    {
                        Order = order,
                        Product = product,
                        Quantity = item.Quantity
                    };
                    listToSave.Add(newItemOrder);
                }

                foreach (var item in listToSave)
                {
                    await itemOrderRepositories.RegisterItemOrderAsync(item);
                }
                

                return new ResultViewModel<List<ItemOrder>>(listToSave);

            }
            catch (DbUpdateException)
            {
                return new ResultViewModel<List<ItemOrder>>("02X09 - Server failure");
            }
        }
    }
}
