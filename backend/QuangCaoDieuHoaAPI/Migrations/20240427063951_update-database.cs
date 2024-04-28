using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuangCaoDieuHoaAPI.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 27, 6, 39, 50, 803, DateTimeKind.Utc).AddTicks(1324));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 27, 6, 38, 25, 788, DateTimeKind.Utc).AddTicks(5428));
        }
    }
}
