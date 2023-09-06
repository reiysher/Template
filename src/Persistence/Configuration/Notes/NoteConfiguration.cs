using Domain.Authors;
using Domain.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration.Notes;

internal class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(note => note.Id);
        builder.ToTable("notes");

        builder.Property(note => note.Id)
            .ValueGeneratedNever()
            .IsRequired()
            .HasConversion(
                noteId => noteId.Value,
                value => new NoteId(value));

        builder.Property(note => note.AuthorId)
            .IsRequired();
    }
}
