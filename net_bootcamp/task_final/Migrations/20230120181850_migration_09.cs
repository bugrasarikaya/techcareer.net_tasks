using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskfinal.Migrations
{
    /// <inheritdoc />
    public partial class migration09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingProductLists");

            migrationBuilder.DropColumn(
                name: "ShoppingProductListID",
                table: "ShoppingLists");

            migrationBuilder.CreateTable(
                name: "ShoppingProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingListID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShoppingProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingProducts_ShoppingLists_ShoppingListID",
                        column: x => x.ShoppingListID,
                        principalTable: "ShoppingLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProducts_ProductID",
                table: "ShoppingProducts",
                column: "ProductID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProducts_ShoppingListID",
                table: "ShoppingProducts",
                column: "ShoppingListID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingProducts");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingProductListID",
                table: "ShoppingLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShoppingProductLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ShoppingListID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingProductLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShoppingProductLists_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingProductLists_ShoppingLists_ShoppingListID",
                        column: x => x.ShoppingListID,
                        principalTable: "ShoppingLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProductLists_ProductID",
                table: "ShoppingProductLists",
                column: "ProductID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProductLists_ShoppingListID",
                table: "ShoppingProductLists",
                column: "ShoppingListID",
                unique: true);
        }
    }
}
