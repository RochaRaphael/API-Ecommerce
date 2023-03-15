using API_Ecommerce.Extensions;
using API_Ecommerce.Models;
using API_Ecommerce.Services;
using API_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
                var user = await userServices.GetByIdAsync(id);
                if (user == null)
                    return NotFound("User not found");

                return Ok(new ResultViewModel<ShowUserViewModel>(user));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("42X00 - Server failure"));
            }
        }

        [HttpPost("v1/newaccount/")]
        public async Task<IActionResult> RegisterUser(
        [FromBody] NewUserViewModel model
        )
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var response = await userServices.RegisterUserAsync(model);

                if (response.Data != null)
                    return Ok("Successfully registered user");

                return StatusCode(401, "User already exists");
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
                if (response.Data != null)
                    return Ok(response.Data.LastToken);
                else
                    return StatusCode(401, new ResultViewModel<string>(response.Errors));


            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("42X74 - Server failure"));
            }
        }


        [HttpPut("v1/accounts/update/{id:int}")]
        public async Task<IActionResult> UpdateUser(
            [FromRoute] int id,
            [FromBody] NewUserViewModel model           
            )
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var updateUser = await userServices.UpdateUserAsync(model, id);

                if (updateUser.Data != null)
                    return Ok(new ResultViewModel<NewUserViewModel>(model));

                return StatusCode(401, updateUser.Errors);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("05X04 - Server failure"));
            }

        }

        [HttpDelete("v1/login/delete/{id:int}")]
        public async Task<IActionResult> DeleteUserAsync(
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            try
            {
                var result = await userServices.DeleteUserAsync(id);
                if (result.Data == true)
                    return Ok("User deleted");
                else
                    return StatusCode(404, result.Errors);
            }
            catch
            {
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            }

        }
    }
}
