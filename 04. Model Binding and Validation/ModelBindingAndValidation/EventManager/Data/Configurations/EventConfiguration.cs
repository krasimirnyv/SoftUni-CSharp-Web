namespace EventManager.Data.Configurations;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    private readonly Event[] events =
    [
        new()
        {
            Id = 1,
            Title = "ASP.NET Core Fundamentals Conference",
            Description = "A conference covering the fundamentals of ASP.NET Core MVC.",
            StartDate = new DateTime(2026, 03, 15, 00, 00, 00),
            EndDate = new DateTime(2026, 03, 16, 00, 00, 00),
            MaxParticipants = 300,
            CategoryId = 1
        },

        new()
        {
            Id = 2,
            Title = "Modern Web Development Conference",
            Description = "Topics: MVC, REST, validation, and security basics for web apps.",
            StartDate = new DateTime(2026, 05, 20, 00, 00, 00),
            EndDate = new DateTime(2026, 05, 21, 00, 00, 00),
            MaxParticipants = 400,
            CategoryId = 1
        },

        new()
        {
            Id = 3,
            Title = "Model Binding and Validation Workshop",
            Description = "Hands-on workshop focused on model binding and validation.",
            StartDate = new DateTime(2026, 04, 10, 00, 00, 00),
            EndDate = new DateTime(2026, 04, 10, 00, 00, 00),
            MaxParticipants = 40,
            CategoryId = 2
        },

        new()
        {
            Id = 4,
            Title = "Razor Forms Workshop",
            Description = "Build forms with tag helpers and display validation messages properly.",
            StartDate = new DateTime(2026, 04, 24, 00, 00, 00),
            EndDate = new DateTime(2026, 04, 24, 00, 00, 00),
            MaxParticipants = 35,
            CategoryId = 2
        },

        new()
        {
            Id = 5,
            Title = "Clean Controllers Seminar",
            Description = "How to keep controller actions small and predictable with ModelState.",
            StartDate = new DateTime(2026, 06, 05, 00, 00, 00),
            EndDate = new DateTime(2026, 06, 05, 00, 00, 00),
            MaxParticipants = 120,
            CategoryId = 3
        },

        new()
        {
            Id = 6,
            Title = "Validation Best Practices Seminar",
            Description = "Server-side validation patterns and common mistakes in MVC apps.",
            StartDate = new DateTime(2026, 06, 12, 00, 00, 00),
            EndDate = new DateTime(2026, 06, 12, 00, 00, 00),
            MaxParticipants = 120,
            CategoryId = 3
        },

        new()
        {
            Id = 7,
            Title = "EF Core Essentials Training",
            Description = "DbContext, migrations, relationships, and seeding essentials.",
            StartDate = new DateTime(2026, 02, 17, 00, 00, 00),
            EndDate = new DateTime(2026, 02, 18, 00, 00, 00),
            MaxParticipants = 80,
            CategoryId = 4
        },

        new()
        {
            Id = 8,
            Title = "Testing MVC Forms Training",
            Description = "Practice form submissions, invalid model states, and error rendering.",
            StartDate = new DateTime(2026, 07, 07, 00, 00, 00),
            EndDate = new DateTime(2026, 07, 08, 00, 00, 00),
            MaxParticipants = 70,
            CategoryId = 4
        },

        new()
        {
            Id = 9,
            Title = "Sofia .NET Meetup",
            Description = "Community meetup: mini talks and networking for .NET developers.",
            StartDate = new DateTime(2026, 03, 28, 00, 00, 00),
            EndDate = new DateTime(2026, 03, 28, 00, 00, 00),
            MaxParticipants = 150,
            CategoryId = 5
        },

        new()
        {
            Id = 10,
            Title = "Student Projects Meetup",
            Description = "Students present their projects and discuss common issues and fixes.",
            StartDate = new DateTime(2026, 05, 09, 00, 00, 00),
            EndDate = new DateTime(2026, 05, 09, 00, 00, 00),
            MaxParticipants = 150,
            CategoryId = 5
        },

        new()
        {
            Id = 11,
            Title = "MVC Mini Hackathon",
            Description = "Build a small MVC app with validation rules under time constraints.",
            StartDate = new DateTime(2026, 08, 01, 00, 00, 00),
            EndDate = new DateTime(2026, 08, 02, 00, 00, 00),
            MaxParticipants = 60,
            CategoryId = 6
        }
    ];
    
    
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder
            .HasData(events);
    }
}