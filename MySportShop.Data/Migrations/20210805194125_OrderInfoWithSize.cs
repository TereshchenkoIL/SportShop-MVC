using Microsoft.EntityFrameworkCore.Migrations;

namespace MySportShop.Data.Migrations
{
    public partial class OrderInfoWithSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Size",
                table: "OrderInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderInfo");
        }
    }
}
