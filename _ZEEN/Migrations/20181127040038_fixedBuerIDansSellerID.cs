using Microsoft.EntityFrameworkCore.Migrations;

namespace _ZEEN.Migrations
{
    public partial class fixedBuerIDansSellerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_AspNetUsers_BuyerID",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_BuyerID",
                table: "Shippings");

            migrationBuilder.AlterColumn<string>(
                name: "SellerID",
                table: "Shippings",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerID",
                table: "Shippings",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_SellerID",
                table: "Shippings",
                column: "SellerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_AspNetUsers_SellerID",
                table: "Shippings",
                column: "SellerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_AspNetUsers_SellerID",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_SellerID",
                table: "Shippings");

            migrationBuilder.AlterColumn<string>(
                name: "SellerID",
                table: "Shippings",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerID",
                table: "Shippings",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_BuyerID",
                table: "Shippings",
                column: "BuyerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_AspNetUsers_BuyerID",
                table: "Shippings",
                column: "BuyerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
