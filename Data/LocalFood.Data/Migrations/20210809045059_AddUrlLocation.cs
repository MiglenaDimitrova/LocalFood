namespace LocalFood.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddUrlLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlLocation",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlLocation",
                table: "Locations");
        }
    }
}
