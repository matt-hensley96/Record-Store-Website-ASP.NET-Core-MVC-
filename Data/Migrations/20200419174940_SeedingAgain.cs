using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Goldies.Migrations
{
    public partial class SeedingAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2020, 4, 19, 17, 49, 40, 189, DateTimeKind.Utc).AddTicks(6030));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2020, 4, 18, 14, 51, 25, 437, DateTimeKind.Utc).AddTicks(1636));
        }
    }
}
