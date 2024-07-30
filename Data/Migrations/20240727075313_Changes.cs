using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BorrowedBooks_BorrowedBookId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BorrowedBookId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "BorrowedBookId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "BorrowedBooks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BorrowedBooks");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "BorrowedBooks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BorrowedBookId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BorrowedBookId",
                table: "Books",
                column: "BorrowedBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BorrowedBooks_BorrowedBookId",
                table: "Books",
                column: "BorrowedBookId",
                principalTable: "BorrowedBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
