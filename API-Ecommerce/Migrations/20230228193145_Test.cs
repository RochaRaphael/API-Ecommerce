using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VerificationKey",
                table: "User",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "LastToken",
                table: "User",
                type: "NVARCHAR(MAX)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<bool>(
                name: "IsChecked",
                table: "User",
                type: "BIT",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "BIT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 28, 19, 31, 45, 368, DateTimeKind.Utc).AddTicks(1450),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2023, 2, 28, 19, 9, 15, 828, DateTimeKind.Utc).AddTicks(2352));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VerificationKey",
                table: "User",
                type: "VARCHAR(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastToken",
                table: "User",
                type: "NVARCHAR(MAX)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsChecked",
                table: "User",
                type: "BIT",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "BIT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 28, 19, 9, 15, 828, DateTimeKind.Utc).AddTicks(2352),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldDefaultValue: new DateTime(2023, 2, 28, 19, 31, 45, 368, DateTimeKind.Utc).AddTicks(1450));
        }
    }
}
