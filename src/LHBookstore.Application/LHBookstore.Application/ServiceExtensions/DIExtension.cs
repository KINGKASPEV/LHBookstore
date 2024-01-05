using LHBookstore.Application.Implementations;
using LHBookstore.Application.Implementations.Books;
using LHBookstore.Application.Implementations.Orders;
using LHBookstore.Application.Services;
using LHBookstore.Application.Services.Books;
using LHBookstore.Application.Services.Orders;
using LHBookstore.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace LHBookstore.Application.ServiceExtensions
{
    public static class DIExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IOrderService, OrderService>();
            
            services.AddDbContext<LHBContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }
    }
}
