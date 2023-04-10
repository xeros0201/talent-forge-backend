using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFBackend.Migrations
{
    public partial class RollNullableInStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Rolls_RollId",
                table: "Staff");

            migrationBuilder.AlterColumn<int>(
                name: "RollId",
                table: "Staff",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Rolls_RollId",
                table: "Staff",
                column: "RollId",
                principalTable: "Rolls",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Rolls_RollId",
                table: "Staff");

            migrationBuilder.AlterColumn<int>(
                name: "RollId",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Rolls_RollId",
                table: "Staff",
                column: "RollId",
                principalTable: "Rolls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
