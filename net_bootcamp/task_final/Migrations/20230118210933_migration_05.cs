using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskfinal.Migrations
{
    /// <inheritdoc />
    public partial class migration05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BinaryImage",
                table: "Images",
                newName: "Binary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Binary",
                table: "Images",
                newName: "BinaryImage");
        }
    }
}
