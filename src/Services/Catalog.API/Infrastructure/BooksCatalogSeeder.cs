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
                new BookTag("contemporary"),
                new BookTag("romance"),
                new BookTag("poetry"),
                new BookTag("historical"),
                new BookTag("culinary"),
                new BookTag("cookbook"),
                new BookTag("cultural"),
                new BookTag("exploration"),
                new BookTag("magical"),
                new BookTag("classic"),
                new BookTag("drama"),
                new BookTag("suspense"),
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
                Author.Create(new UserName("mia"), new Name("Mia", "Takahashi"), new Email("mia@email.com"), "", ""),
                Author.Create(new UserName("lila"), new Name("Lila", "Nightshade"), new Email("lila@email.com"), "", ""),
                Author.Create(new UserName("scott"), new Name("F. Scott", "Fitzgerald"), new Email("scott@email.com"), "", ""),
                Author.Create(new UserName("penelope"), new Name("Penelope", "Hanley"), new Email("penelope@email.com"), "", ""),
                Author.Create(new UserName("nelson"), new Name("Isaac", "Nelson"), new Email("nelson@email.com"), "", ""),
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
                    "Slammed",
                    "slammed",
                    "Immerse yourself in the vibrant world of \"Slammed,\" where the pulsating rhythm of competitive poetry slams becomes a metaphor for life's unpredictable cadence. Protagonist Lily, grappling with the raw emotions of love and loss, discovers an unexpected sanctuary in the spoken word. As she navigates the challenges of young adulthood, the poetry slam stage becomes both a battleground and a haven. The complexities of relationships, heartache, and resilience unfold, intertwining with the powerful verses that echo the human experience. Jessica Anderson crafts a narrative that goes beyond romance, offering a lyrical exploration of self-discovery, the transformative power of words, and the universal quest for connection in a world that often feels like a tumultuous slam.",
                    1100,
                    null,
                    authors.First(a => a.UserName.Value == "jessica"),
                    "pic7.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "contemporary"),
                    tags.First(t => t.Tag == "romance"),
                    tags.First(t => t.Tag == "coming-of-age"),
                    tags.First(t => t.Tag == "poetry")),

                Book.Create(
                    "Gas Light",
                    "gas-light",
                    "Immerse yourself in the intoxicating ambiance of \"Gas Light,\" a spellbinding journey into the gaslit alleys of Victorian London. In this atmospheric tale by Alexander Holloway, the city itself becomes a character, shrouded in mist and mystery. Follow the brilliant yet enigmatic detective, Eleanor Thornton, as she navigates a labyrinth of gas-lit streets and unravels a series of perplexing crimes that defy conventional explanation. The dimly lit corners conceal secrets, and whispers of a sinister society echo through cobblestone streets.\nAs Eleanor delves deeper into the shadows, readers are drawn into a world where gas lamps flicker like specters, casting long shadows on the mystery unfolding. The narrative weaves intricate threads of suspense, intrigue, and the delicate dance between light and darkness. Each gas lamp holds a secret, and every twist in the story reveals a new layer of complexity, leaving readers on the edge of their seats.\nAgainst the rich historical backdrop of Victorian London, \"Gas Light\" explores the societal tensions, technological marvels, and class disparities of the era. Alexander Holloway's meticulous research brings the city to life, transporting readers to a time when gas lamps illuminated not only streets but also the shadowy realms of crime and intrigue.\nWith every turn of the page, the tension builds, culminating in a gripping climax that challenges Eleanor Thornton's intellect and resolve. \"Gas Light\" is more than a mystery; it's a mesmerizing exploration of the human psyche, societal undercurrents, and the timeless allure of a city veiled in gas-lit enchantment.",
                    1100,
                    null,
                    authors.First(a => a.UserName.Value == "alexander"),
                    "pic9.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "historical"),
                    tags.First(t => t.Tag == "mystery"),
                    tags.First(t => t.Tag == "fantasy"),
                    tags.First(t => t.Tag == "thriller")),

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

                Book.Create(
                    "Sushi Lover (Recipes)",
                    "sushi-lover",
                    """
                    Indulge your senses in the culinary artistry of sushi with "Sushi Lover (Recipes)" by Mia Takahashi. This gastronomic journey transcends the ordinary, offering not only a collection of delectable recipes but also an immersive exploration of the cultural and artistic aspects of sushi preparation. From the precise art of crafting perfect sushi rice to the delicate dance of slicing sashimi, Mia Takahashi shares her expertise with passion and precision.

                    The pages come alive with vibrant images showcasing the beauty of each dish, accompanied by detailed step-by-step instructions that demystify the sushi-making process. Whether you're a novice or a seasoned chef, Mia's approachable style and insider tips make it easy to recreate the authentic flavors of Japan in your own kitchen.

                    Beyond the recipes, "Sushi Lover" delves into the history and significance of sushi, providing readers with a deeper understanding of the traditions that inspire each roll. Mia's personal anecdotes and cultural insights add a unique layer to the cookbook, transforming it into a celebration of not just food, but also the joy of sharing a meal with loved ones.

                    Prepare to embark on a culinary adventure that goes beyond taste, inviting you to savor the art, culture, and craftsmanship that define the world of sushi. With "Sushi Lover (Recipes)," Mia Takahashi invites you to bring the elegance and flavors of authentic Japanese sushi into your home, creating memorable dining experiences one roll at a time.
                    """,
                    510,
                    null,
                    authors.First(a => a.UserName.Value == "mia"),
                    "pic11.png",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "culinary"),
                    tags.First(t => t.Tag == "cultural"),
                    tags.First(t => t.Tag == "exploration"),
                    tags.First(t => t.Tag == "cookbook")),

                Book.Create(
                    "Moonwarden",
                    "moonwarden",
                    """
                    Embark on an enchanting odyssey through the realms of "Moonwarden," a spellbinding fantasy epic crafted by the imaginative mind of Lila Nightshade. Join protagonist Elara as she discovers her latent magical abilities and is thrust into a world teetering on the brink of celestial chaos. Against the backdrop of a moonlit kingdom, Elara must navigate political intrigue, ancient prophecies, and a rising darkness that threatens to engulf the land.

                    Lila Nightshade weaves a tapestry of vivid imagery, immersing readers in the ethereal landscapes of Moonwarden. The celestial magic pulsates through every page, casting a luminous glow on Elara's journey of self-discovery and the fate of her realm. As alliances shift like phases of the moon, Elara grapples with her destiny and the weight of the powers she wields.

                    "Moonwarden" is more than a fantasy tale; it's a poetic exploration of courage, sacrifice, and the enduring light that resides within even the darkest corners of the soul. Nightshade's prose dances like moonbeams, capturing the essence of a world where magic and destiny converge. With each turn of the page, readers are transported to a realm where the boundaries between reality and enchantment blur, and the moon becomes both witness and catalyst to the unfolding cosmic drama.
                    """,
                    1510,
                    null,
                    authors.First(a => a.UserName.Value == "lila"),
                    "pic12.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "fantasy"),
                    tags.First(t => t.Tag == "magical")),

                Book.Create(
                    "The Great Gatsby",
                    "the-great-gatsby",
                    """
                    Enter the dazzling world of 1920s New York in F. Scott Fitzgerald's timeless classic, "The Great Gatsby." Narrated by Nick Carraway, the novel unveils the extravagant and tumultuous life of Jay Gatsby, a mysterious millionaire with a penchant for lavish parties and an unrequited love. Through opulent prose and nuanced characters, Fitzgerald paints a portrait of the Jazz Age, exploring themes of wealth, illusion, and the elusive pursuit of the American Dream.

                    As the glittering façade of Gatsby's world unfolds, readers are drawn into a web of love triangles, societal expectations, and the consequences of relentless ambition. The green light at the end of Daisy Buchanan's dock becomes a symbol of unattainable aspirations, while Gatsby's mansion stands as a testament to the fragility of dreams in the face of reality.

                    "The Great Gatsby" transcends its era, offering a poignant commentary on the human condition and the pursuit of happiness. Fitzgerald's lyrical prose captures the essence of an era defined by excess and disillusionment, inviting readers to reflect on the timeless themes of love, identity, and the consequences of an unbridled obsession.
                    """,
                    2510,
                    null,
                    authors.First(a => a.UserName.Value == "scott"),
                    "pic13.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "fiction"),
                    tags.First(t => t.Tag == "classic")),

                Book.Create(
                    "After She Left",
                    "after-she-left",
                    """
                    Delve into the poignant exploration of love, loss, and the profound impact of choices in Michael Donovan's "After She Left." The novel unfolds the aftermath of a life-altering departure as protagonist Alex grapples with the void left by a loved one. In a narrative that traverses time and emotions, Donovan skillfully weaves a tale of self-discovery, redemption, and the intricate web of relationships that define our lives.

                    As Alex navigates the complexities of healing, readers are invited to witness the ripple effects of one person's departure on the lives intertwined with theirs. Donovan's evocative storytelling delves into the intricacies of grief, resilience, and the possibility of new beginnings. The narrative unfolds like a tapestry, revealing the interconnected stories of those left behind, each grappling with their own version of loss.

                    "After She Left" is a moving exploration of the human experience, where the echoes of the past reverberate through the present, shaping the characters' destinies. Donovan's prose, both tender and introspective, invites readers to contemplate the enduring power of love and the profound transformations that can arise in the wake of departure.
                    """,
                    580,
                    null,
                    authors.First(a => a.UserName.Value == "penelope"),
                    "pic14.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "contemporary"),
                    tags.First(t => t.Tag == "fiction"),
                    tags.First(t => t.Tag == "drama"),
                    tags.First(t => t.Tag == "romance")),

                Book.Create(
                    "Don't Look Back",
                    "dont-look-back",
                    """
                    Immerse yourself in the gripping mystery of "Don't Look Back" by Emily Harper, where the shadows of the past cast long, haunting echoes on the present. Protagonist Olivia finds herself entangled in a web of secrets and suspense when she returns to her hometown, a place she thought she left behind. As buried memories resurface, Olivia becomes a reluctant detective in her quest to unravel the truth, all while confronting the ghosts that linger in the corners of her past.

                    In this atmospheric narrative, Harper skillfully blends elements of psychological tension and familial drama. Each revelation peels back layers of Olivia's history, weaving a narrative that explores the delicate balance between truth and illusion. The haunting landscape of the small town becomes a character in itself, as dark secrets are unearthed, and the lines between reality and perception blur.

                    "Don't Look Back" is a masterclass in suspense, inviting readers to join Olivia on her journey through a labyrinth of memories and deceit. Harper's prose captivates, keeping readers on the edge of their seats as the mysteries intensify. With every page, the tension mounts, culminating in a climactic revelation that challenges Olivia's understanding of her own story.
                    """,
                    580,
                    null,
                    authors.First(a => a.UserName.Value == "nelson"),
                    "pic15.png",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "psychology"),
                    tags.First(t => t.Tag == "thriller"),
                    tags.First(t => t.Tag == "mystery"),
                    tags.First(t => t.Tag == "suspense")),

                Book.Create(
                    "The Land Beyond the Garden Wall",
                    "the-land-beyond-the-garden-wall",
                    """
                    Embark on a fantastical adventure into the enchanting world created by Isabella Rose in "The Land Beyond the Garden Wall." When young Emily stumbles upon a hidden passage in her family's garden, she discovers a magical realm brimming with wonders and perils. As she traverses this otherworldly land, Emily encounters mystical creatures, uncovers long-lost secrets, and learns that courage and compassion are her greatest allies.

                    Isabella Rose's evocative prose brings to life the rich tapestry of "The Land Beyond the Garden Wall." From vibrant meadows to ancient forests, readers are transported to a realm where imagination knows no bounds. The narrative unfolds like a fairy tale, with each chapter revealing new mysteries, friendships, and challenges that test Emily's resolve.

                    "The Land Beyond the Garden Wall" is more than a fantasy; it's a heartfelt exploration of self-discovery and the transformative power of belief. Rose crafts a narrative that resonates with readers of all ages, inviting them to embrace the magic within and embark on a journey where the ordinary becomes extraordinary.
                    """,
                    1080,
                    null,
                    authors.First(a => a.UserName.Value == "scott"),
                    "pic16.jpg",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    tags.First(t => t.Tag == "fantasy"),
                    tags.First(t => t.Tag == "fiction"),
                    tags.First(t => t.Tag == "adventure")),
            ];

            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }
    }
}
