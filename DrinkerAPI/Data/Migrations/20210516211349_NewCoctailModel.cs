using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkerAPI.Data.Migrations
{
    public partial class NewCoctailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Coctails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Coctails",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Coctails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Coctails");
        }
    }
}
