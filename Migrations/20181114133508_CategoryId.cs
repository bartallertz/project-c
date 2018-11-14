using Microsoft.EntityFrameworkCore.Migrations;

namespace projectC.Migrations
{
    public partial class CategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "Addition",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "House_Number",
                table: "users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Postalcode",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street_Name",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone_Number",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "users",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Addition",
                table: "users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "users");

            migrationBuilder.DropColumn(
                name: "House_Number",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Postalcode",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Street_Name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Telephone_Number",
                table: "users");

            migrationBuilder.DropColumn(
                name: "email",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "products",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
