using API_Ecommerce.Extensions;
using API_Ecommerce.Services;
using API_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API_Ecommerce.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices userServices;
        public UserController(UserServices userService)
        {
            this.userServices = userService;
        }

        [HttpPost("v1/teste")]
        public async Task<IActionResult> RegisterUser(
            [FromBody] NewUserViewModel model
            )
        {
            
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {    
                var newUser = await userServices.RegisterUserAsync(model);

                if (newUser.Success)
                    return Ok(new ResultViewModel<NewUserViewModel>(model));

                return StatusCode(401, newUser.Message);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("05X04 - Server failure"));
            }

        }
    }
}
