using LHBookstore.Application.DTOs.Book;
using LHBookstore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LHBookstore.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBookAsync([FromBody] BookRequestDto book)
        {
            var response = await _bookServices.AddBookAsync(book);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("Rent/{id}")]
        public async Task<IActionResult> RentBook(string id)
        {
            var response = await _bookServices.RentBookAsync(id);

            if (response.Succeeded)
            {
                return Ok(response.Data);
            }

            return BadRequest(response);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateBookAsync(string id, [FromBody] BookRequestDto book)
        {
            var response = await _bookServices.UpdateBookAsync(id, book);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("All-books")]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] int page, [FromQuery] int perPage)
        {
            var response = await _bookServices.GetAllBooksAsync(page, perPage);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(string id)
        {
            var response = await _bookServices.GetBookByIdAsync(id);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBookAsync(string id)
        {
            var response = await _bookServices.DeleteBookAsync(id);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}

