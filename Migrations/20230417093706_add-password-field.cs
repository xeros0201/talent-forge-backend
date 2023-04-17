using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFBackend.Migrations
{
    public partial class addpasswordfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Client");
        }
    }
}
