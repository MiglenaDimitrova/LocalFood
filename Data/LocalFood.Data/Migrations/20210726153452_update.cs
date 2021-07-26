using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalFood.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBio",
                table: "Producers");

            migrationBuilder.RenameColumn(
                name: "TownName",
                table: "Locations",
                newName: "LocalityName");

            migrationBuilder.AddColumn<bool>(
                name: "IsBio",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBio",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "LocalityName",
                table: "Locations",
                newName: "TownName");

            migrationBuilder.AddColumn<bool>(
                name: "IsBio",
                table: "Producers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
