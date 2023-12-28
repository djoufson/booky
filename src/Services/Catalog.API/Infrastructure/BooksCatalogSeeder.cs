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
            BookTag[] tags = [
                new BookTag("fantasy"),
                new BookTag("adventure"),
                new BookTag("coming-of-age"),
                new BookTag("mystery"),
                new BookTag("fiction"),
                new BookTag("psychology"),
                new BookTag("travelogue"),
                new BookTag("crime"),
                new BookTag("thriller"),
                new BookTag("animals"),
            ];

            await context.AddRangeAsync(tags);
            await context.SaveChangesAsync();
        }

        if (!await context.Authors.AnyAsync())
        {
            Author[] authors = [
                Author.Create(new UserName("j-rowling"), new Name("Rowling", "J. K."), new Email("rowling@email.com"), "", ""),
                Author.Create(new UserName("owilson"), new Name("Olivia", "Wilson"), new Email("owilson@email.com"), "", ""),
                Author.Create(new UserName("dazai"), new Name("Osamu", "Dazai"), new Email("dazai@email.com"), "", ""),
                Author.Create(new UserName("fischer"), new Name("Markus", "Fischer"), new Email("mfischer@email.com"), "", ""),
                Author.Create(new UserName("alice"), new Name("Alice", "McDermott"), new Email("al@email.com"), "", ""),
                Author.Create(new UserName("emma"), new Name("Emma", "Simmons"), new Email("emmons@email.com"), "", ""),
            ];

            await context.AddRangeAsync(authors);
            await context.SaveChangesAsync();
        }

        if (!await context.Books.AnyAsync())
        {
            var tags = await context.Tags.ToArrayAsync();
            var authors = await context.Authors.ToArrayAsync();

            Book[] books = [
                Book.Create(
                    "Harry Potter",
                    "harry-potter",
                    "Join young wizard Harry Potter as he navigates the magical world, discovering his unique abilities and forming lifelong friendships at Hogwarts School of Witchcraft and Wizardry. As dark forces rise, Harry learns about his connection to the dark wizard Voldemort and faces challenges that will shape his destiny. This epic series explores themes of friendship, courage, and the battle between good and evil.",
                    930,
                    null,
                    authors.First(a => a.UserName.Value == "j-rowling"),
                    "pic1.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "fantasy"),
                    tags.First(t => t.Tag == "adventure"),
                    tags.First(t => t.Tag == "coming-of-age")),

                Book.Create(
                    "Soul",
                    "soul",
                    "In a world where souls hold the key to unimaginable power, follow protagonist Alex as they embark on a perilous journey to unlock the mysteries of their own soul. As ancient forces awaken, the fate of the realms hangs in the balance, and Alex must confront their deepest fears to save everything they hold dear.",
                    430,
                    null,
                    authors.First(a => a.UserName.Value == "owilson"),
                    "pic2.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "fantasy"),
                    tags.First(t => t.Tag == "adventure"),
                    tags.First(t => t.Tag == "mystery")),

                Book.Create(
                    "The Book Of Art",
                    "the-book-of-art",
                    "In a world where souls hold the key to unimaginable power, follow protagonist Alex as they embark on a perilous journey to unlock the mysteries of their own soul. As ancient forces awaken, the fate of the realms hangs in the balance, and Alex must confront their deepest fears to save everything they hold dear.",
                    430,
                    null,
                    authors.First(a => a.UserName.Value == "owilson"),
                    "pic3.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "fantasy"),
                    tags.First(t => t.Tag == "adventure")),

                Book.Create(
                    "No Longer Human",
                    "no-longer-human",
                    "Dive into the tumultuous life of Yozo Oba, the protagonist haunted by a profound sense of alienation and detachment from society. Through introspective narratives, Osamu Dazai explores themes of identity crisis, societal expectations, and the relentless search for genuine human connection in a world that often feels devoid of authenticity.",
                    1130,
                    null,
                    authors.First(a => a.UserName.Value == "dazai"),
                    "pic4.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "psychology"),
                    tags.First(t => t.Tag == "fiction")),

                Book.Create(
                    "Big Swiss",
                    "big-swiss",
                    "Embark on a culinary journey through the heart of Switzerland with chef Thomas Müller. In \"Big Swiss,\" Müller shares his passion for traditional Swiss cuisine, offering a delectable blend of recipes, cultural anecdotes, and breathtaking landscapes. From cheese fondue in the Alps to chocolate delicacies in Zurich, this book celebrates the rich flavors and diverse culinary heritage of Switzerland.",
                    610,
                    null,
                    authors.First(a => a.UserName.Value == "dazai"),
                    "pic5.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "travelogue")),

                Book.Create(
                    "Absolution",
                    "absolution",
                    "In this gripping psychological thriller, detective Emily Thompson is tasked with solving a series of heinous crimes that lead her to confront her own haunted past. As she delves into the twisted minds of criminals, the line between justice and revenge blurs. \"Absolution\" explores the complexities of morality, redemption, and the dark corners of the human psyche.",
                    990,
                    null,
                    authors.First(a => a.UserName.Value == "alice"),
                    "pic6.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "thriller"),
                    tags.First(t => t.Tag == "crime"),
                    tags.First(t => t.Tag == "psychology")),

                Book.Create(
                    "The Little Dog",
                    "the-little-dog",
                    "Follow the heartwarming journey of a small canine named Oliver as he brings joy and healing to the lives he touches. In \"The Little Dog,\" Emma Simmons weaves a tale of companionship, resilience, and the profound impact of small gestures. As Oliver's adventures unfold, readers are reminded of the enduring bond between humans and their furry friends.",
                    990,
                    null,
                    authors.First(a => a.UserName.Value == "alice"),
                    "pic10.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "fiction"),
                    tags.First(t => t.Tag == "animals")),
            ];

            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }
    }
}
