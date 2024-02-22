using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Migrations
{
    /// <inheritdoc />
    public partial class ReadyPlayerOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_category",
                columns: table => new
                {
                    idCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_category", x => x.idCategory);
                });

            migrationBuilder.CreateTable(
                name: "tbl_console",
                columns: table => new
                {
                    idConsole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_console", x => x.idConsole);
                });

            migrationBuilder.CreateTable(
                name: "tbl_product",
                columns: table => new
                {
                    idProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idCategory = table.Column<int>(type: "int", nullable: false),
                    idConsole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_product", x => x.idProduct);
                    table.ForeignKey(
                        name: "FK_tbl_product_tbl_category_idCategory",
                        column: x => x.idCategory,
                        principalTable: "tbl_category",
                        principalColumn: "idCategory",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_product_tbl_console_idConsole",
                        column: x => x.idConsole,
                        principalTable: "tbl_console",
                        principalColumn: "idConsole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_idCategory",
                table: "tbl_product",
                column: "idCategory");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_idConsole",
                table: "tbl_product",
                column: "idConsole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_product");

            migrationBuilder.DropTable(
                name: "tbl_category");

            migrationBuilder.DropTable(
                name: "tbl_console");
        }
    }
}
