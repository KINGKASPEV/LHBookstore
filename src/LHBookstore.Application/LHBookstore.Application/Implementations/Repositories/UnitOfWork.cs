﻿using LHBookstore.Application.Interfaces.IRepositories;
using LHBookstore.Infrastructure;

namespace LHBookstore.Application.Implementations.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LHBContext _dbContext;
        public UnitOfWork(LHBContext dbContext)
        {
            _dbContext = dbContext;
            BookRepository = new BookRepository(_dbContext);
            OrderRepository = new OrderRepository(_dbContext);
        }
        public IBookRepository BookRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public int SaveChanges() => _dbContext.SaveChanges();
        public void Dispose() => _dbContext.Dispose();
    }
}
