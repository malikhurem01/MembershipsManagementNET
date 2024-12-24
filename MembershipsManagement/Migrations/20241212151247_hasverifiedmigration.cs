using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class hasverifiedmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasVerified",
                table: "Members");

            migrationBuilder.AddColumn<bool>(
                name: "HasVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasVerified",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "HasVerified",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
