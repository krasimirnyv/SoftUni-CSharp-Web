namespace BookShelf.Data.Configuration;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    private readonly Author[] authors =
    [
        new Author
        {
            Id = Guid.Parse("79f1f7bd-8745-4c39-b4a0-cfec65a33dce"),
            Name = "George Orwell"
        },

        new Author
        {
            Id = Guid.Parse("665a9d35-bfad-453a-a38d-67858d501bb1"),
            Name = "Fyodor Dostoevsky",
            Country = "Russia"
        },

        new Author
        {
            Id = Guid.Parse("66a2348d-b60c-4db6-ad02-b341f086892d"),
            Name = "Jane Austen",
            Country = "Britain"
        },

        new Author
        {
            Id = Guid.Parse("8e14aa56-8098-4a50-bcfd-e694f018c29a"),
            Name = "Ernest Hemingway",
            Country = "USA"
        },

        new Author
        {
            Id = Guid.Parse("5736a045-bd63-408c-a7eb-bdcaacf9e63e"),
            Name = "Gabriel García Márquez",
            Country = "Colombia"
        }
    ];
    
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .HasData(authors);
    }
}