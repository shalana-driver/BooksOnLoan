using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksOnLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Returned",
                table: "Transaction",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 1,
                column: "Returned",
                value: false);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 2,
                column: "Returned",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Returned",
                table: "Transaction");
        }
    }
}
