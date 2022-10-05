using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectMedimall.Migrations
{
    public partial class MedicineTrend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Trend",
                table: "Medicines",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trend",
                table: "Medicines");
        }
    }
}
