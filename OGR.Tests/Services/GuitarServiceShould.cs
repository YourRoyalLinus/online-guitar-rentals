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
    class GuitarServiceShould
    {
         [Test]
         public void Add_New_Guitar_To_Db()
        {
            var mockDbSet = new Mock<DbSet<Guitar>>();
            var mockContext = new Mock<RentalContext>();

            mockContext.Setup(g => g.Guitars).Returns(mockDbSet.Object);
            var svc = new GuitarService(mockContext.Object);

            svc.Add(new Guitar());

            mockContext.Verify(a => a.Add(It.IsAny<Guitar>()), Times.Once);
            mockContext.Verify(s => s.SaveChanges(), Times.Once);
        }

        [Test]
        public void Get_Guitars_By_Id()
        {
            var guitars = new List<Guitar>()
            {
                new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender"
                },

                new Guitar
                {
                    Id = 2,
                    Name = "Bass with Face"
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar"
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(g => g.Guitars).Returns(mockDbSet.Object);

            var service = new GuitarService(mockContext.Object);
            var guitar = service.Get(2);

            guitar.Name.Should().Be("Bass with Face");
        }

        [Test]
        public void Get_Guitars_By_Type()
        {
            var guitars = new List<Guitar>()
            {
                new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender",
                    Type = "Electric"
                },

                new Guitar
                {
                    Id = 4,
                    Name = "Bobby Gibson",
                    Type = "Electric"
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar",
                    Type = "Acoustic"
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(g => g.Guitars).Returns(mockDbSet.Object);

            var service = new GuitarService(mockContext.Object);
            var queryResults = service.GetByType("Electric");

            queryResults.Should().HaveCount(2);
            queryResults.Should().Contain(g => g.Name == "Fender Appender");
            queryResults.Should().Contain(g => g.Name == "Bobby Gibson");
        }

        [Test]
        public void Get_Guitars_By_Style()
        {
            var guitars = new List<Guitar>()
            {
                new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender",
                    Type = "Electric",
                    Style = "Lagoon Blue"
                },

                new Guitar
                {
                    Id = 4,
                    Name = "Bobby Gibson",
                    Type = "Electric",
                    Style = "Lagoon Blue"
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar",
                    Type = "Acoustic",
                    Style = "Lagoon Blue"

                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(g => g.Guitars).Returns(mockDbSet.Object);

            var service = new GuitarService(mockContext.Object);
            var queryResults = service.GetByStyle("Lagoon Blue");

            queryResults.Should().HaveCount(3);
            queryResults.Should().Contain(g => g.Name == "Fender Appender");
            queryResults.Should().Contain(g => g.Name == "Bobby Gibson");
            queryResults.Should().Contain(g => g.Name == "Acoustic Guitar");
        }

        [Test]
        public void Get_Guitars_By_NumberStrings()
        {
            var guitars = new List<Guitar>()
            {
                new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender",
                    Type = "Electric",
                    NumberOfStrings = 6
                },

                new Guitar
                {
                    Id = 4,
                    Name = "Bobby Gibson",
                    Type = "Electric",
                    NumberOfStrings = 7
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar",
                    Type = "Acoustic",
                    NumberOfStrings = 6
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(g => g.Guitars).Returns(mockDbSet.Object);

            var service = new GuitarService(mockContext.Object);
            var queryResults = service.GetByNumberStrings(6);

            queryResults.Should().HaveCount(2);
            queryResults.Should().Contain(g => g.Name == "Fender Appender");
            queryResults.Should().Contain(g => g.Name == "Acoustic Guitar");
        }

        [Test]
        public void Get_Guitars_By_NumberStrings_Range()
        {
            var guitars = new List<Guitar>()
            {
                new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender",
                    Type = "Electric",
                    NumberOfStrings = 6
                },

                new Guitar
                {
                    Id = 4,
                    Name = "Bobby Gibson",
                    Type = "Electric",
                    NumberOfStrings = 7
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar",
                    Type = "Acoustic",
                    NumberOfStrings = 6
                },

                new Guitar
                {
                    Id = 2,
                    Name = "Bass with Face",
                    Type = "Bass",
                    NumberOfStrings = 4
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(g => g.Guitars).Returns(mockDbSet.Object);

            var service = new GuitarService(mockContext.Object);
            var queryResults = service.GetByNumberStrings(4, 6);

            queryResults.Should().HaveCount(3);
            queryResults.Should().Contain(g => g.Name == "Fender Appender");
            queryResults.Should().Contain(g => g.Name == "Acoustic Guitar");
            queryResults.Should().Contain(g => g.Name == "Bass with Face");
        }

        [Test]
        public void Get_Type()
        {
            var guitars = new List<Guitar>()
            {
                 new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender",
                    Type = "Electric",
                    Style = "Lagoon Blue"
                },

                new Guitar
                {
                    Id = 4,
                    Name = "Pinky",
                    Type = "Electric",
                    Style = "Pink"
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar",
                    Type = "Acoustic",
                    Style = "Lagoon Blue"

                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);
 
            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(g => g.Guitars).Returns(mockDbSet.Object);

            var service = new GuitarService(mockContext.Object);
            var style = service.GetType(1);

            style.Should().Be("Electric");
        }

        [Test]
        public void Get_NumberOfStrings()
        {
            var guitars = new List<Guitar>()
            {
                new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender",
                    Type = "Electric",
                    Style = "Lagoon Blue",
                    NumberOfStrings = 6
                },

                new Guitar
                {
                    Id = 4,
                    Name = "Pinky",
                    Type = "Electric",
                    Style = "Pink",
                    NumberOfStrings = 5
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar",
                    Type = "Acoustic",
                    Style = "Lagoon Blue",
                    NumberOfStrings = 6

                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(g => g.Guitars).Returns(mockDbSet.Object);

            var service = new GuitarService(mockContext.Object);
            var style = service.GetNumberStrings(4);

            style.Should().Be(5);
        }

        [Test]
        public void Get_Style()
        {
            var guitars = new List<Guitar>()
            {
                new Guitar
                {
                    Id = 1,
                    Name = "Fender Appender",
                    Type = "Electric",
                    Style = "Lagoon Blue"
                },

                new Guitar
                {
                    Id = 4,
                    Name = "Pinky",
                    Type = "Electric",
                    Style = "Pink"
                },

                new Guitar
                {
                    Id = 3,
                    Name = "Acoustic Guitar",
                    Type = "Acoustic",
                    Style = "Lagoon Blue"

                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Guitar>>();
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Provider).Returns(guitars.Provider);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.Expression).Returns(guitars.Expression);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.ElementType).Returns(guitars.ElementType);
            mockDbSet.As<IQueryable<Guitar>>().Setup(p => p.GetEnumerator()).Returns(guitars.GetEnumerator);

            var mockContext = new Mock<RentalContext>();
            mockContext.Setup(g => g.Guitars).Returns(mockDbSet.Object);

            var service = new GuitarService(mockContext.Object);
            var style = service.GetStyle(4);

            style.Should().Be("Pink");
        }
    }
}
