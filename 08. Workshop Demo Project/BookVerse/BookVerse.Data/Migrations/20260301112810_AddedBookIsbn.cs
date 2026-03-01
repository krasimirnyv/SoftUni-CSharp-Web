using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookIsbn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
            //     columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //     values: new object[] { "7c9ea5af-895f-4ea5-b866-11e4dfcf6a66", "AQAAAAIAAYagAAAAEIZ4w5wFNrZ9RtHlN3YYQno+R9iett4XpCNrrOL08a3b0Pqw0rH8jvsjdKzZeh/ryA==", "e5fdc58c-7c17-4d61-bf21-b8c4a53fdf3d" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Isbn",
                value: "9783127323207");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Isbn",
                value: "9781234567897");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Isbn",
                value: "9782123456803");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "Books");

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
            //     columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //     values: new object[] { "5fb56ef3-d6f8-4380-b44b-6b5135ffaa19", "AQAAAAIAAYagAAAAENuWq3mQDZzSwTw34aq/A3qHr/Ktag0SzgYQOqm/gJXGq0FSxXsX2wGO2grHi5Wn+w==", "c12fe3d2-0350-483a-91c0-90d7a6ac7672" });
        }
    }
}
