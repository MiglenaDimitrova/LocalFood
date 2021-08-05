using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalFood.Data.Migrations
{
    public partial class AddUsersProducers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producers_Favorites_FavoriteId",
                table: "Producers");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Producers_FavoriteId",
                table: "Producers");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Producers");

            migrationBuilder.CreateTable(
                name: "UsersProducers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersProducers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersProducers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersProducers_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersProducers_ApplicationUserId",
                table: "UsersProducers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersProducers_IsDeleted",
                table: "UsersProducers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UsersProducers_ProducerId",
                table: "UsersProducers",
                column: "ProducerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersProducers");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteId",
                table: "Producers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producers_FavoriteId",
                table: "Producers",
                column: "FavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_IsDeleted",
                table: "Favorites",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Producers_Favorites_FavoriteId",
                table: "Producers",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
