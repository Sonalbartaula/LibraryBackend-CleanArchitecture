using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend_CleanArchitecture.Migrations
{
    /// <inheritdoc />
    public partial class editedaddedbooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemberName",
                table: "Activities",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Activities",
                newName: "Subtitle");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Activities",
                newName: "MemberName");

            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "Activities",
                newName: "Description");
        }
    }
}
