using LHBookstore.Domain.Entities;

namespace LHBookstore.Application.Interfaces.Services
{
    public interface IBookServices
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(string id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(string id);
    }
}
