﻿using API_Ecommerce.Extensions;
using API_Ecommerce.Models;
using API_Ecommerce.Services;
using API_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API_Ecommerce.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices productServices;
        public ProductController(ProductServices productService)
        {
            this.productServices = productService;
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
                var product = await productServices.GetByIdAsync(id);
                if (product == null)
                    return NotFound("Product not found");

                return Ok(new ResultViewModel<Product>(product));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("42X00 - Server failure"));
            }
        }
    }
}