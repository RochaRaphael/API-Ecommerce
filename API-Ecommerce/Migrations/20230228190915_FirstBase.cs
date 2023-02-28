using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class FirstBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Url = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false),
                    Deleted = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Login = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    VerificationKey = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    LastToken = table.Column<string>(type: "NVARCHAR(MAX)", maxLength: 256, nullable: false),
                    IsChecked = table.Column<bool>(type: "BIT", nullable: false),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false),
                    Deleted = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Url = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false),
                    Active = table.Column<bool>(type: "BIT", nullable: false),
                    Deleted = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "DATE", nullable: false, defaultValue: new DateTime(2023, 2, 28, 19, 9, 15, 828, DateTimeKind.Utc).AddTicks(2352))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItem",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItem", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Order_ProductId",
                        column: x => x.OrderId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_OrderId",
                        column: x => x.ProductId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItem_ProductId",
                table: "PedidoItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoItem");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
