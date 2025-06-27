using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dsw2025Tpi.Api.Migrations
{
    /// <inheritdoc />
    public partial class prueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _sku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    _internalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    _name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    _description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    _currentUnitPrice = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    _stockQuantity = table.Column<int>(type: "int", nullable: false),
                    _isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
