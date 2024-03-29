﻿using AutoMapper;
using LHBookstore.Application.DTOs.Book;
using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Application.Interfaces.Services;
using LHBookstore.Application.Utilities;
using LHBookstore.Domain;
using LHBookstore.Domain.Entities;
using Microsoft.Extensions.Logging;

public class BookServices : IBookServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public BookServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BookServices> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResponse<BookResponseDto>> AddBookAsync(BookRequestDto book)
    {
        try
        {
            if (book == null)
            {
                return ApiResponse<BookResponseDto>.Failed(false, "Book cannot be null", 400, null);
            }

            var newBook = _mapper.Map<Book>(book);
            await _unitOfWork.BookRepository.AddBookAsync(newBook);
            await _unitOfWork.SaveChangesAsync();

            var newBookDto = _mapper.Map<BookResponseDto>(newBook);

            return ApiResponse<BookResponseDto>.Success(newBookDto, "Book added successfully", 201);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in AddBookAsync: {ex.Message}");
            return ApiResponse<BookResponseDto>.Failed(false, "An error occurred while adding the book", 500, new List<string> { ex.Message });
        }
    }

    public async Task<ApiResponse<BookResponseDto>> RentBookAsync(string Id)
    {
        try
        {
            var book = await _unitOfWork.BookRepository.GetBookByIdAsync(Id);

            if (book == null)
            {
                return ApiResponse<BookResponseDto>.Failed(false, "Book should not be null", 400, null);
            }

            if (!book.IsAvailable)
            {
                return ApiResponse<BookResponseDto>.Failed(false, "Book not currently available, check back later", 400, null);
            }

            book.IsAvailable = true;

            await _unitOfWork.BookRepository.UpdateBookAsync(book);

            await _unitOfWork.SaveChangesAsync();

            var rentedBookDto = _mapper.Map<BookResponseDto>(book);

            return ApiResponse<BookResponseDto>.Success(rentedBookDto, "Book rented successfully", 200);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Renting out books: {ex.Message}");
            return ApiResponse<BookResponseDto>.Failed(false, "error occured while renting out this book", 500, new List<string> { ex.Message});
        }

    }



    public async Task<ApiResponse<PageResult<List<BookResponseDto>>>> GetAllBooksAsync(int page, int perPage)
    {
        try
        {
            if (page <= 0 || perPage <= 0)
            {
                return ApiResponse<PageResult<List<BookResponseDto>>>.Failed(false, "Page and PerPage must be greater than zero", 400, null);
            }

            var allBooks = await _unitOfWork.BookRepository.GetAllBooksAsync();
            var paginatedBooks = await Pagination<Book>.GetPager(allBooks, perPage, page, b => b.Title, b => b.Id);

            var bookDtos = _mapper.Map<List<BookResponseDto>>(paginatedBooks.Data);

            var pageResult = new PageResult<List<BookResponseDto>>
            {
                Data = bookDtos,
                TotalPageCount = paginatedBooks.TotalPageCount,
                CurrentPage = paginatedBooks.CurrentPage,
                PerPage = paginatedBooks.PerPage,
                TotalCount = paginatedBooks.TotalCount
            };

            return ApiResponse<PageResult<List<BookResponseDto>>>.Success(pageResult, "Orders retrieved successfully", 200);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in GetAllBooksAsync: {ex.Message}");
            return ApiResponse<PageResult<List<BookResponseDto>>>.Failed(false, "An error occurred while retrieving books", 500, new List<string> { ex.Message });
        }
    }

    public async Task<ApiResponse<string>> DeleteBookAsync(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id))
            {
                return ApiResponse<string>.Failed(false, "Book id cannot be null or empty", 400, null);
            }

            var existingBook = await _unitOfWork.BookRepository.GetBookByIdAsync(id);

            if (existingBook == null)
            {
                return ApiResponse<string>.Failed(false, $"Book with id {id} not found", 404, null);
            }

            await _unitOfWork.BookRepository.DeleteBookAsync(existingBook);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<string>.Success($"Book with id {id} deleted successfully", "Book deleted successfully", 200);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in DeleteBookAsync: {ex.Message}");
            return ApiResponse<string>.Failed(false, "An error occurred while deleting the book", 500, new List<string> { ex.Message });
        }
    }

    public async Task<ApiResponse<BookResponseDto>> GetBookByIdAsync(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id))
            {
                return ApiResponse<BookResponseDto>.Failed(false, "Book id cannot be null or empty", 400, null);
            }

            var book = await _unitOfWork.BookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                return ApiResponse<BookResponseDto>.Failed(false, $"Book with id {id} not found", 404, null);
            }

            var bookDto = _mapper.Map<BookResponseDto>(book);

            return ApiResponse<BookResponseDto>.Success(bookDto, "Book retrieved successfully", 200);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in GetBookByIdAsync: {ex.Message}");
            return ApiResponse<BookResponseDto>.Failed(false, "An error occurred while retrieving the book", 500, new List<string> { ex.Message });
        }
    }

    public async Task<ApiResponse<BookResponseDto>> UpdateBookAsync(string id, BookRequestDto book)
    {
        try
        {
            if (string.IsNullOrEmpty(id))
            {
                return ApiResponse<BookResponseDto>.Failed(false, "Book id cannot be null or empty", 400, null);
            }

            var existingBook = await _unitOfWork.BookRepository.GetBookByIdAsync(id);

            if (existingBook == null)
            {
                return ApiResponse<BookResponseDto>.Failed(false, $"Book with id {id} not found", 404, null);
            }

            _mapper.Map(book, existingBook);

           await _unitOfWork.BookRepository.UpdateBookAsync(existingBook);
            await _unitOfWork.SaveChangesAsync();

            var updatedBookDto = _mapper.Map<BookResponseDto>(existingBook);

            return ApiResponse<BookResponseDto>.Success(updatedBookDto, "Book updated successfully", 200);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in UpdateBookAsync: {ex.Message}");
            return ApiResponse<BookResponseDto>.Failed(false, "An error occurred while updating the book", 500, new List<string> { ex.Message });
        }
    }

}
