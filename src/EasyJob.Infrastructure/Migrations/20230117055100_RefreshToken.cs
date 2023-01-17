using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyJob.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc56836e-0345-4f01-a883-47f39e32e079"),
                columns: new[] { "CreatedAt", "RefreshToken", "RefreshTokenExpireDate", "Salt" },
                values: new object[] { new DateTime(2023, 1, 17, 5, 50, 59, 983, DateTimeKind.Utc).AddTicks(771), null, null, "affc2435-b726-4ea2-9dda-9a4104e913c7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireDate",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc56836e-0345-4f01-a883-47f39e32e079"),
                columns: new[] { "CreatedAt", "Salt" },
                values: new object[] { new DateTime(2023, 1, 9, 6, 57, 28, 483, DateTimeKind.Utc).AddTicks(5692), "ed5edc2d-5501-4a11-a3d9-fdf292b24151" });
        }
    }
}
