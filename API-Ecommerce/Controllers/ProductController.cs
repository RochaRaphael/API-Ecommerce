﻿using API_Ecommerce.Extensions;
using API_Ecommerce.Models;
using API_Ecommerce.Services;
using API_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("v1/product/{id:int}")]
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

        [Authorize(Roles = "admin, boss")]
        [HttpPost("v1/newproduct/")]
        public async Task<IActionResult> RegisterProduct(
            [FromBody] NewProductViewModel model
            )
        {

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var result = await productServices.RegisterProductAsync(model);

                if (result.Data != null)
                    return Ok(new ResultViewModel<NewProductViewModel>(model));

                return StatusCode(401, result.Errors);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("55X21 - Server failure"));
            }

        }

        [Authorize(Roles = "admin, boss")]
        [HttpPut("v1/product/update/{id:int}")]
        public async Task<IActionResult> UpdateProduct(
            [FromRoute] int id,
            [FromBody] NewProductViewModel model          
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            try
            {
                var updateProduct = await productServices.UpdateProductAsync(model, id);

                if (updateProduct.Data != null)
                    return Ok(new ResultViewModel<NewProductViewModel>(model));

                return StatusCode(401, updateProduct.Errors);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("33X46 - Server failure"));
            }
        }

        [Authorize(Roles = "admin, boss")]
        [HttpDelete("v1/product/delete/{id:int}")]
        public async Task<IActionResult> DeleteProductAsync(
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            try
            {
                var result = await productServices.DeleteProductAsync(id);
                if (result.Data == true)
                    return Ok("Product deleted");
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
