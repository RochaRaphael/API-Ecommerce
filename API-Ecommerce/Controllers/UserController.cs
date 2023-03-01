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

        [HttpGet("v1/accounts/{id:int}")]
        public async Task<IActionResult> FindUserById(
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var user = await userServices.FindUserByIdAsync(id);
                if (user == null)
                    return NotFound("User not found");
                
                return Ok(new ResultViewModel<ShowUserViewModel>(user));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("42X00 - Server failure"));
            }
        }

            [HttpPost("v1/accounts/login/")]
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


        [HttpPost("v1/login/")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var response = await userServices.LoginAsync(model);
                if (response.Success)
                    return Ok(response.Message);
                else
                    return StatusCode(401, new ResultViewModel<string>(response.Message));


            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("42X74 - Server failure"));
            }
        }

        [HttpPost("v1/login/delete/{id:int}")]
        public async Task<IActionResult> DeleteUserAsync(
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            try
            {
                var response = await userServices.DeleteUserAsync(id);
                return Ok(response.Message);
            }
            catch
            {
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            }
            
        }
    }
}
