using Catalog.API.Infra.Data;
using Catalog.API.Models;
using Catalog.API.Models.Types;
using Microsoft.EntityFrameworkCore;
using Shared.EF.Database;

namespace Catalog.API.Infra;

internal class BooksCatalogSeeder : IDbSeeder<CatalogDbContext>
{
    public async Task SeedAsync(CatalogDbContext context)
    {
        if(!await context.Authors.AnyAsync())
        {
            Author[] authors = [
                Author.Create(
                    new UserName("johndoe"),
                    new Name("John", "Doe"),
                    new Email("johndoe@email.com"),
                    "A passionate romancer and theatre maker",
                    null
                ),
                Author.Create(
                    new UserName("janesmith"),
                    new Name("Jane", "Smith"),
                    new Email("janesmith@email.com"),
                    "A woman with love for rich stories and passionate romance",
                    null
                ),
                Author.Create(
                    new UserName("alice237"),
                    new Name("Alice", null),
                    new Email("alice@email.com"),
                    "An author of drama pieces",
                    null
                ),
            ];

            await context.AddRangeAsync(authors);
            await context.SaveChangesAsync();
        }

        if(!await context.Books.AnyAsync())
        {
            var firstAuthor = await context.Authors.FirstAsync();
            var book1 = Book.Create(
                "Figaro's wedding",
                "The incredible wedding ceremony of Figaro",
                250,
                null,
                firstAuthor,
                DateTime.UtcNow,
                DateTime.UtcNow
            );
            var book2 = Book.Create(
                "Figaro's wedding - Act 2",
                "The incredible wedding ceremony of Figaro",
                300,
                null,
                firstAuthor,
                DateTime.UtcNow,
                DateTime.UtcNow
            );

            var lastAuthor = await context.Authors.LastAsync();

            var book3 = Book.Create(
                "Previous Halloween, Last Halloween",
                "Dive deep inside the weirdest Halloween ever... Boo",
                600,
                null,
                lastAuthor,
                DateTime.UtcNow,
                DateTime.UtcNow
            );

            await context.AddRangeAsync(book1, book2);
            await context.SaveChangesAsync();
        }
    }
}
