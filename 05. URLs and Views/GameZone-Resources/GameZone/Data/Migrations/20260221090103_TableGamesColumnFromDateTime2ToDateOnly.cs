using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameZone.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableGamesColumnFromDateTime2ToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReleasedOn",
                table: "Games",
                type: "DATE",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleasedOn",
                value: new DateOnly(1998, 3, 31));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleasedOn",
                value: new DateOnly(2013, 9, 17));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReleasedOn",
                value: new DateOnly(2002, 9, 27));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleasedOn",
                table: "Games",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "DATE");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReleasedOn",
                value: new DateTime(1998, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReleasedOn",
                value: new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReleasedOn",
                value: new DateTime(2002, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
