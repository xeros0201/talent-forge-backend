using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFBackend.Migrations
{
    public partial class UpdateRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Rolls_RollId",
                table: "Staff");

            migrationBuilder.DropTable(
                name: "Rolls");

            migrationBuilder.RenameColumn(
                name: "RollId",
                table: "Staff",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_RollId",
                table: "Staff",
                newName: "IX_Staff_RoleId");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Roles_RoleId",
                table: "Staff",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Roles_RoleId",
                table: "Staff");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Staff",
                newName: "RollId");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_RoleId",
                table: "Staff",
                newName: "IX_Staff_RollId");

            migrationBuilder.CreateTable(
                name: "Rolls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rolls", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Rolls_RollId",
                table: "Staff",
                column: "RollId",
                principalTable: "Rolls",
                principalColumn: "Id");
        }
    }
}
