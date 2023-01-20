using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskfinal.Migrations
{
    /// <inheritdoc />
    public partial class migration07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "Accounts",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    ShoppingProductListID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingProductLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingListID = table.Column<int>(type: "int", nullable: false),
                    ShoppingProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingProductLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShoppingProductLists_ShoppingLists_ShoppingListID",
                        column: x => x.ShoppingListID,
                        principalTable: "ShoppingLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingProductListID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
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
                        name: "FK_ShoppingProducts_ShoppingProductLists_ShoppingProductListID",
                        column: x => x.ShoppingProductListID,
                        principalTable: "ShoppingProductLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProductLists_ShoppingListID",
                table: "ShoppingProductLists",
                column: "ShoppingListID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProducts_ProductID",
                table: "ShoppingProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProducts_ShoppingProductListID",
                table: "ShoppingProducts",
                column: "ShoppingProductListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingProducts");

            migrationBuilder.DropTable(
                name: "ShoppingProductLists");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Accounts",
                newName: "AccountName");
        }
    }
}
