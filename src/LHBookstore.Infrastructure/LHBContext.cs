using LHBookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LHBookstore.Infrastructure
{
    public class LHBContext : DbContext
    {
        public LHBContext(DbContextOptions<LHBContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderItem>().HasOne(x => x.Order).WithMany(x => x.OrderItems);
        }
    }
}