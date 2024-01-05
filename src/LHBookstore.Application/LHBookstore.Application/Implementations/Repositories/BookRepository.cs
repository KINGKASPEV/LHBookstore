using LHBookstore.Application.Interfaces.IRepositories;
using LHBookstore.Domain.Entities;
using LHBookstore.Infrastructure;
using System.Linq.Expressions;

namespace LHBookstore.Application.Implementations.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LHBContext dbContext) : base(dbContext) { }
        public void AddBook(Book book) => Add(book);
        public void DeleteBook(Book book) => Delete(book);
        public List<Book> FindBook(Expression<Func<Book, bool>> condition) => FindByCondition(condition);
        public Book GetBookById(string id) => GetById(id);
        public List<Book> GetBooks() => GetAll();
        public void UpdateBook(Book book) => Update(book);
    }
}

