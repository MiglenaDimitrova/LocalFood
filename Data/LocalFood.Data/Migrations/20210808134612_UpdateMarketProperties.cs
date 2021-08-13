namespace LocalFood.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UpdateMarketProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markets_Locations_LocationId",
                table: "Markets");

            migrationBuilder.DropIndex(
                name: "IX_Markets_LocationId",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Markets");

            migrationBuilder.AddColumn<string>(
                name: "FullAddress",
                table: "Markets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullAddress",
                table: "Markets");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Markets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Markets_LocationId",
                table: "Markets",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_Locations_LocationId",
                table: "Markets",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
