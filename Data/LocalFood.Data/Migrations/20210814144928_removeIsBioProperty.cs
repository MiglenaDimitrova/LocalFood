using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalFood.Data.Migrations
{
    public partial class removeIsBioProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBio",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBio",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
