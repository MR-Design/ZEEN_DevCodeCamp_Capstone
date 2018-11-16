using Microsoft.EntityFrameworkCore.Migrations;

namespace _ZEEN.Migrations
{
    public partial class deleteImageFromSaleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Sales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Sales",
                nullable: true);
        }
    }
}
