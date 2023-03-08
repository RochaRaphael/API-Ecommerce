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

        [HttpGet("v1/category/{id:int}")]
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
                var result = await categoryServices.RegisterCategoryAsync(categoryName);

                if (result.Data != null)
                    return Ok(new ResultViewModel<Category>(result.Data));

                return StatusCode(401, result.Errors);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("55X21 - Server failure"));
            }
        }

        [HttpPut("v1/category/update/{id:int}")]
        public async Task<IActionResult> UpdateCategory(
            [FromRoute] int id,
            [FromBody] string model          
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

        [HttpDelete("v1/category/delete/{id:int}")]
        public async Task<IActionResult> DeleteCategoryAsync(
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            try
            {
                var result = await categoryServices.DeleteCategoryAsync(id);
                if (result.Data == true)
                    return Ok("Category deleted");
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
