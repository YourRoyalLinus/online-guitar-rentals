using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalData.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentalAssets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Available = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true),
                    NumberOfStrings = table.Column<int>(nullable: true),
                    Specifications = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingRegions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbrv = table.Column<string>(nullable: false),
                    Region = table.Column<string>(nullable: true),
                    States = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributionCenters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(nullable: false),
                    ShippingRegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistributionCenters_ShippingRegions_ShippingRegionId",
                        column: x => x.ShippingRegionId,
                        principalTable: "ShippingRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Telephone = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ShippingRegionId = table.Column<int>(nullable: true),
                    RenewalDate = table.Column<DateTime>(nullable: true),
                    ExperationDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_ShippingRegions_ShippingRegionId",
                        column: x => x.ShippingRegionId,
                        principalTable: "ShippingRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributionCenterId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    DeliveryStartTime = table.Column<DateTime>(nullable: false),
                    DeliveryEndTime = table.Column<DateTime>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Couriers_DistributionCenters_DistributionCenterId",
                        column: x => x.DistributionCenterId,
                        principalTable: "DistributionCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalAssetId = table.Column<int>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    DistributionCenterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_DistributionCenters_DistributionCenterId",
                        column: x => x.DistributionCenterId,
                        principalTable: "DistributionCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_RentalAssets_RentalAssetId",
                        column: x => x.RentalAssetId,
                        principalTable: "RentalAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Holds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalAssetId = table.Column<int>(nullable: true),
                    SubscriberId = table.Column<int>(nullable: true),
                    DistributionCenterId = table.Column<int>(nullable: true),
                    HoldPlaced = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holds_DistributionCenters_DistributionCenterId",
                        column: x => x.DistributionCenterId,
                        principalTable: "DistributionCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Holds_RentalAssets_RentalAssetId",
                        column: x => x.RentalAssetId,
                        principalTable: "RentalAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Holds_Users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalAssetId = table.Column<int>(nullable: false),
                    SubscriberId = table.Column<int>(nullable: false),
                    DistributionCenterId = table.Column<int>(nullable: false),
                    RentedOut = table.Column<DateTime>(nullable: false),
                    Returned = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalHistories_DistributionCenters_DistributionCenterId",
                        column: x => x.DistributionCenterId,
                        principalTable: "DistributionCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalHistories_RentalAssets_RentalAssetId",
                        column: x => x.RentalAssetId,
                        principalTable: "RentalAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentalHistories_Users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalAssetId = table.Column<int>(nullable: false),
                    SubscriberId = table.Column<int>(nullable: false),
                    DistributionCenterId = table.Column<int>(nullable: false),
                    Since = table.Column<DateTime>(nullable: false),
                    Until = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_DistributionCenters_DistributionCenterId",
                        column: x => x.DistributionCenterId,
                        principalTable: "DistributionCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_RentalAssets_RentalAssetId",
                        column: x => x.RentalAssetId,
                        principalTable: "RentalAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_DistributionCenterId",
                table: "Couriers",
                column: "DistributionCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionCenters_ShippingRegionId",
                table: "DistributionCenters",
                column: "ShippingRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_DistributionCenterId",
                table: "Holds",
                column: "DistributionCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_RentalAssetId",
                table: "Holds",
                column: "RentalAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Holds_SubscriberId",
                table: "Holds",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_DistributionCenterId",
                table: "Inventory",
                column: "DistributionCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_RentalAssetId",
                table: "Inventory",
                column: "RentalAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalHistories_DistributionCenterId",
                table: "RentalHistories",
                column: "DistributionCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalHistories_RentalAssetId",
                table: "RentalHistories",
                column: "RentalAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalHistories_SubscriberId",
                table: "RentalHistories",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DistributionCenterId",
                table: "Rentals",
                column: "DistributionCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalAssetId",
                table: "Rentals",
                column: "RentalAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_SubscriberId",
                table: "Rentals",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShippingRegionId",
                table: "Users",
                column: "ShippingRegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Couriers");

            migrationBuilder.DropTable(
                name: "Holds");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "RentalHistories");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "DistributionCenters");

            migrationBuilder.DropTable(
                name: "RentalAssets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ShippingRegions");
        }
    }
}
