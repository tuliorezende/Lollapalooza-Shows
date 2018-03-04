using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lollapalooza.Services.Migrations
{
    public partial class CreateUserScheduleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSchedule",
                columns: table => new
                {
                    UserIdentifier = table.Column<string>(nullable: false),
                    ShowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSchedule", x => new { x.UserIdentifier, x.ShowId });
                    table.UniqueConstraint("AK_UserSchedule_ShowId_UserIdentifier", x => new { x.ShowId, x.UserIdentifier });
                    table.ForeignKey(
                        name: "FK_UserSchedule_Show_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Show",
                        principalColumn: "ShowId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSchedule");
        }
    }
}
