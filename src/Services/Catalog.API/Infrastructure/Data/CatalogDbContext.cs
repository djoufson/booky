using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Infra.Data;

public sealed class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    internal DbSet<Book> Books { get; set; }
    internal DbSet<Author> Authors { get; set; }
    internal DbSet<BookTag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
    }
}
