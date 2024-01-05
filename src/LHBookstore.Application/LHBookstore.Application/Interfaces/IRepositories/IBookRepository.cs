using LHBookstore.Domain.Entities;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace LHBookstore.Application.Interfaces.IRepositories
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
