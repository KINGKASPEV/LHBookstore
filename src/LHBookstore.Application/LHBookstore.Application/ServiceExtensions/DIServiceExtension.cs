﻿using LHBookstore.Application.Implementations.Repositories;
using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Application.Interfaces.Services;
using LHBookstore.Application.ServiceImplementations;
using LHBookstore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LHBookstore.Application.ServiceExtensions
{
    public static class DIServiceExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IBookServices, BookServices>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IConnectionProvider, ConnectionProvider>();
            //services.AddScoped<InventoryManagementService>();
            services.AddDbContext<LHBContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }
    }
}
