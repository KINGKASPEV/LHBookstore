﻿using LHBookstore.Application.DTOs.Book;
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
        public async Task<IActionResult> AddBook([FromBody] BookRequestDto book)
        {
            var response = await _bookServices.AddBookAsync(book);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateBook(string id, [FromBody] BookRequestDto book)
        {
            var response = await _bookServices.UpdateBookAsync(id, book);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("All-books")]
        public async Task<IActionResult> GetAllBooks([FromQuery] int page, [FromQuery] int perPage)
        {
            var response = await _bookServices.GetAllBooksAsync(page, perPage);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(string id)
        {
            var response = await _bookServices.GetBookByIdAsync(id);

            if (response.Succeeded)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook(string id)
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

