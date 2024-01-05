using System.Linq.Expressions;

namespace LHBookstore.Application.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<List<T>> FindByCondition(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
