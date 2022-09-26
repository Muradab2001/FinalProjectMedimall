using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectMedimall.Migrations
{
    public partial class CreateDiscountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Medicines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Percentage = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DiscountId",
                table: "Medicines",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Discounts_DiscountId",
                table: "Medicines",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Discounts_DiscountId",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_DiscountId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Medicines");
        }
    }
}
