using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Domain.Entities;
using LHBookstore.Infrastructure;
using System.Linq.Expressions;

namespace LHBookstore.Application.Implementations.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LHBContext dbContext) : base(dbContext) { }

        public async Task AddBookAsync(Book book) => await AddAsync(book);

        public async Task<List<Book>> FindBookAsync(Expression<Func<Book, bool>> condition) => await FindByConditionAsync(condition);

        public async Task<Book> GetBookByIdAsync(string id) => await GetByIdAsync(id);

        public async Task<List<Book>> GetAllBooksAsync() => await GetAllAsync();

        public async Task UpdateBookAsync(Book book)
        {
            Update(book);
            await SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Book book)
        {
             Delete(book);
            await SaveChangesAsync();
        }
    }
}

