using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskfinal.Migrations
{
    /// <inheritdoc />
    public partial class migration14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "ShoppingLists",
                newName: "TotalCost");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "ShoppingProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingProducts");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "ShoppingLists",
                newName: "TotalPrice");
        }
    }
}
