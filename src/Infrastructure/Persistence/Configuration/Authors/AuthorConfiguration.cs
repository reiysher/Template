using Domain.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Authors;

internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(author => author.Id);
        builder.ToTable("authors");

        builder.Property(author => author.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.OwnsOne(author => author.FullName, fullNameBuilder =>
        {
            fullNameBuilder.Property(fn => fn.FirstName)
                .HasColumnName("first_name");

            fullNameBuilder.Property(fn => fn.LastName)
                .HasColumnName("last_name");

            fullNameBuilder.Property(fn => fn.MiddleName)
                .HasColumnName("middle_name");
        });
    }
}
