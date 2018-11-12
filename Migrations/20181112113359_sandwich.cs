using Microsoft.EntityFrameworkCore.Migrations;

namespace projectC.Migrations
{
    public partial class sandwich : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageURL",
                table: "products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "products");
        }
    }
}
