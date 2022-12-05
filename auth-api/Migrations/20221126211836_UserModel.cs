using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auth_api.Migrations
{
    public partial class UserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CorporateName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FantasyName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CorporateName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FantasyName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");
        }
    }
}
