using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class MyInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Price" },
                values: new object[] { "A kit provided by Glossier, containing skin care, hair care and makeup products", 100m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Price" },
                values: new object[] { "TEST", 50m });
        }
    }
}
