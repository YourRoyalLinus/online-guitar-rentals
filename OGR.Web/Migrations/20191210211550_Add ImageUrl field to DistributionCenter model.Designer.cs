﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentalData;

namespace OnlineGuitarRentals.Migrations
{
    [DbContext(typeof(RentalContext))]
    [Migration("20191210211550_Add ImageUrl field to DistributionCenter model")]
    partial class AddImageUrlfieldtoDistributionCentermodel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RentalData.Models.Courier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeliveryEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryStartTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DistributionCenterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DistributionCenterId");

                    b.ToTable("Couriers");
                });

            modelBuilder.Entity("RentalData.Models.DistributionCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ShippingRegionId")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShippingRegionId");

                    b.ToTable("DistributionCenters");
                });

            modelBuilder.Entity("RentalData.Models.Hold", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DistributionCenterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("HoldPlaced")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RentalAssetId")
                        .HasColumnType("int");

                    b.Property<int?>("SubscriberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DistributionCenterId");

                    b.HasIndex("RentalAssetId");

                    b.HasIndex("SubscriberId");

                    b.ToTable("Holds");
                });

            modelBuilder.Entity("RentalData.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DistributionCenterId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("RentalAssetId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DistributionCenterId");

                    b.HasIndex("RentalAssetId");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("RentalData.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DistributionCenterId")
                        .HasColumnType("int");

                    b.Property<int>("RentalAssetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Since")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubscriberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Until")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DistributionCenterId");

                    b.HasIndex("RentalAssetId");

                    b.HasIndex("SubscriberId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("RentalData.Models.RentalAsset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AltImgUrl1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltImgUrl2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltImgUrl3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltImgUrl4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltImgUrl5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AltImgUrl6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Available")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("RentalAssets");

                    b.HasDiscriminator<string>("Discriminator").HasValue("RentalAsset");
                });

            modelBuilder.Entity("RentalData.Models.RentalHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DistributionCenterId")
                        .HasColumnType("int");

                    b.Property<int>("RentalAssetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RentedOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Returned")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubscriberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DistributionCenterId");

                    b.HasIndex("RentalAssetId");

                    b.HasIndex("SubscriberId");

                    b.ToTable("RentalHistories");
                });

            modelBuilder.Entity("RentalData.Models.ShippingRegion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbrv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("States")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShippingRegions");
                });

            modelBuilder.Entity("RentalData.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("RentalData.Models.Guitar", b =>
                {
                    b.HasBaseType("RentalData.Models.RentalAsset");

                    b.Property<int>("NumberOfStrings")
                        .HasColumnType("int");

                    b.Property<string>("Specifications")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Guitar");
                });

            modelBuilder.Entity("RentalData.Models.Subscriber", b =>
                {
                    b.HasBaseType("RentalData.Models.User");

                    b.Property<int>("Active")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExperationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RenewalDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ShippingRegionId")
                        .HasColumnType("int");

                    b.HasIndex("ShippingRegionId");

                    b.HasDiscriminator().HasValue("Subscriber");
                });

            modelBuilder.Entity("RentalData.Models.Courier", b =>
                {
                    b.HasOne("RentalData.Models.DistributionCenter", "DistributionCenter")
                        .WithMany()
                        .HasForeignKey("DistributionCenterId");
                });

            modelBuilder.Entity("RentalData.Models.DistributionCenter", b =>
                {
                    b.HasOne("RentalData.Models.ShippingRegion", "ShippingRegion")
                        .WithMany("DistributionCenter")
                        .HasForeignKey("ShippingRegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RentalData.Models.Hold", b =>
                {
                    b.HasOne("RentalData.Models.DistributionCenter", "DistributionCenter")
                        .WithMany()
                        .HasForeignKey("DistributionCenterId");

                    b.HasOne("RentalData.Models.RentalAsset", "RentalAsset")
                        .WithMany()
                        .HasForeignKey("RentalAssetId");

                    b.HasOne("RentalData.Models.Subscriber", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId");
                });

            modelBuilder.Entity("RentalData.Models.Inventory", b =>
                {
                    b.HasOne("RentalData.Models.DistributionCenter", "DistributionCenter")
                        .WithMany("Inventory")
                        .HasForeignKey("DistributionCenterId");

                    b.HasOne("RentalData.Models.RentalAsset", "RentalAsset")
                        .WithMany()
                        .HasForeignKey("RentalAssetId");
                });

            modelBuilder.Entity("RentalData.Models.Rental", b =>
                {
                    b.HasOne("RentalData.Models.DistributionCenter", "DistributionCenter")
                        .WithMany()
                        .HasForeignKey("DistributionCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentalData.Models.RentalAsset", "RentalAsset")
                        .WithMany()
                        .HasForeignKey("RentalAssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentalData.Models.Subscriber", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RentalData.Models.RentalHistory", b =>
                {
                    b.HasOne("RentalData.Models.DistributionCenter", "DistributionCenter")
                        .WithMany()
                        .HasForeignKey("DistributionCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentalData.Models.RentalAsset", "RentalAsset")
                        .WithMany()
                        .HasForeignKey("RentalAssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentalData.Models.Subscriber", "Subscriber")
                        .WithMany("RentalHistory")
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RentalData.Models.Subscriber", b =>
                {
                    b.HasOne("RentalData.Models.ShippingRegion", "ShippingRegion")
                        .WithMany("Subscribers")
                        .HasForeignKey("ShippingRegionId");
                });
#pragma warning restore 612, 618
        }
    }
}
