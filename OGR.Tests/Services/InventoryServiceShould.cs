using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RentalData;
using RentalData.Models;
using RentalServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
namespace UnitTests.Services
{
    [TestFixture]
    class InventoryServiceShould
    {
        [Test]
        public void Add_Inventory_To_Db()
        {
            var mockDbSet = new Mock<DbSet<Inventory>>();
            var mockContext = new Mock<RentalContext>();

            mockContext.Setup(d => d.Inventory).Returns(mockDbSet.Object);
            var svc = new InventoryService(mockContext.Object);

            svc.Add(new Inventory());

            mockContext.Verify(a => a.Add(It.IsAny<Inventory>()), Times.Once);
            mockContext.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Test]
        public void Get_All_Inventory_Guitar_Types()
        {
            var guitars = GetGuitars().AsQueryable();
            var inv = GetInventories().AsQueryable();
            var type = "Electric";

            var mockDbSetGuitars = new Mock<DbSet<Guitar>>();
            mockDbSetGuitars.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSetGuitars.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSetGuitars.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSetGuitars.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockDbSetInv = new Mock<DbSet<Inventory>>();
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Guitars).Returns(mockDbSetGuitars.Object);
            mockContext.Setup(r => r.Inventory).Returns(mockDbSetInv.Object);

            var service = new InventoryService(mockContext.Object);
            var types = service.GetAllType(type);

            types.Should().HaveCount(2);
            types.Should().Contain(i => i.RentalAsset.Name == "Fender Appender");
            types.Should().Contain(i => i.RentalAsset.Name == "Bobby Gibson");
        }

        [Test]
        public void Get_Inventory_By_Price()
        {
            var inv = GetInventories().AsQueryable();

            var mockDbSetInv = new Mock<DbSet<Inventory>>();
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSetInv.Object);

            var service = new InventoryService(mockContext.Object);
            var results = service.GetByPrice(555.99);

            results.Should().HaveCount(1);
            results.FirstOrDefault().RentalAsset.Name.Should().Be("Fender Appender");

        }

        [Test]
        public void Get_Inventory_By_PriceRange()
        {
            var inv = GetInventories().AsQueryable();

            var mockDbSetInv = new Mock<DbSet<Inventory>>();
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSetInv.Object);

            var service = new InventoryService(mockContext.Object);
            var results = service.GetByPrice(500, 600);

            results.Should().HaveCount(1);
            results.FirstOrDefault().RentalAsset.Name.Should().Be("Fender Appender");
        }

        [Test]
        public void Get_Inventory_By_Stock()
        {
            var inv = GetInventories().AsQueryable();

            var mockDbSetInv = new Mock<DbSet<Inventory>>();
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSetInv.Object);

            var service = new InventoryService(mockContext.Object);
            var results = service.GetByStock(16);

            results.Should().HaveCount(1);
            results.FirstOrDefault().RentalAsset.Name.Should().Be("Acoustic Guitar");

        }

        [Test]
        public void Get_Inventory_By_StockRange()
        {
            var inv = GetInventories().AsQueryable();

            var mockDbSetInv = new Mock<DbSet<Inventory>>();
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSetInv.Object);

            var service = new InventoryService(mockContext.Object);
            var results = service.GetByStock(13, 18);

            results.Should().HaveCount(1);
            results.FirstOrDefault().RentalAsset.Name.Should().Be("Acoustic Guitar");

        }

        [Test]
        public void Get_Inventory_By_AssetId()
        {
            var inv = GetInventories().AsQueryable();

            var mockDbSetInv = new Mock<DbSet<Inventory>>();
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSetInv.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSetInv.Object);

            var service = new InventoryService(mockContext.Object);
            var results = service.GetByAssetId(1);

            results.Price.Should().Be(555.99);
            results.Stock.Should().Be(12);
        }

        [Test]
        public void Get_Inventory_Asset_Name()
        {
            var assets = GetGuitars().AsQueryable();

            var mockDbSet = new Mock<DbSet<RentalAsset>>();
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.Provider).Returns(assets.Provider);
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.Expression).Returns(assets.Expression);
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.ElementType).Returns(assets.ElementType);
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.GetEnumerator()).Returns(assets.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.RentalAssets).Returns(mockDbSet.Object);

            var service = new InventoryService(mockContext.Object);
            var name = service.GetAssetName(4);

            name.Should().Be("Bobby Gibson");
        }

        [Test]
        public void Get_Inventory_Asset_Brand()
        {
            var guitars = GetGuitars().AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Guitars).Returns(mockDbSet.Object);

            var service = new InventoryService(mockContext.Object);
            var name = service.GetAssetBrand(4);

            name.Should().Be("Gibson");
        }

        [Test]
        public void Get_Inventory_Asset_Style()
        {
            var guitars = GetGuitars().AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Guitars).Returns(mockDbSet.Object);

            var service = new InventoryService(mockContext.Object);
            var name = service.GetAssetStyle(4);

            name.Should().Be("Cool Blue");
        }

        [Test]
        public void Get_Inventory_Asset_Type()
        {
            var guitars = GetGuitars().AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Guitars).Returns(mockDbSet.Object);

            var service = new InventoryService(mockContext.Object);
            var name = service.GetAssetType(4);

            name.Should().Be("Electric");
        }

        private static IEnumerable<Inventory> GetInventories()
        {
            return new List<Inventory>()
            {
                new Inventory
                {
                    Id = 0,
                    RentalAsset = GetGuitars().FirstOrDefault(i => i.Id == 1),
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 0),
                    Price = 555.99,
                    Stock = 12
                },

                new Inventory
                {
                    Id = 1,
                    RentalAsset = GetGuitars().FirstOrDefault(i => i.Id == 4),
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 1),
                    Price = 799.99,
                    Stock = 5
                },

                new Inventory
                {
                    Id = 2,
                    RentalAsset = GetGuitars().FirstOrDefault(i => i.Id == 3),
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 2),
                    Price = 199.00,
                    Stock = 16
                },
            };
        }

        private static IEnumerable<Guitar> GetGuitars()
        {
            return new List<Guitar>()
            {
                new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender",
                    Type = "Electric",
                    Brand = "Fender",
                    Style = "Hot Pink"
                },

                new Guitar
                {
                    Id = 4,
                    Name = "Bobby Gibson",
                    Type = "Electric",
                    Brand = "Gibson",
                    Style = "Cool Blue"
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar",
                    Type = "Acoustic",
                    Brand = "Rogue",
                    Style = "Black"

                }
            };
        }

        private static IEnumerable<DistributionCenter> GetDistributionCenters()
        {
            return new List<DistributionCenter>()
            {
                new DistributionCenter
                {
                    Id = 0,
                    Address = "123 Scott Street, Orlando, FL, 12345",
                    Name = "Random Warehouse One",
                    Telephone = "123-456-7890",
                    ShippingRegion = new ShippingRegion
                    {
                        Id = 2,
                        Abbrv = "TX",
                        Region = "SW",
                        States = "Texas"
                    },
                },

                new DistributionCenter
                {
                    Id = 1,
                    Address = "432 Scott Street, Orlando, FL, 12345",
                    Name = "Random Warehouse Two",
                    Telephone = "123-654-0987",
                    ShippingRegion = new ShippingRegion
                    {
                        Id = 2,
                        Abbrv = "TX",
                        Region = "SW",
                        States = "Texas"
                    },
                },

                new DistributionCenter
                {
                    Id = 2,
                    Address = "188 Bartt Street, Columbus, OH, 54321",
                    Name = "Random Shipping Center One",
                    Telephone = "987-654-3210",
                     ShippingRegion = new ShippingRegion
                    {
                        Id = 1,
                        States = "New York",
                        Abbrv = "NY",
                        Region = "NE"
                    },
                }
            };
        }
    }
}
