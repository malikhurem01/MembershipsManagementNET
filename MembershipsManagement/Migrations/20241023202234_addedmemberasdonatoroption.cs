using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class addedmemberasdonatoroption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Incomes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_MemberId",
                table: "Incomes",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Members_MemberId",
                table: "Incomes",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Members_MemberId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_MemberId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Incomes");
        }
    }
}
