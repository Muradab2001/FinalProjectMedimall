using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectMedimall.Migrations
{
    public partial class created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Point",
                table: "Rates",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Rates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_AppUserId",
                table: "Rates",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_AspNetUsers_AppUserId",
                table: "Rates",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_AspNetUsers_AppUserId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_AppUserId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Rates");

            migrationBuilder.AlterColumn<byte>(
                name: "Point",
                table: "Rates",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
