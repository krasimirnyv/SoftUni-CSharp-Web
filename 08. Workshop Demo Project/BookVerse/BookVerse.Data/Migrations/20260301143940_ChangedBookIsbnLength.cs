using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedBookIsbnLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
            //     columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //     values: new object[] { "ac5696ec-bf67-474d-b541-33fc51110217", "AQAAAAIAAYagAAAAEFxbk9SKXoT7AsaA8nWBdj1z9ODsdrEdIyNViNmC3ZEnikzVfQXTblAkCboRxDXQWw==", "db085aab-e45a-4a66-b9d8-22d8dbf20323" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            // migrationBuilder.UpdateData(
            //     table: "AspNetUsers",
            //     keyColumn: "Id",
            //     keyValue: "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
            //     columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //     values: new object[] { "7c9ea5af-895f-4ea5-b866-11e4dfcf6a66", "AQAAAAIAAYagAAAAEIZ4w5wFNrZ9RtHlN3YYQno+R9iett4XpCNrrOL08a3b0Pqw0rH8jvsjdKzZeh/ryA==", "e5fdc58c-7c17-4d61-bf21-b8c4a53fdf3d" });
        }
    }
}
