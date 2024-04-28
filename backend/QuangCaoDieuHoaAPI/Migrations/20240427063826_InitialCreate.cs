using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuangCaoDieuHoaAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 27, 6, 38, 25, 788, DateTimeKind.Utc).AddTicks(5428));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 26, 4, 26, 27, 249, DateTimeKind.Utc).AddTicks(8312));
        }
    }
}
