using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGuitarRentals.Migrations
{
    public partial class RemoveAltImageUrl6Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltImgUrl6",
                table: "RentalAssets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AltImgUrl6",
                table: "RentalAssets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
