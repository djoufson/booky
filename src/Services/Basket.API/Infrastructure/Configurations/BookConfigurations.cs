using Basket.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.API.Infrastructure.Configurations;

internal class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasIndex(b => b.Slug);
        builder.HasIndex(b => b.Author);
        builder.HasIndex(b => b.ExternalId);
    }
}
