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
    class SubscriberServiceShould
    {
        [Test]
        public void Add_Subscriber_To_Db()
        {
            var mockDbSet = new Mock<DbSet<Subscriber>>();
            var mockContext = new Mock<RentalContext>();

            mockContext.Setup(r => r.Subscribers).Returns(mockDbSet.Object);
            var service = new SubscriberService(mockContext.Object);

            service.Add(new Subscriber());

            mockContext.Verify(a => a.Add(It.IsAny<Subscriber>()), Times.Once);
            mockContext.Verify(s => s.SaveChanges(), Times.Once);
        }
    }
}
