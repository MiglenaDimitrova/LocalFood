using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalFood.Data.Migrations
{
    public partial class AddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producers_Images_ImageId1",
                table: "Producers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImageId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Producers_ImageId1",
                table: "Producers");

            migrationBuilder.DropColumn(
                name: "ImageId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageId1",
                table: "Producers");

            migrationBuilder.DropColumn(
                name: "ProducerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Producers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Producers_ImageId",
                table: "Producers",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producers_Images_ImageId",
                table: "Producers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producers_Images_ImageId",
                table: "Producers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_ImageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ImageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Producers_ImageId",
                table: "Producers");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId1",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "Producers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId1",
                table: "Producers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProducerId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId1",
                table: "Products",
                column: "ImageId1");

            migrationBuilder.CreateIndex(
                name: "IX_Producers_ImageId1",
                table: "Producers",
                column: "ImageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Producers_Images_ImageId1",
                table: "Producers",
                column: "ImageId1",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_ImageId1",
                table: "Products",
                column: "ImageId1",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
