using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RentalData;
using RentalData.Models;
using OnlineGuitarRentals.Controllers;
using OnlineGuitarRentals.Models.Inventory;
using Microsoft.AspNetCore.Mvc;


namespace UnitTests.Controllers
{
    [TestFixture]
    class InventoryControllerShould
    {
        [Test]
        public void Return_Inventory_Index_View()
        {
            var mockInventoryService = new Mock<IInventory>();
            mockInventoryService.Setup(r => r.GetAll()).Returns(GetInventoryLedger());
            var controller = new InventoryController(mockInventoryService.Object);

            var result = controller.Index("");

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<InventoryIndexModel>();
            viewModel.Subject.Inventory.Count().Should().Be(2);
        }

        [Test]
        public void Return_SubscriberIndexModel()
        {
            var mockInventoryService = new Mock<IInventory>();
            mockInventoryService.Setup(r => r.GetAll()).Returns(GetInventoryLedger());
            var controller = new InventoryController(mockInventoryService.Object);

            var result = controller.Index("");

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<InventoryIndexModel>();
        }

        private static IEnumerable<Inventory> GetInventoryLedger()
        {
            return new List<Inventory>
            {
                new Inventory
                {
                    Id = 10,
                    Stock = 100,
                    Price = 499.99F,
                    RentalAsset = new Guitar
                    {
                        Id = 33,
                        Brand = "Gibson",
                        Name = "Guitar Name",
                        Style = "Pink",
                        Type = "Electric",
                        NumberOfStrings = 6

                    },
                    DistributionCenter = new DistributionCenter
                    {
                        Id = 88,
                        Name = "Faux Distro Center",
                        Address = "123 Lane Albany, NY, 12355",
                        Telephone = "123-456-7890"
                    }
                },
                new Inventory
                {
                    Id = 10,
                    Stock = 100,
                    Price = 499.99F,
                    RentalAsset = new Guitar
                    {
                        Id = 66,
                        Brand = "Fender",
                        Name = "Fender Name",
                        Style = "Maple",
                        Type = "Bass",
                        NumberOfStrings = 4

                    },
                    DistributionCenter = new DistributionCenter
                    {
                        Id = 99,
                        Name = "Fake Warehouse",
                        Address = "123 Street Boston, MA, 55321",
                        Telephone = "777-446-8890"
                    }
                }
            };
        }
    }
}
