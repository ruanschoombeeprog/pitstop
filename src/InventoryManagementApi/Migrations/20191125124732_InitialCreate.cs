using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pitstop.InventoryManagementApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ProductCode = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.ProductCode);
                });

            migrationBuilder.CreateTable(
                name: "InventoryUsed",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    JobId = table.Column<string>(nullable: true),
                    QuantityUsed = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    DateStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryUsed", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "InventoryUsed");
        }
    }
}
