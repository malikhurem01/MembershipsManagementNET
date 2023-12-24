using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class donatorwithdzemat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DzematId",
                table: "Donators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Donators_DzematId",
                table: "Donators",
                column: "DzematId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donators_Dzemati_DzematId",
                table: "Donators",
                column: "DzematId",
                principalTable: "Dzemati",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donators_Dzemati_DzematId",
                table: "Donators");

            migrationBuilder.DropIndex(
                name: "IX_Donators_DzematId",
                table: "Donators");

            migrationBuilder.DropColumn(
                name: "DzematId",
                table: "Donators");
        }
    }
}
