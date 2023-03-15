using API_Ecommerce.Extensions;
using API_Ecommerce.Models;
using API_Ecommerce.Services;
using API_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API_Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderServices orderServices;
        public OrderController(OrderServices orderService)
        {
            this.orderServices = orderService;
        }


        [HttpGet("v1/order/listby/{id:int}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var result = await orderServices.GetListByUserIdAsync(id);
                if (result == null)
                    return Ok("There isn't order with this user");

                return Ok(new ResultViewModel<List<ShowOrderViewModel>>(result.Data));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("88X07 - Server failure"));
            }
        }

        [HttpPost("v1/neworder/{id:int}")]
        public async Task<IActionResult> RegisterProduct(
            [FromRoute] int id,
            [FromBody] List<NewItemOrderViewModel> model
            )
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var result = await orderServices.RegisterOrderAsync(model, id);

                if (result.Data == null)
                    return StatusCode(401, result.Errors);


                return Ok("Registered order");
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("55X21 - Server failure"));
            }

        }
    }
}
