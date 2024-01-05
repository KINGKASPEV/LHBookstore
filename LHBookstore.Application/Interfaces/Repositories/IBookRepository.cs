using LHBookstore.Domain.Entities;
using System.Linq.Expressions;

namespace LHBookstore.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetBooks();
        void AddBook(Book book);
        void DeleteBook(Book book);
        public List<Book> FindBook(Expression<Func<Book, bool>> condition);
        Book GetBookById(string id);
        void UpdateBook(Book book);
    }
}
