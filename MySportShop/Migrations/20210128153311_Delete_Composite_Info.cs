using Microsoft.EntityFrameworkCore.Migrations;

namespace MySportShop.Migrations
{
    public partial class Delete_Composite_Info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInfos",
                table: "ProductInfos");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                table: "ProductInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "InfoId",
                table: "ProductInfos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInfos",
                table: "ProductInfos",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfos_ProductId",
                table: "ProductInfos",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInfos",
                table: "ProductInfos");

            migrationBuilder.DropIndex(
                name: "IX_ProductInfos_ProductId",
                table: "ProductInfos");

            migrationBuilder.DropColumn(
                name: "InfoId",
                table: "ProductInfos");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                table: "ProductInfos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInfos",
                table: "ProductInfos",
                columns: new[] { "ProductId", "PropertyName" });
        }
    }
}
