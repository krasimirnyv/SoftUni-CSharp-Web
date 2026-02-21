#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameZone.Data.Migrations
{
    using System;
    
    using Microsoft.EntityFrameworkCore.Migrations;
    
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    PublisherName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ReleasedOn = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "Fighting" },
                    { 4, "Sports" },
                    { 5, "Racing" },
                    { 6, "Strategy" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GenreId", "ImageUrl", "PublisherName", "ReleasedOn", "Title" },
                values: new object[,]
                {
                    { 1, "A legendary real-time strategy game that defined competitive esports.", 6, "https://www.pcguide.com/wp-content/uploads/2022/07/Starcraft-2-system-requirements.jpg", "Blizzard Entertainment", new DateTime(1998, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "StarCraft" },
                    { 2, "An open-world action game offering freedom, crime, and unforgettable characters.", 1, "https://w0.peakpx.com/wallpaper/409/681/HD-wallpaper-grand-theft-auto-grand-theft-auto-v.jpg", "Rockstar Games", new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V" },
                    { 3, "A realistic rally racing game focused on precision, terrain, and driving skill.", 5, "https://www.motorsportmagazine.com/wp-content/uploads/2025/02/Scalextric-Colin-McRae-800x450.jpg", "Codemasters", new DateTime(2002, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Colin McRae Rally 5" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GenreId",
                table: "Games",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
