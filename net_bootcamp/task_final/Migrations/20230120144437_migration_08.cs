using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskfinal.Migrations
{
    /// <inheritdoc />
    public partial class migration08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingProducts");

            migrationBuilder.RenameColumn(
                name: "ShoppingProductID",
                table: "ShoppingProductLists",
                newName: "ProductID");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ShoppingProductLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProductLists_ProductID",
                table: "ShoppingProductLists",
                column: "ProductID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingProductLists_Products_ProductID",
                table: "ShoppingProductLists",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingProductLists_Products_ProductID",
                table: "ShoppingProductLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingProductLists_ProductID",
                table: "ShoppingProductLists");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShoppingProductLists");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ShoppingProductLists",
                newName: "ShoppingProductID");

            migrationBuilder.CreateTable(
                name: "ShoppingProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ShoppingProductListID = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_ShoppingProducts_ProductID",
                table: "ShoppingProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingProducts_ShoppingProductListID",
                table: "ShoppingProducts",
                column: "ShoppingProductListID");
        }
    }
}
