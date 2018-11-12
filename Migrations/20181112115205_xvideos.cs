using Microsoft.EntityFrameworkCore.Migrations;

namespace projectC.Migrations
{
    public partial class xvideos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageURL_products_ProductId",
                table: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ImageURL",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageURL_ProductId",
                table: "ImageURL",
                newName: "IX_ImageURL_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageURL_products_productId",
                table: "ImageURL",
                column: "productId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageURL_products_productId",
                table: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ImageURL",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageURL_productId",
                table: "ImageURL",
                newName: "IX_ImageURL_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageURL_products_ProductId",
                table: "ImageURL",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
