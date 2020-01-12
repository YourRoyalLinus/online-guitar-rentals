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
    class DistributionServiceShould
    {
        [Test]
        public void Add_DistributionCenter_To_Db()
        {
            var mockDbSet = new Mock<DbSet<DistributionCenter>>();
            var mockContext = new Mock<RentalContext>();

            mockContext.Setup(d => d.DistributionCenters).Returns(mockDbSet.Object);
            var svc = new DistributionService(mockContext.Object);

            svc.Add(new DistributionCenter());

            mockContext.Verify(a => a.Add(It.IsAny<DistributionCenter>()), Times.Once);
            mockContext.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Test]
        public void Get_Couriers_For_DistributionCenter()
        {
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 1);
            var couriers = GetCouriers().AsQueryable();

            var mockDbSet = new Mock<DbSet<Courier>>();
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.Provider).Returns(couriers.Provider);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.Expression).Returns(couriers.Expression);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.ElementType).Returns(couriers.ElementType);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.GetEnumerator()).Returns(couriers.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(d => d.Couriers).Returns(mockDbSet.Object);

            var svc = new DistributionService(mockContext.Object);
            var result = svc.GetCouriers(center.Id);

            result.Should().HaveCount(2);
        }

        [Test]
        public void Get_Subscribers_Of_DistributionCenter()
        {
            var subs = GetSubscribers().AsQueryable();
            var centers = GetDistributionCenters().AsQueryable();
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 1);

            var mockDbSetSubs = new Mock<DbSet<Subscriber>>();
            mockDbSetSubs.As<IQueryable<Subscriber>>().Setup(p => p.Provider).Returns(subs.Provider);
            mockDbSetSubs.As<IQueryable<Subscriber>>().Setup(p => p.Expression).Returns(subs.Expression);
            mockDbSetSubs.As<IQueryable<Subscriber>>().Setup(p => p.ElementType).Returns(subs.ElementType);
            mockDbSetSubs.As<IQueryable<Subscriber>>().Setup(p => p.GetEnumerator()).Returns(subs.GetEnumerator);

            var mockDbSetCenters = new Mock<DbSet<DistributionCenter>>();
            mockDbSetCenters.As<IQueryable<DistributionCenter>>().Setup(p => p.Provider).Returns(centers.Provider);
            mockDbSetCenters.As<IQueryable<DistributionCenter>>().Setup(p => p.Expression).Returns(centers.Expression);
            mockDbSetCenters.As<IQueryable<DistributionCenter>>().Setup(p => p.ElementType).Returns(centers.ElementType);
            mockDbSetCenters.As<IQueryable<DistributionCenter>>().Setup(p => p.GetEnumerator()).Returns(centers.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Subscribers).Returns(mockDbSetSubs.Object);
            mockContext.Setup(r => r.DistributionCenters).Returns(mockDbSetCenters.Object);

            var service = new DistributionService(mockContext.Object);
            var subscribers = service.GetSubscribers(center.Id);

            subscribers.Should().HaveCount(2);
            subscribers.Should().Contain(r => r.FirstName == "Joe");
            subscribers.Should().Contain(r => r.FirstName == "Barb");
        }

        [Test]
        public void Get_Rental_Assets_Of_DistributionCenter()
        {
            var inv = GetInventories().AsQueryable();
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 1);
            var asset = 2;


            var mockDbSet = new Mock<DbSet<Inventory>>();
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var assets = service.GetAssets(center.Id, asset);

            assets.Should().HaveCount(1);
            assets.First().Price.Should().Be(600);
            assets.First().Stock.Should().Be(5);
        } 
        
        [Test]
        public void Get_TotalAssetValue_Of_DistributionCenter() 
        {
            var inv = GetInventories().AsQueryable();
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 1);

            var mockDbSet = new Mock<DbSet<Inventory>>();
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var assets = service.GetTotalAssetValue(center.Id);

            assets.Should<double>().Be(3000.0);
        }

        [Test]
        public void Get_TotalStock_Of_DistributionCenter()
        {
            var inv = GetInventories().AsQueryable();
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 0 );

            var mockDbSet = new Mock<DbSet<Inventory>>();
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var stock = service.GetTotalStock(center.Id);

            stock.Should<int>().Be(10);
        }

        [Test]
        public void Get_AssetPrice_Of_DistributionCenter()
        {
            var inv = GetInventories().AsQueryable();
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 2);
            var asset = 3;


            var mockDbSet = new Mock<DbSet<Inventory>>();
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var assetPrice = service.GetAssetPrice(center.Id, asset);

            assetPrice.Should<double>().Be(200.0);
        }

        [Test]
        public void Get_AssetStock_Of_DistributionCenter()
        {
            var inv = GetInventories().AsQueryable();
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 2);
            var asset = 3;


            var mockDbSet = new Mock<DbSet<Inventory>>();
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Provider).Returns(inv.Provider);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.Expression).Returns(inv.Expression);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.ElementType).Returns(inv.ElementType);
            mockDbSet.As<IQueryable<Inventory>>().Setup(p => p.GetEnumerator()).Returns(inv.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Inventory).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var assetStock = service.GetAssetStock(center.Id, asset);

            assetStock.Should<int>().Be(11);
        }

        [Test]
        public void Get_DeliveryStates_For_DistributionCenter()
        {
            var centers = GetDistributionCenters().AsQueryable();
            var center = centers.FirstOrDefault(c => c.Id == 2);

            var mockDbSet = new Mock<DbSet<DistributionCenter>>();
            mockDbSet.As<IQueryable<DistributionCenter>>().Setup(p => p.Provider).Returns(centers.Provider);
            mockDbSet.As<IQueryable<DistributionCenter>>().Setup(p => p.Expression).Returns(centers.Expression);
            mockDbSet.As<IQueryable<DistributionCenter>>().Setup(p => p.ElementType).Returns(centers.ElementType);
            mockDbSet.As<IQueryable<DistributionCenter>>().Setup(p => p.GetEnumerator()).Returns(centers.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.DistributionCenters).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var states = service.GetDeliveryStates(center.Id);

            states.Should().Be("New York");
        }

        [Test]
        public void Get_DeliveryRegion_For_DistributionCenter()
        {
            var centers = GetDistributionCenters().AsQueryable();
            var center = centers.FirstOrDefault(c => c.Id == 2);

            var mockDbSet = new Mock<DbSet<DistributionCenter>>();
            mockDbSet.As<IQueryable<DistributionCenter>>().Setup(p => p.Provider).Returns(centers.Provider);
            mockDbSet.As<IQueryable<DistributionCenter>>().Setup(p => p.Expression).Returns(centers.Expression);
            mockDbSet.As<IQueryable<DistributionCenter>>().Setup(p => p.ElementType).Returns(centers.ElementType);
            mockDbSet.As<IQueryable<DistributionCenter>>().Setup(p => p.GetEnumerator()).Returns(centers.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.DistributionCenters).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var region = service.GetDeliveryRegion(center.Id);

            region.Should().Be("NE");
        }

        [Test]
        public void Get_DeliveryHours_For_DistributionCenter()
        {
            var couriers = GetCouriers().AsQueryable();
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 0);


            var mockDbSet = new Mock<DbSet<Courier>>();
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.Provider).Returns(couriers.Provider);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.Expression).Returns(couriers.Expression);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.ElementType).Returns(couriers.ElementType);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.GetEnumerator()).Returns(couriers.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Couriers).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var results = service.GetDeliveryHours(center.Id);

            results.Should().HaveCount(5);
        }

        [Test]
        public void GetCourierNames_For_DistributionCenter()
        {
            var couriers = GetCouriers().AsQueryable(); 
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 0);

            var mockDbSet = new Mock<DbSet<Courier>>();
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.Provider).Returns(couriers.Provider);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.Expression).Returns(couriers.Expression);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.ElementType).Returns(couriers.ElementType);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.GetEnumerator()).Returns(couriers.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Couriers).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var results = service.GetCourierNames(center.Id);

            results.Should().HaveLength(4);
            results.Should().Contain("USPS");

        }

        [Test]
        public void Get_IsDelivering_For_DistributionCenter()
        {
            var couriers = GetCouriers().AsQueryable();
            var center = GetDistributionCenters().FirstOrDefault(c => c.Id == 1);

            var mockDbSet = new Mock<DbSet<Courier>>();
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.Provider).Returns(couriers.Provider);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.Expression).Returns(couriers.Expression);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.ElementType).Returns(couriers.ElementType);
            mockDbSet.As<IQueryable<Courier>>().Setup(p => p.GetEnumerator()).Returns(couriers.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Couriers).Returns(mockDbSet.Object);

            var service = new DistributionService(mockContext.Object);
            var delivery = service.IsDelivering(center.Id);

            switch(DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    delivery.Should().BeTrue();
                    break;
                case DayOfWeek.Sunday:
                    delivery.Should().BeTrue();
                    break;
                case DayOfWeek.Monday:
                    delivery.Should().BeFalse();
                    break;
                case DayOfWeek.Tuesday:
                    delivery.Should().BeFalse();
                    break;
                case DayOfWeek.Wednesday:
                    delivery.Should().BeFalse();
                    break;
                case DayOfWeek.Thursday:
                    delivery.Should().BeFalse();
                    break;
                case DayOfWeek.Friday:
                    delivery.Should().BeFalse();
                    break;
                default:
                    delivery.Should().BeFalse();
                    break;
            }
        }

        //Helper Functions
        private static IEnumerable<Courier> GetCouriers()
        {
            DateTime dt = DateTime.Now;

            return new List<Courier>()
            {
                new Courier
                {
                    Id = 0,
                    DayOfWeek = 1,
                    DeliveryStartTime = dt,
                    DeliveryEndTime = dt,
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 0),
                    Name = "USPS"
                  
                },
                new Courier
                {
                    Id = 1,
                    DayOfWeek = 2,
                    DeliveryStartTime = dt,
                    DeliveryEndTime = dt,
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 0),
                    Name = "USPS"

                },

                new Courier
                {
                    Id = 2,
                    DayOfWeek = 3,
                    DeliveryStartTime = dt,
                    DeliveryEndTime = dt,
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 0),
                    Name = "USPS"

                },

                new Courier
                {
                    Id = 0,
                    DayOfWeek = 4,
                    DeliveryStartTime = dt,
                    DeliveryEndTime = dt,
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 0),
                    Name = "USPS"

                },

                new Courier
                {
                    Id = 3,
                    DayOfWeek = 5,
                    DeliveryStartTime = dt,
                    DeliveryEndTime = dt,
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 0),
                    Name = "USPS"

                },

                new Courier
                {
                    Id = 4,
                    DayOfWeek = 6,
                    DeliveryStartTime = dt,
                    DeliveryEndTime = dt.AddHours(5),
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 1),
                    Name = "FedEx"

                }
                ,

                new Courier
                {
                    Id = 5,
                    DayOfWeek = 0,
                    DeliveryStartTime = dt,
                    DeliveryEndTime = dt.AddHours(5),
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 1),
                    Name = "FedEx"

                }
            };
        }
        private static IEnumerable<Subscriber> GetSubscribers()
        {
            return new List<Subscriber>()
            {
                new Subscriber
                {
                    Id = 1,
                    ShippingRegion = new ShippingRegion
                    {
                        Id = 1,
                        States = "New York",
                        Abbrv = "NY",
                        Region = "NE",
                        DistributionCenter = GetDistributionCenters().Where(s => s.ShippingRegion.Id == 1)
                    },
                    FirstName = "Mary",
                    LastName = "Sue"
                },

                new Subscriber
                {
                    Id = 2,
                     ShippingRegion = new ShippingRegion
                    {
                        Id = 1,
                        States = "New York",
                        Abbrv = "NY",
                        Region = "NE",
                        DistributionCenter = GetDistributionCenters().Where(s => s.ShippingRegion.Id == 1)
                    },
                    FirstName = "Gary",
                    LastName = "Sue"
                },

                new Subscriber
                {
                    Id = 3,
                    ShippingRegion = new ShippingRegion
                    {
                        Id = 2,
                        Abbrv = "TX",
                        Region = "SW",
                        States = "Texas",
                        DistributionCenter = GetDistributionCenters().Where(s => s.ShippingRegion.Id == 2)
                    },
                    FirstName = "Joe",
                    LastName = "Brown"
                },

                new Subscriber
                {
                    Id = 4,
                    ShippingRegion = new ShippingRegion
                    {
                        Id = 2,
                        Abbrv = "TX",
                        Region = "SW",
                        States = "Texas",
                        DistributionCenter = GetDistributionCenters().Where(s => s.ShippingRegion.Id == 2)
                    },
                    FirstName = "Barb",
                    LastName = "Brown"
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
                    RentalAsset = new Guitar() {Id = 1, Name = "Electric Guitar"},
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 0),
                    Price = 400,
                    Stock = 10
                },

                new Inventory
                {
                    Id = 1,
                    RentalAsset = new Guitar() {Id = 2, Name = "Bass Guitar"},
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 1),
                    Price = 600,
                    Stock = 5
                },

                new Inventory
                {
                    Id = 2,
                    RentalAsset = new Guitar() {Id = 3, Name = "Acoustic Guitar"},
                    DistributionCenter = GetDistributionCenters().FirstOrDefault(c => c.Id == 2),
                    Price = 200,
                    Stock = 11
                },
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
