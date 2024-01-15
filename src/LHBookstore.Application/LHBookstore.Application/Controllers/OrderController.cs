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
        public async Task<IActionResult> PlaceOrderAsync([FromBody] OrderRequestDto orderRequest, [FromQuery] string bookId)
        {
            var response = await _orderService.PlaceOrderAsync(orderRequest, bookId);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateOrderAsync(string Id, OrderRequestDto orderRequest)
        {
            var response = await _orderService.UpdateOrderAsync(Id, orderRequest);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("all-orders")]
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
                return Ok(response.Data);
            }
            return BadRequest(response);
        }

        [HttpGet("get-by-status")]
        public async Task<IActionResult> GetOrdersByStatusAsync([FromQuery] string status, [FromQuery] int page, [FromQuery] int perPage)
        {
            var response = await _orderService.GetOrdersByStatusAsync(status, page, perPage);
            if (response.Succeeded)
            {
                return Ok(response.Data);
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
