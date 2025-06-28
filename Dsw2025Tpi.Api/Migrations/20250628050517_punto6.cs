using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dsw2025Tpi.Api.Migrations
{
    /// <inheritdoc />
    public partial class punto6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    _email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    _phoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _customerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _shippingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    _billingAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    _notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    _totalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    _status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers__customerId",
                        column: x => x._customerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _orderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _productId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _quantity = table.Column<int>(type: "int", nullable: false),
                    _unitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    _subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders__orderId",
                        column: x => x._orderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products__productId",
                        column: x => x._productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers__email",
                table: "Customers",
                column: "_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems__orderId",
                table: "OrderItems",
                column: "_orderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems__productId",
                table: "OrderItems",
                column: "_productId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders__customerId",
                table: "Orders",
                column: "_customerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
