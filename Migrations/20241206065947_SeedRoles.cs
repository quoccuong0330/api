using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09f276bb-980e-49ec-aeac-8c571e8a3120");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "523e8672-4c63-45f5-b2eb-7da3ca71e0a6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34e04342-8260-4f0a-9ffe-0b62f154747a", null, "Alice", "ADMIN" },
                    { "393fb099-4b28-46bd-88fc-8815295a6f33", null, "Rai", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34e04342-8260-4f0a-9ffe-0b62f154747a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "393fb099-4b28-46bd-88fc-8815295a6f33");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09f276bb-980e-49ec-aeac-8c571e8a3120", null, "Alice", "ADMIN" },
                    { "523e8672-4c63-45f5-b2eb-7da3ca71e0a6", null, "Rai", "USER" }
                });
        }
    }
}
