namespace EventManager.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private readonly Category[] categories =
    [
        new() { Id = 1, Name = "Conference" },
        new() { Id = 2, Name = "Workshop" },
        new() { Id = 3, Name = "Seminar" },
        new() { Id = 4, Name = "Training" },
        new() { Id = 5, Name = "Meetup" },
        new() { Id = 6, Name = "Hackathon" },
        new() { Id = 7, Name = "Webinar" },
        new() { Id = 8, Name = "Bootcamp" }
    ];
    
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasData(categories);
    }
}