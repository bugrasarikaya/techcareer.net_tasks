using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskfinal.Migrations
{
    /// <inheritdoc />
    public partial class migration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingProducts_Products_ProductID",
                table: "ShoppingProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingProducts_ShoppingLists_ShoppingListID",
                table: "ShoppingProducts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingProducts_ProductID",
                table: "ShoppingProducts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingProducts_ShoppingListID",
                table: "ShoppingProducts");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImageID",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageID",
                table: "Products",
                column: "ImageID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageID",
                table: "Products",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingProducts_Products_ProductID",
                table: "ShoppingProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingProducts_ShoppingLists_ShoppingListID",
                table: "ShoppingProducts",
                column: "ShoppingListID",
                principalTable: "ShoppingLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
