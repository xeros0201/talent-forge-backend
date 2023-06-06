using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFBackend.Migrations
{
    public partial class _24_5_2023_03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffClients");

            migrationBuilder.DropColumn(
                name: "TotalProjects",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Client",
                newName: "Suburb");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Client",
                newName: "StreetName");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientSince",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StreetNo",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CalendarProjectStaff",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    DayStatus = table.Column<int>(type: "int", nullable: false),
                    IsHoliday = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarProjectStaff", x => new { x.ProjectId, x.StaffId, x.Date });
                    table.ForeignKey(
                        name: "FK_CalendarProjectStaff_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarProjectStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarProjectStaff_StaffId",
                table: "CalendarProjectStaff",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Staff_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "Staff",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Staff_ManagerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "CalendarProjectStaff");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ClientSince",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "StreetNo",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "Suburb",
                table: "Client",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "Client",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "TotalProjects",
                table: "Client",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StaffClients",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffClients", x => new { x.StaffId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_StaffClients_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffClients_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffClients_ClientId",
                table: "StaffClients",
                column: "ClientId");
        }
    }
}
