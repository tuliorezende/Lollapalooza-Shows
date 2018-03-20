using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lollapalooza.Services.Migrations
{
    public partial class CreateTimeToAlertAndBooleanAlertValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowRemember",
                table: "UserSchedule",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TimeMinutesToAlert",
                table: "UserSchedule",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowRemember",
                table: "UserSchedule");

            migrationBuilder.DropColumn(
                name: "TimeMinutesToAlert",
                table: "UserSchedule");
        }
    }
}
