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

    internal static readonly Func<CatalogDbContext, int, int, IAsyncEnumerable<Book>> GetAllBooksQuery =
        EF.CompileAsyncQuery((CatalogDbContext ctx, int pageNumber, int pageSize) => ctx.Books
            .AsNoTracking()
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .Include(b => b.Author)
            .Include(b => b.Tags));

    internal static readonly Func<CatalogDbContext, string, int, int, IAsyncEnumerable<Book>> SearchByNameOrTagOrAuthor = 
        EF.CompileAsyncQuery((CatalogDbContext ctx, string query, int pageNumber, int pageSize) => ctx.Books
            .AsNoTracking()
            .Where(b =>
                b.Title.Contains(query) ||
                b.Author.Name.First.Contains(query) ||
                b.Tags.Select(t => t.Tag).Contains(query) ||
                (b.Author.Name.Last == null) || b.Author.Name.Last.Contains(query))
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize));
}
