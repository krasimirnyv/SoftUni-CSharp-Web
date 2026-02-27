namespace Homies.Data.Configuration;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TypeConfiguration : IEntityTypeConfiguration<EventType>
{
    private readonly EventType[] eventTypes =
    [
        new()
        {
            Id = 1,
            Name = "Animals"
        },
        new()
        {
            Id = 2,
            Name = "Fun"
        },
        new()
        {
            Id = 3,
            Name = "Discussion"
        },
        new()
        {
            Id = 4,
            Name = "Work"
        }
    ];
    
    public void Configure(EntityTypeBuilder<EventType> entity)
    {
        entity.HasData(eventTypes);
    }
}