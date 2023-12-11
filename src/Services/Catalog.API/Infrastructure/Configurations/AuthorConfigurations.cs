using Catalog.API.Models;
using Catalog.API.Models.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Infrastructure.Configurations;

internal class AuthorConfigurations : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);
        builder
            .Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => new AuthorId(value)
            );

        builder
            .Property(a => a.UserName)
            .HasConversion(u => u.Value, value => new UserName(value));

        builder
            .Property(a => a.Email)
            .HasConversion(email => email.Value, value => new Email(value));

        builder.OwnsOne(u => u.Name, name =>
        {
            name
                .Property(n => n.First)
                .HasColumnName("FirstName");
            name
                .Property(n => n.Last)
                .HasColumnName("LastName");
        });
    }
}
