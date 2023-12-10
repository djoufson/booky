using Catalog.API.Models;
using Catalog.API.Models.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Infra.Configurations;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder
            .Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => new BookId(value)
            );

        builder
            .Property(a => a.AuthorId)
            .HasConversion(
                id => id.Value,
                value => new AuthorId(value)
            );
    }
}
