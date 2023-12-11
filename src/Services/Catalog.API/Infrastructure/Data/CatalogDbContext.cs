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
                b.Title.ToLower().Contains(query.ToLower()) ||
                b.Author.Name.First.ToLower().Contains(query.ToLower()) ||
                b.Tags.Select(t => t.Tag.ToLower()).Any(t => t.Contains(query.ToLower())) ||
                (b.Author.Name.Last == null) || b.Author.Name.Last.ToLower().Contains(query.ToLower()))
            .Skip(pageSize * (pageNumber - 1))
            .Include(b => b.Author)
            .Include(b => b.Tags)
            .Take(pageSize));
}
