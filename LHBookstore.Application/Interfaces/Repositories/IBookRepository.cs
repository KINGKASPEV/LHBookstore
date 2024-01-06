using LHBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LHBookstore.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();
        Task AddBookAsync(Book book);
        Task DeleteBookAsync(Book book);
        Task<List<Book>> FindBookAsync(Expression<Func<Book, bool>> condition);
        Task<Book> GetBookByIdAsync(string id);
        Task UpdateBookAsync(Book book);
    }
}
