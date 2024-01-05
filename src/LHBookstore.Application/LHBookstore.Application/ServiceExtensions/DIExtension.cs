using LHBookstore.Application.Implementations.Repositories;
using LHBookstore.Application.Implementations.Services;
using LHBookstore.Application.Interfaces.IRepositories;
using LHBookstore.Application.Interfaces.IServices;
using LHBookstore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LHBookstore.Application.ServiceExtensions
{
    public static class DIExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           // services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddDbContext<LHBContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }
    }
}
