using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFBackend.Migrations
{
    public partial class ModifyStaffTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roll",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Roll",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
