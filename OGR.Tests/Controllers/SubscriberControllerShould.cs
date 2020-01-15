using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RentalData;
using RentalData.Models;
using OnlineGuitarRentals.Controllers;
using OnlineGuitarRentals.Models.Distribution;
using Microsoft.AspNetCore.Mvc;
using OnlineGuitarRentals.Models.Subscriber;
using System.Data.SqlTypes;
using System;

namespace UnitTests.Controllers
{
    [TestFixture]
    class SubscriberControllerShould
    {
        [Test]
        public void Return_Subscriber_Index_View()
        {
            var mockSubscriberService = new Mock<ISubscriber>();
            mockSubscriberService.Setup(r => r.GetAll()).Returns(GetAllSubscribers());
            var controller = new SubscriberController(mockSubscriberService.Object);

            var result = controller.Index("");

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<SubscriberIndexModel>();
            viewModel.Subject.Subscriber.Count().Should().Be(2);
        }

        [Test]
        public void Return_SubscriberIndexModel()
        {
            var mockSubscriberService = new Mock<ISubscriber>();
            mockSubscriberService.Setup(r => r.GetAll()).Returns(GetAllSubscribers());
            var controller = new SubscriberController(mockSubscriberService.Object);

            var result = controller.Index("");

            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<SubscriberIndexModel>();
        }

        [Test]
        public void Return_Subscriber_Detail_View()
        {
            var mockSubscriberService = new Mock<ISubscriber>();
            mockSubscriberService.Setup(r => r.Get(429)).Returns(GetSubscriber());
            mockSubscriberService.Setup(r => r.GetRentals(429)).Returns(new List<Rental> { });
            mockSubscriberService.Setup(r => r.GetRentalHistories(429)).Returns(new List<RentalHistory> { });
            mockSubscriberService.Setup(r => r.GetHolds(429)).Returns(new List<Hold> { });
            var controller = new SubscriberController(mockSubscriberService.Object);

            var result = controller.Detail(429);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<SubscriberDetailModel>();
            viewModel.Subject.FirstName.Should().Be("Jim");
            viewModel.Subject.LastName.Should().Be("Brown");
        }

        [Test]
        public void Return_Subscriber_DetailModel()
        {
            var mockSubscriberService = new Mock<ISubscriber>();
            mockSubscriberService.Setup(r => r.Get(521)).Returns(GetSubscriber());
            var controller = new SubscriberController(mockSubscriberService.Object);

            var result = controller.Detail(521);

            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<SubscriberDetailModel>();
        }

        [Test]
        public void Display_Nameless_Subscriber() 
        {
            var mockSubscriberService = new Mock<ISubscriber>();
            mockSubscriberService.Setup(r => r.Get(429)).Returns(GetNamelessSubscriber());
            var controller = new SubscriberController(mockSubscriberService.Object);

            var result = controller.Detail(429);


            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<SubscriberDetailModel>();
        }

        [Test]
        public void Display_Infoless_Subscriber() 
        {
            var mockSubscriberService = new Mock<ISubscriber>();
            mockSubscriberService.Setup(r => r.Get(429)).Returns(GetInfolessSubscriber());
            var controller = new SubscriberController(mockSubscriberService.Object);

            var result = controller.Detail(429);


            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<SubscriberDetailModel>();
        }

        private static IEnumerable<Subscriber> GetAllSubscribers()
        {
            return new List<Subscriber>
            {
                new Subscriber
                {
                    Id = 429,
                    FirstName = "Jim",
                    LastName = "Brown",
                    Address = "777 Fake Street, Dallas TX, 12345",
                    Email = "Fake@Yahoo.com",
                    Telephone = "815-739-7518",
                    DateOfBirth = DateTime.Today,
                    ShippingRegion = new ShippingRegion
                    {
                        Id = 500,
                        Abbrv = "NY, RI",
                        Region = "NE",
                        States = "New York, Rhode Island"
                    },
                    RenewalDate = DateTime.Today,
                    ExperationDate = DateTime.Today,
                    Active = 1
                },

                new Subscriber
                {
                    Id = 521,
                    FirstName = "Alexandria",
                    LastName = "Hummels",
                    Address = "777 Fake Street, Dallas TX, 12345",
                    Email = "Faux@Yahoo.com",
                    Telephone = "617-814-7364",
                    DateOfBirth = DateTime.Today,
                    ShippingRegion = new ShippingRegion
                    {
                        Id = 500,
                        Abbrv = "TX, AZ",
                        Region = "SW",
                        States = "Texas, Arizona"
                    },
                    RenewalDate = DateTime.Today,
                    ExperationDate = DateTime.Today,
                    Active = 0
                }
            };
        }

        private static Subscriber GetSubscriber()
        {
            return new Subscriber
            {
                Id = 429,
                FirstName = "Jim",
                LastName = "Brown",
                Address = "777 Fake Street, Dallas TX, 12345",
                Email = "Fake@Yahoo.com",
                Telephone = "815-739-7518",
                DateOfBirth = DateTime.Today,
                ShippingRegion = new ShippingRegion
                {
                    Id = 500,
                    Abbrv = "NY, RI",
                    Region = "NE",
                    States = "New York, Rhode Island"
                },
                RenewalDate = DateTime.Today,
                ExperationDate = DateTime.Today,
                Active = 1
            };
        }

        private static Subscriber GetNamelessSubscriber()
        {
            return new Subscriber
            {
                Id = 429,
                Address = "777 Fake Street, Dallas TX, 12345",
                Email = "Fake@Yahoo.com",
                Telephone = "815-739-7518",
                DateOfBirth = DateTime.Today,
                ShippingRegion = new ShippingRegion
                {
                    Id = 500,
                    Abbrv = "NY, RI",
                    Region = "NE",
                    States = "New York, Rhode Island"
                },
                RenewalDate = DateTime.Today,
                ExperationDate = DateTime.Today,
                Active = 1
            };
        }

        private static Subscriber GetInfolessSubscriber()
        {
            return new Subscriber 
            {
                Id = 429,
                FirstName = "Jim",
                LastName = "Brown"
            };
        }

        private static IEnumerable<User> GetAllUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 430,
                    FirstName = "Janice",
                    LastName = "Brown",
                    Address = "777 Fake Street, Dallas TX, 12345",
                    Email = "Quasi@Yahoo.com",
                    Telephone = "815-739-7518",
                    DateOfBirth = DateTime.Today
                },

                new User
                {
                    Id = 522,
                    FirstName = "Bradley",
                    LastName = "Hummels",
                    Address = "777 Fake Street, Dallas TX, 12345",
                    Email = "Pseudo@Yahoo.com",
                    Telephone = "617-814-7364",
                    DateOfBirth = DateTime.Today
                }
            };
        }

    }
}
