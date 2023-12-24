using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class incomeanddonator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Donators");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DonatorId",
                table: "Incomes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncomeItemId",
                table: "Incomes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "IncomeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpreadsheetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeItem_Spreadsheets_SpreadsheetId",
                        column: x => x.SpreadsheetId,
                        principalTable: "Spreadsheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_DonatorId",
                table: "Incomes",
                column: "DonatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_IncomeItemId",
                table: "Incomes",
                column: "IncomeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItem_SpreadsheetId",
                table: "IncomeItem",
                column: "SpreadsheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Donators_DonatorId",
                table: "Incomes",
                column: "DonatorId",
                principalTable: "Donators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_IncomeItem_IncomeItemId",
                table: "Incomes",
                column: "IncomeItemId",
                principalTable: "IncomeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Donators_DonatorId",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_IncomeItem_IncomeItemId",
                table: "Incomes");

            migrationBuilder.DropTable(
                name: "IncomeItem");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_DonatorId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_IncomeItemId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "DonatorId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "IncomeItemId",
                table: "Incomes");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Incomes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Donators",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
