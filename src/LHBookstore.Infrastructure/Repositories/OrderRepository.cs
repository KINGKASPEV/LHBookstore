using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Domain.Entities;
using LHBookstore.Infrastructure;
using System.Linq.Expressions;

namespace LHBookstore.Application.Implementations.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LHBContext dbContext) : base(dbContext) { }

        public async Task AddOrderAsync(Order order) => await AddAsync(order);

        public async Task<List<Order>> FindOrdersAsync(Expression<Func<Order, bool>> condition) => await FindByConditionAsync(condition);

        public async Task<Order> GetOrderByIdAsync(string id) => await GetByIdAsync(id);

        public async Task<List<Order>> GetAllOrdersAsync() => await GetAllAsync();

        public async Task UpdateOrderAsync(Order order)
        {
            Update(order);
            await SaveChangesAsync();
        }
        public async Task DeleteOrderAsync(Order order)
        {
            Delete(order);
            await SaveChangesAsync();
        }
    }
}
