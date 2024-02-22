using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    /// <inheritdoc />
    public partial class segundaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productName",
                table: "tbl_product",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "productDescription",
                table: "tbl_product",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "consoleName",
                table: "tbl_console",
                newName: "ConsoleName");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tbl_product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tbl_product");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "tbl_product",
                newName: "productName");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "tbl_product",
                newName: "productDescription");

            migrationBuilder.RenameColumn(
                name: "ConsoleName",
                table: "tbl_console",
                newName: "consoleName");
        }
    }
}
