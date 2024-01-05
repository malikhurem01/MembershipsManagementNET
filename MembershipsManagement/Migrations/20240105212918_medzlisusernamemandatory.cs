using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class medzlisusernamemandatory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Medzlisi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Medzlisi",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserName",
                value: "medzlistuzla");

            migrationBuilder.UpdateData(
                table: "Medzlisi",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserName",
                value: "medzlisgracanica");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Medzlisi",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
