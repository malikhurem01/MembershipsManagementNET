using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class incomeanddonator2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeItem_Spreadsheets_SpreadsheetId",
                table: "IncomeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_IncomeItem_IncomeItemId",
                table: "Incomes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeItem",
                table: "IncomeItem");

            migrationBuilder.RenameTable(
                name: "IncomeItem",
                newName: "IncomeItems");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItem_SpreadsheetId",
                table: "IncomeItems",
                newName: "IX_IncomeItems_SpreadsheetId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Donators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Donators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeItems",
                table: "IncomeItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeItems_Spreadsheets_SpreadsheetId",
                table: "IncomeItems",
                column: "SpreadsheetId",
                principalTable: "Spreadsheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_IncomeItems_IncomeItemId",
                table: "Incomes",
                column: "IncomeItemId",
                principalTable: "IncomeItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeItems_Spreadsheets_SpreadsheetId",
                table: "IncomeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_IncomeItems_IncomeItemId",
                table: "Incomes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeItems",
                table: "IncomeItems");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Donators");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Donators");

            migrationBuilder.RenameTable(
                name: "IncomeItems",
                newName: "IncomeItem");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeItems_SpreadsheetId",
                table: "IncomeItem",
                newName: "IX_IncomeItem_SpreadsheetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeItem",
                table: "IncomeItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeItem_Spreadsheets_SpreadsheetId",
                table: "IncomeItem",
                column: "SpreadsheetId",
                principalTable: "Spreadsheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_IncomeItem_IncomeItemId",
                table: "Incomes",
                column: "IncomeItemId",
                principalTable: "IncomeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
