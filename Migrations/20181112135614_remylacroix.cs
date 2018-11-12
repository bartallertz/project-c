using Microsoft.EntityFrameworkCore.Migrations;

namespace projectC.Migrations
{
    public partial class remylacroix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageURL_products_productId",
                table: "ImageURL");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageURL",
                table: "ImageURL");

            migrationBuilder.RenameTable(
                name: "ImageURL",
                newName: "imageURLs");

            migrationBuilder.RenameIndex(
                name: "IX_ImageURL_productId",
                table: "imageURLs",
                newName: "IX_imageURLs_productId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_imageURLs",
                table: "imageURLs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_imageURLs_products_productId",
                table: "imageURLs",
                column: "productId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_imageURLs_products_productId",
                table: "imageURLs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_imageURLs",
                table: "imageURLs");

            migrationBuilder.RenameTable(
                name: "imageURLs",
                newName: "ImageURL");

            migrationBuilder.RenameIndex(
                name: "IX_imageURLs_productId",
                table: "ImageURL",
                newName: "IX_ImageURL_productId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageURL",
                table: "ImageURL",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageURL_products_productId",
                table: "ImageURL",
                column: "productId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
