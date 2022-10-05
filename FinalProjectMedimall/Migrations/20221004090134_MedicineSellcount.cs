using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectMedimall.Migrations
{
    public partial class MedicineSellcount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sellcount",
                table: "Medicines",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sellcount",
                table: "Medicines");
        }
    }
}
