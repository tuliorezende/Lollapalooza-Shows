using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lollapalooza.Services.Migrations
{
    public partial class ChangeUserScheduleStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserSchedule_ShowId_UserIdentifier",
                table: "UserSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchedule_ShowId",
                table: "UserSchedule",
                column: "ShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSchedule_ShowId",
                table: "UserSchedule");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserSchedule_ShowId_UserIdentifier",
                table: "UserSchedule",
                columns: new[] { "ShowId", "UserIdentifier" });
        }
    }
}
