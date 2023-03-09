using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsWithSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "User",
                type: "VARBINARY",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 9, 18, 8, 17, 630, DateTimeKind.Utc).AddTicks(9853),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2023, 2, 28, 19, 31, 45, 368, DateTimeKind.Utc).AddTicks(1450));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 28, 19, 31, 45, 368, DateTimeKind.Utc).AddTicks(1450),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2023, 3, 9, 18, 8, 17, 630, DateTimeKind.Utc).AddTicks(9853));
        }
    }
}
