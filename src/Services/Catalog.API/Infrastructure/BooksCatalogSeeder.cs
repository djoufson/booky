using Catalog.API.Infra.Data;
using Catalog.API.Models;
using Catalog.API.Models.Types;
using Microsoft.EntityFrameworkCore;
using Shared.EF.Database;

namespace Catalog.API.Infrastructure;

internal class BooksCatalogSeeder : IDbSeeder<CatalogDbContext>
{
    public async Task SeedAsync(CatalogDbContext context)
    {
        if (!await context.Tags.AnyAsync())
        {
            var horrorTag = new BookTag("horror");
            var romanceTag = new BookTag("romance");
            var theatreTag = new BookTag("theatre");
            await context.AddRangeAsync(horrorTag, romanceTag, theatreTag);
            await context.SaveChangesAsync();
        }

        if (!await context.Authors.AnyAsync())
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

        if (!await context.Books.AnyAsync())
        {
            var horrorTag = await context.Tags.OrderBy(t => t.Id).FirstOrDefaultAsync(t => t.Tag == "horror") ?? new BookTag("horror");
            var romanceTag = await context.Tags.OrderBy(t => t.Id).FirstOrDefaultAsync(t => t.Tag == "romance") ?? new BookTag("romance");
            var theatreTag = await context.Tags.OrderBy(t => t.Id).FirstOrDefaultAsync(t => t.Tag == "theatre") ?? new BookTag("theatre");
            var authors = await context.Authors.ToArrayAsync();
            var firstAuthor = authors.OrderBy(a => a.Id.Value).First();
            var book1 = Book.Create(
                "Figaro's wedding",
                "The incredible wedding ceremony of Figaro",
                250,
                null,
                firstAuthor,
                "pic1.jpg",
                DateTime.UtcNow,
                DateTime.UtcNow,
                theatreTag,
                romanceTag
            );
            var book2 = Book.Create(
                "Figaro's wedding - Act 2",
                "The incredible wedding ceremony of Figaro",
                300,
                null,
                firstAuthor,
                "pic2.jpg",
                DateTime.UtcNow,
                DateTime.UtcNow,
                theatreTag,
                romanceTag
            );

            var lastAuthor = authors.OrderBy(a => a.Id.Value).Last();
            var book3 = Book.Create(
                "Previous Halloween, Last Halloween",
                "Dive deep inside the weirdest Halloween ever... Boo",
                600,
                null,
                lastAuthor,
                "pic3.jpg",
                DateTime.UtcNow,
                DateTime.UtcNow,
                horrorTag
            );

            await context.AddRangeAsync(book1, book2, book3);
            await context.SaveChangesAsync();
        }
    }
}
