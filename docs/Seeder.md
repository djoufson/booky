``` c#
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
                new BookTag("contemporary"),
                new BookTag("romance"),
                new BookTag("poetry"),
                new BookTag("historical"),
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
                Author.Create(new UserName("jessica"), new Name("Jessica", "Anderson"), new Email("jessica@email.com"), "", ""),
                Author.Create(new UserName("alexander"), new Name("Alexander", "Holloway"), new Email("alexander@email.com"), "", ""),
            ];

            await context.AddRangeAsync(authors);
            await context.SaveChangesAsync();
        }

        if (!await context.Books.AnyAsync())
        {
            var tags = await context.Tags.ToArrayAsync();
            var authors = await context.Authors.ToArrayAsync();

            System.Console.WriteLine("\n--> Tags");
            foreach (var tag in tags)
            {
                Console.WriteLine(tag);
            }
            System.Console.WriteLine("\n--> Authors");
            foreach (var author in authors)
            {
                Console.WriteLine(author.UserName.Value);
            }

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

                // Book.Create(
                //     "Slammed",
                //     "slammed",
                //     "Immerse yourself in the vibrant world of \"Slammed,\" where the pulsating rhythm of competitive poetry slams becomes a metaphor for life's unpredictable cadence. Protagonist Lily, grappling with the raw emotions of love and loss, discovers an unexpected sanctuary in the spoken word. As she navigates the challenges of young adulthood, the poetry slam stage becomes both a battleground and a haven. The complexities of relationships, heartache, and resilience unfold, intertwining with the powerful verses that echo the human experience. Jessica Anderson crafts a narrative that goes beyond romance, offering a lyrical exploration of self-discovery, the transformative power of words, and the universal quest for connection in a world that often feels like a tumultuous slam.",
                //     1100,
                //     null,
                //     authors.First(a => a.UserName.Value == "jessica"),
                //     "pic7.jpg",
                //     DateTime.UtcNow,
                //     DateTime.UtcNow,
                //     tags.First(t => t.Tag == "contemporary"),
                //     tags.First(t => t.Tag == "romance"),
                //     tags.First(t => t.Tag == "coming-of-age"),
                //     tags.First(t => t.Tag == "poetry")),

                // Book.Create(
                //     "Gas Light",
                //     "gas-light",
                //     "Immerse yourself in the intoxicating ambiance of \"Gas Light,\" a spellbinding journey into the gaslit alleys of Victorian London. In this atmospheric tale by Alexander Holloway, the city itself becomes a character, shrouded in mist and mystery. Follow the brilliant yet enigmatic detective, Eleanor Thornton, as she navigates a labyrinth of gas-lit streets and unravels a series of perplexing crimes that defy conventional explanation. The dimly lit corners conceal secrets, and whispers of a sinister society echo through cobblestone streets.\nAs Eleanor delves deeper into the shadows, readers are drawn into a world where gas lamps flicker like specters, casting long shadows on the mystery unfolding. The narrative weaves intricate threads of suspense, intrigue, and the delicate dance between light and darkness. Each gas lamp holds a secret, and every twist in the story reveals a new layer of complexity, leaving readers on the edge of their seats.\nAgainst the rich historical backdrop of Victorian London, \"Gas Light\" explores the societal tensions, technological marvels, and class disparities of the era. Alexander Holloway's meticulous research brings the city to life, transporting readers to a time when gas lamps illuminated not only streets but also the shadowy realms of crime and intrigue.\nWith every turn of the page, the tension builds, culminating in a gripping climax that challenges Eleanor Thornton's intellect and resolve. \"Gas Light\" is more than a mystery; it's a mesmerizing exploration of the human psyche, societal undercurrents, and the timeless allure of a city veiled in gas-lit enchantment.",
                //     1100,
                //     null,
                //     authors.First(a => a.UserName.Value == "alexander"),
                //     "pic9.jpg",
                //     DateTime.UtcNow,
                //     DateTime.UtcNow,
                //     tags.First(t => t.Tag == "historical"),
                //     tags.First(t => t.Tag == "mystery"),
                //     tags.First(t => t.Tag == "fantasy"),
                //     tags.First(t => t.Tag == "thriller")),

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

```