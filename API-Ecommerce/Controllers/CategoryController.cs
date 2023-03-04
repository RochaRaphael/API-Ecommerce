using API_Ecommerce.Services;
using API_Ecommerce.Extensions;
using API_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using API_Ecommerce.Models;

namespace API_Ecommerce.Controllers
{
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryServices categoryServices;
        public CategoryController(CategoryServices categoryService)
        {
            this.categoryServices = categoryService;
        }

        [HttpGet("v1 / product /{id: int}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var category = await categoryServices.GetByIdAsync(id);
                if (category == null)
                    return NotFound("Category not found");

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("88X07 - Server failure"));
            }
        }

        [HttpPost("v1/newcategory/")]
        public async Task<IActionResult> RegisterCategory(
            [FromBody] string categoryName
            )
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var newProduct = await categoryServices.RegisterCategoryAsync(categoryName);

                if (newProduct.Data != null)
                    return Ok(new ResultViewModel<NewProductViewModel>(categoryName));

                return StatusCode(401, newProduct.Errors);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("55X21 - Server failure"));
            }
        }

        [HttpPut("v1category/update/{int:id}")]
        public async Task<IActionResult> UpdateCategory(
            [FromBody] string model,
            [FromRoute] int id
            )
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var updateCategory = await categoryServices.UpdateCategoryAsync(model, id);

                if (updateCategory.Data != null)
                    return Ok(new ResultViewModel<string>(model));

                return StatusCode(401, updateCategory.Errors);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("19X33 - Server failure"));
            }
        }
    }
}
