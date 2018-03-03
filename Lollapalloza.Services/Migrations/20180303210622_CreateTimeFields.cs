using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Lollapalooza.Services.Migrations
{
    public partial class CreateTimeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Schedule",
                table: "Show",
                newName: "StartTime");

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "Show",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Show");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Show",
                newName: "Schedule");
        }
    }
}
