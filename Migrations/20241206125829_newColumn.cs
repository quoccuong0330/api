using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class newColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e4c4112-007f-41a3-8ea1-d1e9617c9696");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5000135-bd2c-4748-9e97-ff681ca6194c");

            migrationBuilder.AddColumn<string>(
                name: "userID",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30bfefe0-0cfa-4ddd-95f4-19638f76c8f3", null, "Alice", "ADMIN" },
                    { "44b7e4bf-5781-4d79-94a9-7bda943d28de", null, "Rai", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_userID",
                table: "Comments",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_userID",
                table: "Comments",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_userID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_userID",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30bfefe0-0cfa-4ddd-95f4-19638f76c8f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44b7e4bf-5781-4d79-94a9-7bda943d28de");

            migrationBuilder.DropColumn(
                name: "userID",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e4c4112-007f-41a3-8ea1-d1e9617c9696", null, "Alice", "ADMIN" },
                    { "b5000135-bd2c-4748-9e97-ff681ca6194c", null, "Rai", "USER" }
                });
        }
    }
}
