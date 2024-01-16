using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Team21V4._5.Data.Migrations
{
    public partial class UpdateProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoughtStock",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DeliveredQuantity",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Precipitation",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "WasteQuantity",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoughtStock",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveredQuantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Precipitation",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "WasteQuantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
