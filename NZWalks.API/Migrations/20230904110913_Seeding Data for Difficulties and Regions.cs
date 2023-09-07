using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8d69b141-ba8b-443d-9168-cdeb365abb50"), "Medium" },
                    { new Guid("ac690f97-6a2d-401e-b8bf-29be66455e11"), "Easy" },
                    { new Guid("e63932c7-3a93-4cce-b6f5-6c5f79bc11df"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0f5e69f2-3ae8-41e9-a9cf-36ccad797389"), "NSN", "Nelson", "Nelson.jpg" },
                    { new Guid("1ce9a54a-c3f0-4c8d-9ff6-c509cb0c3113"), "STL", "SouthLand", "SouthLand.jpg" },
                    { new Guid("77f7f756-f8e9-4c0e-9432-390bee46215a"), "AKL", "AuckLand", "AuckLand.jpg" },
                    { new Guid("ed40e45b-ca5e-4452-b9f4-0ea688c65326"), "WGN", "Weelington", "Weelington.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8d69b141-ba8b-443d-9168-cdeb365abb50"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ac690f97-6a2d-401e-b8bf-29be66455e11"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("e63932c7-3a93-4cce-b6f5-6c5f79bc11df"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0f5e69f2-3ae8-41e9-a9cf-36ccad797389"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1ce9a54a-c3f0-4c8d-9ff6-c509cb0c3113"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("77f7f756-f8e9-4c0e-9432-390bee46215a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ed40e45b-ca5e-4452-b9f4-0ea688c65326"));
        }
    }
}
