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
    class RentalServiceShould
    {
        [Test]
        public void Add_Rental_To_Db()
        {
            var mockDbSet = new Mock<DbSet<Rental>>();
            var mockContext = new Mock<RentalContext>();

            mockContext.Setup(r => r.Rentals).Returns(mockDbSet.Object);
            var svc = new RentalService(mockContext.Object);

            svc.Add(new Rental());

            mockContext.Verify(a => a.Add(It.IsAny<Rental>()), Times.Once);
            mockContext.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Test]
        public void Get_All_Rentals()
        {
            var rentals = new List<Rental>()
            {
                new Rental
                {
                    Id = 111,
                    Since = DateTime.Now,
                    Until = DateTime.Now
                },
                new Rental
                {
                    Id = 222,
                    Since = DateTime.Now,
                    Until = DateTime.Now
                },
                new Rental
                {
                    Id = 333,
                    Since = DateTime.Now,
                    Until = DateTime.Now
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Rental>>();
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.Provider).Returns(rentals.Provider);
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.Expression).Returns(rentals.Expression);
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.ElementType).Returns(rentals.ElementType);
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.GetEnumerator()).Returns(rentals.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Rentals).Returns(mockDbSet.Object);

            var service = new RentalService(mockContext.Object);
            var rental = service.GetAll();

            rental.Should().HaveCount(3);
            rental.Should().Contain(r => r.Id == 111);
            rental.Should().Contain(r => r.Id == 222);
            rental.Should().Contain(r => r.Id == 333);
        }

        [Test]
        public void Get_Rental_By_Id()
        {
            var dt = DateTime.Now;

            var rentals = new List<Rental>()
            {
                new Rental
                {
                    Id = 111,
                    Since = DateTime.Now,
                    Until = DateTime.Now
                },
                new Rental
                {
                    Id = 222,
                    Since = DateTime.Now,
                    Until = dt
                },
                new Rental
                {
                    Id = 333,
                    Since = DateTime.Now,
                    Until = DateTime.Now
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Rental>>();
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.Provider).Returns(rentals.Provider);
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.Expression).Returns(rentals.Expression);
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.ElementType).Returns(rentals.ElementType);
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.GetEnumerator()).Returns(rentals.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Rentals).Returns(mockDbSet.Object);

            var service = new RentalService(mockContext.Object);
            var rental = service.GetById(222);

            rental.Id.Should().Be(222);
            rental.Until.Should().Be(dt);
        }

        [Test]
        public void Get_Current_Rental_Subs()
        {
            var asset = new Guitar()
            {
                Id = 10,
                Type = "Bass",
                Name = "Bass Guitar"
            };

            var subOne = new Subscriber
            {
                Id = 1,
                FirstName = "Jim",
                LastName = "Brown"
            };

            var subTwo = new Subscriber
            {
                Id = 2,
                FirstName = "Michael",
                LastName = "James"
            };

            var subThree = new Subscriber
            {
                Id = 3,
                FirstName = "Sarah",
                LastName = "Shannons"
            };


            var rentals = new List<Rental>()
            {
                new Rental
                {
                    Id = 111,
                    Since = DateTime.Now,
                    Until = DateTime.Now,
                    Subscriber = subOne,
                    RentalAsset = asset
                },
                new Rental
                {
                    Id = 222,
                    Since = DateTime.Now,
                    Until = DateTime.Now,
                    Subscriber = subTwo,
                    RentalAsset = asset
                },
                new Rental
                {
                    Id = 333,
                    Since = DateTime.Now,
                    Until = DateTime.Now,
                    Subscriber = subThree,
                    RentalAsset = new Guitar()
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Rental>>();
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.Provider).Returns(rentals.Provider);
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.Expression).Returns(rentals.Expression);
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.ElementType).Returns(rentals.ElementType);
            mockDbSet.As<IQueryable<Rental>>().Setup(p => p.GetEnumerator()).Returns(rentals.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Rentals).Returns(mockDbSet.Object);

            var service = new RentalService(mockContext.Object);
            var rental = service.GetCurrentRentalSubs(asset.Id);

            rental.Should().HaveCount(2);
            rental.Should().Contain("Michael James");
            rental.Should().Contain("Jim Brown");
        }

        [Test]
        public void Get_Current_Holds()
        {
            var holds = GetCurrentHolds().AsQueryable();

            var mockDbSet = new Mock<DbSet<Hold>>();
            mockDbSet.As<IQueryable<Hold>>().Setup(p => p.Provider).Returns(holds.Provider);
            mockDbSet.As<IQueryable<Hold>>().Setup(p => p.Expression).Returns(holds.Expression);
            mockDbSet.As<IQueryable<Hold>>().Setup(p => p.ElementType).Returns(holds.ElementType);
            mockDbSet.As<IQueryable<Hold>>().Setup(p => p.GetEnumerator()).Returns(holds.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(r => r.Holds).Returns(mockDbSet.Object);

            var service = new RentalService(mockContext.Object);
            var hold = service.GetCurrentHolds(1);

            hold.Should().HaveCount(2);
            hold.Should().Contain(h => h.Subscriber.FirstName == "Mary");
            hold.Should().Contain(h => h.Subscriber.FirstName == "Brad");
        }

        //Helper Functions
        private static IEnumerable<Hold> GetCurrentHolds()
        {
            return new List<Hold>()
            {
                new Hold
                {
                    Id = 999,
                    HoldPlaced = DateTime.Now,
                    RentalAsset = new Guitar() { Id = 0 },
                    Subscriber = new Subscriber() { Id = 1, FirstName = "John", LastName = "Smith" },
                    DistributionCenter = new DistributionCenter(){Id = 5 }
                },
                new Hold
                {
                    Id = 777,
                    HoldPlaced = DateTime.Now,
                    RentalAsset = new Guitar() { Id = 1 },
                    Subscriber = new Subscriber() { Id = 1, FirstName = "Mary", LastName = "Smith" },
                    DistributionCenter = new DistributionCenter(){Id = 5 }
                },
                new Hold
                {
                    Id = 555,
                    HoldPlaced = DateTime.Now,
                    RentalAsset = new Guitar() { Id = 1 },
                    Subscriber = new Subscriber() { Id = 1, FirstName = "Brad", LastName = "Smith" },
                    DistributionCenter = new DistributionCenter()
                }
            };
        }
    }
}
