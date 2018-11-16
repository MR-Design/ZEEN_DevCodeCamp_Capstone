using Microsoft.EntityFrameworkCore.Migrations;

namespace _ZEEN.Migrations
{
    public partial class FKtostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SaleID",
                table: "Sales",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SaleID",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
