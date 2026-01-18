#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GarageApp.Migrations
{
    using System;
    
    using Microsoft.EntityFrameworkCore.Migrations;
    
    /// <inheritdoc />
    public partial class SeedInitialCarsAndGarages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GarageApp",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("5ae2fdda-84dd-4d9d-a09e-e4b180e507c5"), "Sofia, Balkan Star North", "Balkan Star North Garage" },
                    { new Guid("a0556d26-3224-4568-93e4-8ba997308f04"), "Sofia Centre", "Sofia Central Garage" },
                    { new Guid("bb0ef97b-d3a7-4a57-b39b-98c15ee5758c"), "Sofia, Porsche Sofia West", "Porsche Sofia West Garage" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BodyStyle", "GarageId", "IsAvailable", "Make", "Model", "Year" },
                values: new object[,]
                {
                    { new Guid("78ca1339-45e9-4585-8d7e-ea699e6488c2"), 8, new Guid("a0556d26-3224-4568-93e4-8ba997308f04"), true, "Mazda", "CX-5", 2022 },
                    { new Guid("9623f701-ac46-4a9a-9863-0831190da302"), 3, new Guid("bb0ef97b-d3a7-4a57-b39b-98c15ee5758c"), false, "Honda", "Civic", 2018 },
                    { new Guid("9c048e12-c131-4a5e-85af-c0d374bbe04b"), 2, new Guid("bb0ef97b-d3a7-4a57-b39b-98c15ee5758c"), true, "Ford", "Focus", 2016 },
                    { new Guid("aa701bd7-c722-42db-a393-3f1b41f9bdba"), 1, new Guid("a0556d26-3224-4568-93e4-8ba997308f04"), false, "Audi", "A4", 2018 },
                    { new Guid("ca9d2135-e400-4684-9eb5-5232c466f428"), 5, new Guid("5ae2fdda-84dd-4d9d-a09e-e4b180e507c5"), true, "Skoda", "Octavia", 2019 },
                    { new Guid("d4d2dab1-a742-4c8a-bb63-76cc86f016ac"), 1, new Guid("5ae2fdda-84dd-4d9d-a09e-e4b180e507c5"), true, "Toyota", "Corolla", 2021 },
                    { new Guid("d542460e-235c-435d-8a6a-30918ce3a8a5"), 5, new Guid("a0556d26-3224-4568-93e4-8ba997308f04"), true, "BMW", "320d", 2007 },
                    { new Guid("dc266079-c3ac-4046-a672-a3c0b0bc6975"), 2, new Guid("5ae2fdda-84dd-4d9d-a09e-e4b180e507c5"), true, "Volkswagen", "Golf", 2020 },
                    { new Guid("ddb7b8fb-a81d-44e5-a2eb-7d748e090152"), 1, new Guid("bb0ef97b-d3a7-4a57-b39b-98c15ee5758c"), false, "Mercedes-Benz", "C220", 2017 },
                    { new Guid("f93dc41c-45a9-40b4-b4b6-bc720ada9c29"), 8, new Guid("a0556d26-3224-4568-93e4-8ba997308f04"), true, "Hyundai", "Tucson", 2023 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("78ca1339-45e9-4585-8d7e-ea699e6488c2"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("9623f701-ac46-4a9a-9863-0831190da302"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("9c048e12-c131-4a5e-85af-c0d374bbe04b"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("aa701bd7-c722-42db-a393-3f1b41f9bdba"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("ca9d2135-e400-4684-9eb5-5232c466f428"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("d4d2dab1-a742-4c8a-bb63-76cc86f016ac"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("d542460e-235c-435d-8a6a-30918ce3a8a5"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("dc266079-c3ac-4046-a672-a3c0b0bc6975"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("ddb7b8fb-a81d-44e5-a2eb-7d748e090152"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("f93dc41c-45a9-40b4-b4b6-bc720ada9c29"));

            migrationBuilder.DeleteData(
                table: "GarageApp",
                keyColumn: "Id",
                keyValue: new Guid("5ae2fdda-84dd-4d9d-a09e-e4b180e507c5"));

            migrationBuilder.DeleteData(
                table: "GarageApp",
                keyColumn: "Id",
                keyValue: new Guid("a0556d26-3224-4568-93e4-8ba997308f04"));

            migrationBuilder.DeleteData(
                table: "GarageApp",
                keyColumn: "Id",
                keyValue: new Guid("bb0ef97b-d3a7-4a57-b39b-98c15ee5758c"));
        }
    }
}
