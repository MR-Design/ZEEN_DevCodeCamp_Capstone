using Microsoft.EntityFrameworkCore.Migrations;

namespace _ZEEN.Migrations
{
    public partial class addFKinSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleID",
                table: "Sales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "regularUserId",
                table: "Sales",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_regularUserId",
                table: "Sales",
                column: "regularUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RegularUsers_regularUserId",
                table: "Sales",
                column: "regularUserId",
                principalTable: "RegularUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RegularUsers_regularUserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_regularUserId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SaleID",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "regularUserId",
                table: "Sales");
        }
    }
}
