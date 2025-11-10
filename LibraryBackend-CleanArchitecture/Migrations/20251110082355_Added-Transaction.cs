using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend_CleanArchitecture.Migrations
{
    /// <inheritdoc />
    public partial class AddedTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameColumn(
                name: "Booktitle",
                table: "Transaction",
                newName: "BookTitle");

            migrationBuilder.RenameColumn(
                name: "MemeberName",
                table: "Transaction",
                newName: "MemberName");

            migrationBuilder.RenameColumn(
                name: "IssueStatus",
                table: "Transaction",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Categories",
                table: "Books",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameColumn(
                name: "BookTitle",
                table: "Transactions",
                newName: "Booktitle");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Transactions",
                newName: "IssueStatus");

            migrationBuilder.RenameColumn(
                name: "MemberName",
                table: "Transactions",
                newName: "MemeberName");

            migrationBuilder.AlterColumn<string>(
                name: "Categories",
                table: "Books",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");
        }
    }
}
