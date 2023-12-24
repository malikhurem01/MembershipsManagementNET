using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MembershipsManagement.Migrations
{
    public partial class addedincometype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IncomeType",
                table: "IncomeItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncomeType",
                table: "IncomeItems");
        }
    }
}
