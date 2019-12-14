using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGuitarRentals.Migrations
{
    public partial class AddImageUrlfieldtoDistributionCentermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "DistributionCenters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "DistributionCenters");
        }
    }
}
