using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESimple.Migrations
{
    public partial class AddUserTypeToUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "AbpUsers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AbpUsers");
        }
    }
}
