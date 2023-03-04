﻿using API_Ecommerce.Services;
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

        [HttpGet]
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
    }
}
