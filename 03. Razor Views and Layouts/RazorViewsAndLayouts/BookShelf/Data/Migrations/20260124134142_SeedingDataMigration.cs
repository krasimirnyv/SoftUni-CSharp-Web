using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookShelf.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("5736a045-bd63-408c-a7eb-bdcaacf9e63e"), "Colombia", "Gabriel García Márquez" },
                    { new Guid("665a9d35-bfad-453a-a38d-67858d501bb1"), "Russia", "Fyodor Dostoevsky" },
                    { new Guid("66a2348d-b60c-4db6-ad02-b341f086892d"), "Britain", "Jane Austen" },
                    { new Guid("79f1f7bd-8745-4c39-b4a0-cfec65a33dce"), null, "George Orwell" },
                    { new Guid("8e14aa56-8098-4a50-bcfd-e694f018c29a"), "USA", "Ernest Hemingway" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Title", "Year" },
                values: new object[,]
                {
                    { new Guid("35483dcf-685b-4ac1-ae01-e02638ecd5f0"), new Guid("5736a045-bd63-408c-a7eb-bdcaacf9e63e"), "One Hundred Years of Solitude", 1967 },
                    { new Guid("5d058393-fce5-459b-8cad-0873e9704411"), new Guid("8e14aa56-8098-4a50-bcfd-e694f018c29a"), "The Old Man and the Sea", 1952 },
                    { new Guid("661ee1c6-e927-429a-a225-c90339a41717"), new Guid("79f1f7bd-8745-4c39-b4a0-cfec65a33dce"), "1984", 1949 },
                    { new Guid("91eb90d5-72ec-4362-91d5-db0488e466ca"), new Guid("79f1f7bd-8745-4c39-b4a0-cfec65a33dce"), "Animal Farm", 1945 },
                    { new Guid("a597ec9c-c3ae-4d91-b62b-0df4c9046892"), new Guid("665a9d35-bfad-453a-a38d-67858d501bb1"), "Crime and Punishment", 1866 },
                    { new Guid("e20b5bc9-b1d1-43bc-9ba2-cd5c1f8df9d6"), new Guid("665a9d35-bfad-453a-a38d-67858d501bb1"), "The Brothers Karamazov", 1880 },
                    { new Guid("e7acaae5-0acb-4b1b-a818-447a19373862"), new Guid("5736a045-bd63-408c-a7eb-bdcaacf9e63e"), "Love in the Time of Cholera", 1985 },
                    { new Guid("e7de82ed-bbdf-4b85-aa1a-56ff33f415ab"), new Guid("66a2348d-b60c-4db6-ad02-b341f086892d"), "Pride and Prejudice", 1813 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("35483dcf-685b-4ac1-ae01-e02638ecd5f0"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5d058393-fce5-459b-8cad-0873e9704411"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("661ee1c6-e927-429a-a225-c90339a41717"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("91eb90d5-72ec-4362-91d5-db0488e466ca"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("a597ec9c-c3ae-4d91-b62b-0df4c9046892"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("e20b5bc9-b1d1-43bc-9ba2-cd5c1f8df9d6"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("e7acaae5-0acb-4b1b-a818-447a19373862"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("e7de82ed-bbdf-4b85-aa1a-56ff33f415ab"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("5736a045-bd63-408c-a7eb-bdcaacf9e63e"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("665a9d35-bfad-453a-a38d-67858d501bb1"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("66a2348d-b60c-4db6-ad02-b341f086892d"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("79f1f7bd-8745-4c39-b4a0-cfec65a33dce"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("8e14aa56-8098-4a50-bcfd-e694f018c29a"));
        }
    }
}
