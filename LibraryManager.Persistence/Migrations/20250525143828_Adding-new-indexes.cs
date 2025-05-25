using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addingnewindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Loan_UserId",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_BookCategory_BookId",
                table: "BookCategory");

            migrationBuilder.CreateIndex(
                name: "IX_User_Document",
                table: "User",
                column: "Document",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loan_UserId_BookId_LoanStatus",
                table: "Loan",
                columns: new[] { "UserId", "BookId", "LoanStatus" },
                unique: true,
                filter: "LoanStatus in (0, 1, 2, 4)");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_BookId_CategoryId",
                table: "BookCategory",
                columns: new[] { "BookId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_ISBN",
                table: "Book",
                column: "ISBN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Document",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Loan_UserId_BookId_LoanStatus",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_BookCategory_BookId_CategoryId",
                table: "BookCategory");

            migrationBuilder.DropIndex(
                name: "IX_Book_ISBN",
                table: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_UserId",
                table: "Loan",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_BookId",
                table: "BookCategory",
                column: "BookId");
        }
    }
}
