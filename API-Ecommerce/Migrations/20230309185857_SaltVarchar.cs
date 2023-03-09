using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class SaltVarchar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "User",
                type: "VARCHAR",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 9, 18, 58, 57, 853, DateTimeKind.Utc).AddTicks(7438),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2023, 3, 9, 18, 8, 17, 630, DateTimeKind.Utc).AddTicks(9853));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Salt",
                table: "User",
                type: "VARBINARY",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 9, 18, 8, 17, 630, DateTimeKind.Utc).AddTicks(9853),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2023, 3, 9, 18, 58, 57, 853, DateTimeKind.Utc).AddTicks(7438));
        }
    }
}
