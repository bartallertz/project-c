using Microsoft.EntityFrameworkCore.Migrations;

namespace projectC.Migrations
{
    public partial class pornhub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageURL",
                table: "products",
                nullable: false,
                defaultValue: 0);
        }
    }
}
