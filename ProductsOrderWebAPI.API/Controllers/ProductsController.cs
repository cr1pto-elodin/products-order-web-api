using Microsoft.AspNetCore.Mvc;
using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Application.Interfaces;
using ProductsOrderWebAPI.Domain.Exceptions;

namespace ProductsOrderWebAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductsService productService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            try
            {
                var result = await productService.AddProduct(dto);
                
                return CreatedAtAction(nameof(FindById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var product = await productService.FindById(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto dto)
        {
            try
            {
                await productService.UpdateProduct(dto);
                return NoContent();
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
