using AutoMapper;
using LHBookstore.Application.DTOs.Book;
using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Application.Interfaces.Services;
using LHBookstore.Application.Utilities;
using LHBookstore.Domain;
using LHBookstore.Domain.Entities;

namespace LHBookstore.Application.ServiceImplementations
{
    public class BookServices : IBookServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<BookResponseDto>>> GetAllBooksAsync(int page, int perPage)
        {
            if (page <= 0 || perPage <= 0)
            {
                return ApiResponse<List<BookResponseDto>>.Failed(false, "Page and PerPage must be greater than zero", 400, null);
            }

            var allBooks = await _unitOfWork.BookRepository.GetAllBooksAsync();
            var paginatedBooks = await Pagination<Book>.GetPager(allBooks, perPage, page, b => b.Title, b => b.Id);

            var bookDtos = _mapper.Map<List<BookResponseDto>>(paginatedBooks.Data);

            return ApiResponse<List<BookResponseDto>>.Success(bookDtos, "Books retrieved successfully", 200);
        }

        public async Task<ApiResponse<BookResponseDto>> GetBookByIdAsync(string id)
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

        public async Task<ApiResponse<BookResponseDto>> AddBookAsync(BookRequestDto book)
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

        public async Task<ApiResponse<BookResponseDto>> UpdateBookAsync(string id, BookRequestDto book)
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

            _unitOfWork.BookRepository.UpdateBookAsync(existingBook);
            await _unitOfWork.SaveChangesAsync();

            var updatedBookDto = _mapper.Map<BookResponseDto>(existingBook);

            return ApiResponse<BookResponseDto>.Success(updatedBookDto, "Book updated successfully", 200);
        }

        public async Task<ApiResponse<string>> DeleteBookAsync(string id)
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
    }
}
