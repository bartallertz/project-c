using Microsoft.EntityFrameworkCore.Migrations;

namespace projectC.Migrations
{
    public partial class SubCategoryid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_SubCategoryId",
                table: "products",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_SubCategories_SubCategoryId",
                table: "products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_SubCategories_SubCategoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_SubCategoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "products");
        }
    }
}
