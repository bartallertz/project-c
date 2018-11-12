using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace projectC.Migrations
{
    public partial class meidenvanhollandhard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favourites_users_UserId",
                table: "favourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favourites",
                table: "favourites");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "favourites");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "favourites",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "favourites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_favourites",
                table: "favourites",
                columns: new[] { "ProductId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_favourites_products_ProductId",
                table: "favourites",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_favourites_users_UserId",
                table: "favourites",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favourites_products_ProductId",
                table: "favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_favourites_users_UserId",
                table: "favourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favourites",
                table: "favourites");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "favourites");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "favourites",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "favourites",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_favourites",
                table: "favourites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_favourites_users_UserId",
                table: "favourites",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
