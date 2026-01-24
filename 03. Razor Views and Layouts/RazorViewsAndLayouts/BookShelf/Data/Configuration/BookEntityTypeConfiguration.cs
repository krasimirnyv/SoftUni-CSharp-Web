namespace BookShelf.Data.Configuration;

using Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    private readonly Book[] books =
    [
        // George Orwell
        new Book
        {
            Id = Guid.Parse("661ee1c6-e927-429a-a225-c90339a41717"),
            Title = "1984",
            Year = 1949,
            AuthorId = Guid.Parse("79f1f7bd-8745-4c39-b4a0-cfec65a33dce")
        },
        new Book
        {
            Id = Guid.Parse("91eb90d5-72ec-4362-91d5-db0488e466ca"),
            Title = "Animal Farm",
            Year = 1945,
            AuthorId = Guid.Parse("79f1f7bd-8745-4c39-b4a0-cfec65a33dce")
        },

        // Fyodor Dostoevsky
        new Book
        {
            Id = Guid.Parse("a597ec9c-c3ae-4d91-b62b-0df4c9046892"),
            Title = "Crime and Punishment",
            Year = 1866,
            AuthorId = Guid.Parse("665a9d35-bfad-453a-a38d-67858d501bb1")
        },
        new Book
        {
            Id = Guid.Parse("e20b5bc9-b1d1-43bc-9ba2-cd5c1f8df9d6"),
            Title = "The Brothers Karamazov",
            Year = 1880,
            AuthorId = Guid.Parse("665a9d35-bfad-453a-a38d-67858d501bb1")
        },

        // Jane Austen
        new Book
        {
            Id = Guid.Parse("e7de82ed-bbdf-4b85-aa1a-56ff33f415ab"),
            Title = "Pride and Prejudice",
            Year = 1813,
            AuthorId = Guid.Parse("66a2348d-b60c-4db6-ad02-b341f086892d")
        },

        // Ernest Hemingway
        new Book
        {
            Id = Guid.Parse("5d058393-fce5-459b-8cad-0873e9704411"),
            Title = "The Old Man and the Sea",
            Year = 1952,
            AuthorId = Guid.Parse("8e14aa56-8098-4a50-bcfd-e694f018c29a")
        },

        // Gabriel García Márquez
        new Book
        {
            Id = Guid.Parse("35483dcf-685b-4ac1-ae01-e02638ecd5f0"),
            Title = "One Hundred Years of Solitude",
            Year = 1967,
            AuthorId = Guid.Parse("5736a045-bd63-408c-a7eb-bdcaacf9e63e")
        },
        new Book
        {
            Id = Guid.Parse("e7acaae5-0acb-4b1b-a818-447a19373862"),
            Title = "Love in the Time of Cholera",
            Year = 1985,
            AuthorId = Guid.Parse("5736a045-bd63-408c-a7eb-bdcaacf9e63e")
        }
    ];
    
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .HasData(books);
    }
}