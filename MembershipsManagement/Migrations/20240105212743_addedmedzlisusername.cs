using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class addedmedzlisusername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Medzlisi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Medzlisi",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Medzlisi",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserName",
                value: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Medzlisi");
        }
    }
}
