using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lollapalooza.Services.Migrations
{
    public partial class AddingUSerScheduleIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserSchedule_UserIdentifier_ShowId",
                table: "UserSchedule",
                columns: new[] { "UserIdentifier", "ShowId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSchedule_UserIdentifier_ShowId",
                table: "UserSchedule");
        }
    }
}
