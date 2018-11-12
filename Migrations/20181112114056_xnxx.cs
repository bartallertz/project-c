using Microsoft.EntityFrameworkCore.Migrations;

namespace projectC.Migrations
{
    public partial class xnxx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_ImageURL_imageURLId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_imageURLId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "imageURLId",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ImageURL",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageURL_ProductId",
                table: "ImageURL",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageURL_products_ProductId",
                table: "ImageURL",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageURL_products_ProductId",
                table: "ImageURL");

            migrationBuilder.DropIndex(
                name: "IX_ImageURL_ProductId",
                table: "ImageURL");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ImageURL");

            migrationBuilder.AddColumn<int>(
                name: "imageURLId",
                table: "products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_imageURLId",
                table: "products",
                column: "imageURLId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_ImageURL_imageURLId",
                table: "products",
                column: "imageURLId",
                principalTable: "ImageURL",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
