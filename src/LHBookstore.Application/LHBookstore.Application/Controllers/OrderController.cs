using LHBookstore.Application.DTOs.Order;
using LHBookstore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LHBookstore.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrderAsync(OrderRequestDto orderRequest)
        {
            var response = await _orderService.PlaceOrderAsync(orderRequest);
            if (response.Succeeded)
            {
                return Ok(response);
                //return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = response.Data }, response);
            }
            return BadRequest(response);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateOrderAsync(string id, OrderRequestDto orderRequest)
        {
            var response = await _orderService.UpdateOrderAsync(id, orderRequest);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("all-order")]
        public async Task<IActionResult> GetAllOrdersAsync([FromQuery] int page, [FromQuery] int perPage)
        {
            var response = await _orderService.GetAllOrdersAsync(page, perPage);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(string id)
        {
            var response = await _orderService.GetOrderByIdAsync(id);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrderAsync(string id)
        {
            var response = await _orderService.CancelOrderAsync(id);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
