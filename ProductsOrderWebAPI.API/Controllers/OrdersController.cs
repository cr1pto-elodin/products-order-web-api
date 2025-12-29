using Microsoft.AspNetCore.Mvc;
using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Application.Interfaces;
using ProductsOrderWebAPI.Domain.Exceptions;

namespace ProductsOrderWebAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var result = await orderService.AddOrder(dto);

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
            var order = await orderService.FindById(id);
            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDto dto)
        {
            try
            {
                await orderService.UpdateOrder(dto);
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
            await orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
