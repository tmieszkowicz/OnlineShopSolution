using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class Test_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "TEST");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "A kit provided by Glossier, containing skin care, hair care and makeup products");
        }
    }
}
