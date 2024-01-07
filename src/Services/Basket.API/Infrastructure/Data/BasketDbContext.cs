using Basket.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Data;

public class BasketDbContext(DbContextOptions<BasketDbContext> options) : DbContext(options)
{
    internal DbSet<Book> Books { get; set; }
    internal DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BasketDbContext).Assembly);
    }
}
