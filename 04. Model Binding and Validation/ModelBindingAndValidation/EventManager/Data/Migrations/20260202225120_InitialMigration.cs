using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Conference" },
                    { 2, "Workshop" },
                    { 3, "Seminar" },
                    { 4, "Training" },
                    { 5, "Meetup" },
                    { 6, "Hackathon" },
                    { 7, "Webinar" },
                    { 8, "Bootcamp" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CategoryId", "Description", "EndDate", "MaxParticipants", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, "A conference covering the fundamentals of ASP.NET Core MVC.", new DateTime(2026, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 300, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core Fundamentals Conference" },
                    { 2, 1, "Topics: MVC, REST, validation, and security basics for web apps.", new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 400, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modern Web Development Conference" },
                    { 3, 2, "Hands-on workshop focused on model binding and validation.", new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Model Binding and Validation Workshop" },
                    { 4, 2, "Build forms with tag helpers and display validation messages properly.", new DateTime(2026, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, new DateTime(2026, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Razor Forms Workshop" },
                    { 5, 3, "How to keep controller actions small and predictable with ModelState.", new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clean Controllers Seminar" },
                    { 6, 3, "Server-side validation patterns and common mistakes in MVC apps.", new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Validation Best Practices Seminar" },
                    { 7, 4, "DbContext, migrations, relationships, and seeding essentials.", new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 80, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "EF Core Essentials Training" },
                    { 8, 4, "Practice form submissions, invalid model states, and error rendering.", new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 70, new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testing MVC Forms Training" },
                    { 9, 5, "Community meetup: mini talks and networking for .NET developers.", new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 150, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sofia .NET Meetup" },
                    { 10, 5, "Students present their projects and discuss common issues and fixes.", new DateTime(2026, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 150, new DateTime(2026, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Student Projects Meetup" },
                    { 11, 6, "Build a small MVC app with validation rules under time constraints.", new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MVC Mini Hackathon" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
