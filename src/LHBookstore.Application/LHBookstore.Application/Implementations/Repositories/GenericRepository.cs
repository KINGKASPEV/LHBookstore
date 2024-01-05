using LHBookstore.Application.Services;
using LHBookstore.Infrastructure;
using System.Linq.Expressions;

namespace LHBookstore.Application.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LHBContext _dbContext;
        public GenericRepository(LHBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FindByCondition(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
