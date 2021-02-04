using Microsoft.EntityFrameworkCore.Migrations;

namespace MySportShop.Migrations
{
    public partial class Alter_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfos_Orders_OrderId",
                table: "OrderInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfos_Products_ProductId",
                table: "OrderInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInfos_Products_ProductId",
                table: "ProductInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInfos_Properties_PropertyName1",
                table: "ProductInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInfos",
                table: "ProductInfos");

            migrationBuilder.DropIndex(
                name: "IX_ProductInfos_PropertyName1",
                table: "ProductInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "ProductInfos");

            migrationBuilder.DropColumn(
                name: "PropertyName1",
                table: "ProductInfos");

            migrationBuilder.RenameTable(
                name: "ProductInfos",
                newName: "ProductInfo");

            migrationBuilder.RenameTable(
                name: "OrderInfos",
                newName: "OrderInfo");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Properties",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ProductInfo",
                newName: "Size");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInfos_ProductId",
                table: "ProductInfo",
                newName: "IX_ProductInfo_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderInfos_OrderId",
                table: "OrderInfo",
                newName: "IX_OrderInfo_OrderId");

            migrationBuilder.AddColumn<double>(
                name: "Size",
                table: "Properties",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PropertySize",
                table: "ProductInfo",
                type: "float",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "Size");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInfo",
                table: "ProductInfo",
                column: "InfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInfo",
                table: "OrderInfo",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfo_PropertySize",
                table: "ProductInfo",
                column: "PropertySize");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfo_Orders_OrderId",
                table: "OrderInfo",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfo_Products_ProductId",
                table: "OrderInfo",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInfo_Products_ProductId",
                table: "ProductInfo",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInfo_Properties_PropertySize",
                table: "ProductInfo",
                column: "PropertySize",
                principalTable: "Properties",
                principalColumn: "Size",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfo_Orders_OrderId",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderInfo_Products_ProductId",
                table: "OrderInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInfo_Products_ProductId",
                table: "ProductInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInfo_Properties_PropertySize",
                table: "ProductInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInfo",
                table: "ProductInfo");

            migrationBuilder.DropIndex(
                name: "IX_ProductInfo_PropertySize",
                table: "ProductInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInfo",
                table: "OrderInfo");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PropertySize",
                table: "ProductInfo");

            migrationBuilder.RenameTable(
                name: "ProductInfo",
                newName: "ProductInfos");

            migrationBuilder.RenameTable(
                name: "OrderInfo",
                newName: "OrderInfos");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Properties",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "ProductInfos",
                newName: "Value");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInfo_ProductId",
                table: "ProductInfos",
                newName: "IX_ProductInfos_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderInfo_OrderId",
                table: "OrderInfos",
                newName: "IX_OrderInfos_OrderId");

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "ProductInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyName1",
                table: "ProductInfos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInfos",
                table: "ProductInfos",
                column: "InfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInfos",
                table: "OrderInfos",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfos_PropertyName1",
                table: "ProductInfos",
                column: "PropertyName1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfos_Orders_OrderId",
                table: "OrderInfos",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInfos_Products_ProductId",
                table: "OrderInfos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInfos_Products_ProductId",
                table: "ProductInfos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInfos_Properties_PropertyName1",
                table: "ProductInfos",
                column: "PropertyName1",
                principalTable: "Properties",
                principalColumn: "PropertyName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
