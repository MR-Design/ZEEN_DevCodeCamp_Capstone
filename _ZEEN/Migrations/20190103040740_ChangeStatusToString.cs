using Microsoft.EntityFrameworkCore.Migrations;

namespace _ZEEN.Migrations
{
    public partial class ChangeStatusToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Statu",
                table: "Sales",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Statu",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
