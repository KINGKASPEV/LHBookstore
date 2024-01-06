using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Application.Interfaces.Services;
using LHBookstore.Domain.Entities;

namespace LHBookstore.Application.ServiceImplementations
{
    public class BookServices : IBookServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _unitOfWork.BookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Book id cannot be null or empty.");
            }

            return await _unitOfWork.BookRepository.GetBookByIdAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            await _unitOfWork.BookRepository.AddBookAsync(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            var existingBook = await _unitOfWork.BookRepository.GetBookByIdAsync(book.Id);

            if (existingBook == null)
            {
                throw new InvalidOperationException($"Book with id {book.Id} not found.");
            }

            _unitOfWork.BookRepository.UpdateBookAsync(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Book id cannot be null or empty.");
            }

            var book = await _unitOfWork.BookRepository.GetBookByIdAsync(id);

            if (book != null)
            {
                _unitOfWork.BookRepository.DeleteBookAsync(book);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
