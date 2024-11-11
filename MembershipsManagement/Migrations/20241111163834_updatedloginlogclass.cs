using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class updatedloginlogclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DzematName",
                table: "UserLoginLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserLoginLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserLoginLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DzematName",
                table: "UserLoginLogs");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserLoginLogs");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserLoginLogs");
        }
    }
}
