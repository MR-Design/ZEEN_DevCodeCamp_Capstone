using Microsoft.EntityFrameworkCore.Migrations;

namespace _ZEEN.Migrations
{
    public partial class addedAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "RegularUsers",
                newName: "AvatarImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarImage",
                table: "RegularUsers",
                newName: "Picture");
        }
    }
}
