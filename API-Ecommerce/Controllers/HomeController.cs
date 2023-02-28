using Microsoft.AspNetCore.Mvc;

namespace API_Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get(
            [FromServices] IConfiguration config)
        {
            var env = config.GetValue<string>("Env");
            return Ok(new
            {
                environment = env
            });
        }
    }
}
