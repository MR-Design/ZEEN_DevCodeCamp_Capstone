using Microsoft.EntityFrameworkCore.Migrations;

namespace _ZEEN.Migrations
{
    public partial class AddStatusTosales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancled",
                table: "Sales",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReceived",
                table: "Sales",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShipped",
                table: "Sales",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Sales",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancled",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsReceived",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsShipped",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Sales");
        }
    }
}
