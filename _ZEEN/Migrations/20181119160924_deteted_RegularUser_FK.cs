using Microsoft.EntityFrameworkCore.Migrations;

namespace _ZEEN.Migrations
{
    public partial class deteted_RegularUser_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RegularUsers_RegularUserID",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_RegularUserID",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "RegularUserID",
                table: "Sales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegularUserID",
                table: "Sales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_RegularUserID",
                table: "Sales",
                column: "RegularUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RegularUsers_RegularUserID",
                table: "Sales",
                column: "RegularUserID",
                principalTable: "RegularUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
