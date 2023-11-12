using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class addeddebt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Debt",
                table: "MembersInfo");

            migrationBuilder.AddColumn<double>(
                name: "Debt",
                table: "Members",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Debt",
                table: "Members");

            migrationBuilder.AddColumn<double>(
                name: "Debt",
                table: "MembersInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
