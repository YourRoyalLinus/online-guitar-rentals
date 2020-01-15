using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RentalData;
using RentalData.Models;
using RentalServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace UnitTests.Services
{
    [TestFixture]
    class RentalAssetServiceShould
    {
        [Test]
        public void Add_RentalAsset_To_Db()
        {
            var mockDbset = new Mock<DbSet<RentalAsset>>();
            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.RentalAssets).Returns(mockDbset.Object);

            var service = new RentalAssetService(mockContext.Object);
            service.Add(new Guitar());

            mockContext.Verify(a => a.Add(It.IsAny<RentalAsset>()), Times.Once);
            mockContext.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Test]
        public void Get_RentalAsset_Name()
        {
            var assets = GetRentalAssets().AsQueryable();
            var asset = assets.FirstOrDefault(a => a.Id == 1);

            var mockDbset = new Mock<DbSet<RentalAsset>>();
            mockDbset.As<IQueryable<RentalAsset>>().Setup(p => p.Provider).Returns(assets.Provider);
            mockDbset.As<IQueryable<RentalAsset>>().Setup(p => p.Expression).Returns(assets.Expression);
            mockDbset.As<IQueryable<RentalAsset>>().Setup(p => p.ElementType).Returns(assets.ElementType);
            mockDbset.As<IQueryable<RentalAsset>>().Setup(p => p.GetEnumerator()).Returns(assets.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.RentalAssets).Returns(mockDbset.Object);

            var service = new RentalAssetService(mockContext.Object);
            var name = service.GetName(asset.Id);

            name.Should().Be("Fender Appender");
        }

        [Test]
        public void Get_RentalAsset_Description()
        {
            var assets = GetRentalAssets().AsQueryable();
            var asset = assets.FirstOrDefault(a => a.Id == 1);

            var mockDbSet = new Mock<DbSet<RentalAsset>>();
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.Provider).Returns(assets.Provider);
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.Expression).Returns(assets.Expression);
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.ElementType).Returns(assets.ElementType);
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.GetEnumerator()).Returns(assets.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.RentalAssets).Returns(mockDbSet.Object);

            var service = new RentalAssetService(mockContext.Object);
            var desc = service.GetDescription(asset.Id);

            desc.Should().Be("Neat");
        }

        [Test]
        public void Get_RentalAsset_Availability()
        {
            var assets = GetRentalAssets().AsQueryable();
            var asset = assets.FirstOrDefault(a => a.Id == 4);

            var mockDbSet = new Mock<DbSet<RentalAsset>>();
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.Provider).Returns(assets.Provider);
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.Expression).Returns(assets.Expression);
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.ElementType).Returns(assets.ElementType);
            mockDbSet.As<IQueryable<RentalAsset>>().Setup(p => p.GetEnumerator()).Returns(assets.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.RentalAssets).Returns(mockDbSet.Object);

            var service = new RentalAssetService(mockContext.Object);
            var avail = service.GetAvailable(asset.Id);

            avail.Should().BeFalse();
        }

        [Test]
        public void Get_RentalAsset_Price()
        {
            var inv = GetInventories().AsQueryable();

            var mockDbSet = new Mock<DbSet<Inventory>>();
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSet.Object);

            var service = new RentalAssetService(mockContext.Object);
            var price = service.GetPrice(3);

            price.Should().Be(199.99);
        }

        [Test]
        public void Get_RentalAsset_Stock()
        {
            var inv = GetInventories().AsQueryable();

            var mockDbSet = new Mock<DbSet<Inventory>>();
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSet.Object);

            var service = new RentalAssetService(mockContext.Object);
            var price = service.GetStock(3);

            price.Should().Be(16);
        }

        private static IEnumerable<RentalAsset> GetRentalAssets()
        {
            return new List<RentalAsset>()
            {
                new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender",
                    Type = "Electric",
                    Brand = "Fender",
                    Style = "Hot Pink",
                    Description = "Neat",
                    Available = 1,
                    Rating = 4.0F
                },

                new Guitar
                {
                    Id = 4,
                    Name = "Bobby Gibson",
                    Type = "Electric",
                    Brand = "Gibson",
                    Style = "Cool Blue",
                    Description = "Great",
                    Available = 0,
                    Rating = 4.8F
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar",
                    Type = "Acoustic",
                    Brand = "Rogue",
                    Style = "Black",
                    Description = "Meh",
                    Available = 1,
                    Rating = 3.2F

                }
            };
        }

        private static IEnumerable<Inventory> GetInventories()
        {
            return new List<Inventory>()
            {
                new Inventory
                {
                    Id = 0,
                    RentalAsset = GetRentalAssets().FirstOrDefault(i => i.Id == 1),
                    Price = 555.99,
                    Stock = 4
                },

                new Inventory
                {
                    Id = 1,
                    RentalAsset = GetRentalAssets().FirstOrDefault(i => i.Id == 4),
                    Price = 799.99,
                    Stock = 0
                },

                new Inventory
                {
                    Id = 2,
                    RentalAsset = GetRentalAssets().FirstOrDefault(i => i.Id == 3),
                    Price = 199.99,
                    Stock = 16
                },
            };
        }
    }
}
