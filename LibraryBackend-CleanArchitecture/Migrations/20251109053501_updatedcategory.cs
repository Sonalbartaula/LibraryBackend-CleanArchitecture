using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend_CleanArchitecture.Migrations
{
    /// <inheritdoc />
    public partial class updatedcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Books",
                newName: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Categories",
                table: "Books",
                newName: "Category");
        }
    }
}
