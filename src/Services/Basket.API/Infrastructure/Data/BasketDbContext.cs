using Basket.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Data;

public class BasketDbContext(DbContextOptions<BasketDbContext> options) : DbContext(options)
{
    internal DbSet<Customer> Customers { get; set; }
    internal DbSet<CustomerBasket> Baskets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BasketDbContext).Assembly);
    }
}
