using LHBookstore.Application.DTOs.Book;
using LHBookstore.Application.Utilities;
using LHBookstore.Domain;

namespace LHBookstore.Application.Interfaces.Services
{
    public interface IBookServices
    {
        Task<ApiResponse<PageResult<List<BookResponseDto>>>> GetAllBooksAsync(int page, int perPage);
        Task<ApiResponse<BookResponseDto>> GetBookByIdAsync(string id);
        Task<ApiResponse<BookResponseDto>> AddBookAsync(BookRequestDto book);
        Task<ApiResponse<BookResponseDto>> UpdateBookAsync(string id, BookRequestDto book);
        Task<ApiResponse<string>> DeleteBookAsync(string id);
    }
}
