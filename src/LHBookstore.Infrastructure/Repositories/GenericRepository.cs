using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Infrastructure;
using System.Linq.Expressions;

namespace LHBookstore.Application.Implementations.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LHBContext _dbContext;
        public GenericRepository(LHBContext dbContext) => _dbContext = dbContext;
        public void Add(T entity) => _dbContext.Set<T>().Add(entity);
        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);
        public bool Exists(Expression<Func<T, bool>> predicate) => _dbContext.Set<T>().Any(predicate);
        public List<T> FindByCondition(Expression<Func<T, bool>> predicate) => _dbContext.Set<T>().Where(predicate).ToList();
        public List<T> GetAll() => _dbContext.Set<T>().ToList();
        public T GetById(string id) => _dbContext.Set<T>().Find(id);
        public void SaveChanges() => _dbContext.SaveChanges();
        public void Update(T entity) => _dbContext.Set<T>().Update(entity);
    }
}
