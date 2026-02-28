using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityNotes.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;

public class NoteEntityConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> entity)
    {
        entity
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}