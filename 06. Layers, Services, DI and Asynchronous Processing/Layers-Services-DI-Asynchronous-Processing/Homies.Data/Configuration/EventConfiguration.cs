namespace Homies.Data.Configuration;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> entity)
    {
        entity
            .HasMany(e => e.Participants)
            .WithMany()
            .UsingEntity<EventParticipant>(
                ep => ep
                    .HasOne(ep => ep.Helper)
                    .WithMany()
                    .HasForeignKey(ep => ep.HelperId)
                    .OnDelete(DeleteBehavior.Restrict),
                ep => ep
                    .HasOne(ep => ep.Event)
                    .WithMany(e => e.EventsParticipants)
                    .HasForeignKey(ep => ep.EventId)
                    .OnDelete(DeleteBehavior.Restrict),
                ep =>
                {
                    ep.HasKey(e => new { e.HelperId, e.EventId });
                });
    }
}