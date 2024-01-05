using System.Linq.Expressions;

namespace LHBookstore.Application.Interfaces.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(string id);
        List<T> GetAll();
        List<T> FindByCondition(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
