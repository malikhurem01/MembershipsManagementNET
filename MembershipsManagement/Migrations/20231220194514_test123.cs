using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class test123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spreadsheets_Dzemati_DzematId",
                table: "Spreadsheets");

            migrationBuilder.AddForeignKey(
                name: "FK_Spreadsheets_Dzemati_DzematId",
                table: "Spreadsheets",
                column: "DzematId",
                principalTable: "Dzemati",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spreadsheets_Dzemati_DzematId",
                table: "Spreadsheets");

            migrationBuilder.AddForeignKey(
                name: "FK_Spreadsheets_Dzemati_DzematId",
                table: "Spreadsheets",
                column: "DzematId",
                principalTable: "Dzemati",
                principalColumn: "Id");
        }
    }
}
