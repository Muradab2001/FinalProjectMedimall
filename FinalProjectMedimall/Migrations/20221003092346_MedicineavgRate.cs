using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectMedimall.Migrations
{
    public partial class MedicineavgRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RateAvg",
                table: "Medicines",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RateAvg",
                table: "Medicines");
        }
    }
}
