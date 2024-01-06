﻿using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LHBookstore.Application.Implementations.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LHBContext _dbContext;

        public GenericRepository(LHBContext dbContext) => _dbContext = dbContext;

        public async Task AddAsync(T entity) => await _dbContext.Set<T>().AddAsync(entity);

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);
   
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) => await _dbContext.Set<T>().AnyAsync(predicate);

        public async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate) => await _dbContext.Set<T>().Where(predicate).ToListAsync();

        public async Task<List<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(string id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);
    }
}

