using LHBookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LHBookstore.Infrastructure
{
    public class LHBContext : DbContext
    {
        public LHBContext(DbContextOptions<LHBContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}