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

namespace UnitTests.Controllers
{
    [TestFixture]
    class DistributionControllerShould
    {
        [Test]
        public void Return_Distribution_Index_View()
        {
            var mockDistributionService = new Mock<IDistribution>();
            mockDistributionService.Setup(c => c.GetAll()).Returns(GetAllDistributions());
            var controller = new DistributionController(mockDistributionService.Object);

            var result = controller.Index("");

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<DistributionIndexModel>();
            viewModel.Subject.DistributionCenters.Count().Should().Be(3);
        }

        [Test]
        public void Return_DistributionIndexModel()
        {
            var mockDistributionService = new Mock<IDistribution>();
            mockDistributionService.Setup(r => r.GetAll()).Returns(GetAllDistributions());
            var controller = new DistributionController(mockDistributionService.Object);

            var result = controller.Index("");

            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<DistributionIndexModel>();
        }

        [Test]
        public void Return_Distribution_Detail_View()
        {
            var mockDistributionService = new Mock<IDistribution>();
            mockDistributionService.Setup(r => r.Get(123)).Returns(GetDistributionCenter());
            var controller = new DistributionController(mockDistributionService.Object);

            var result = controller.Detail(123);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<DistributionDetailModel>();
            viewModel.Subject.Address.Should().Be("123 Example Street, Boston MA, 12345");
            viewModel.Subject.Telephone.Should().Be("123-456-7890");
        }

        [Test]
        public void Return_DistributionDetailModel()
        {
            var mockDistributionService = new Mock<IDistribution>();
            mockDistributionService.Setup(r => r.Get(123)).Returns(GetDistributionCenter());
            var controller = new DistributionController(mockDistributionService.Object);

            var result = controller.Detail(123);
            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<DistributionDetailModel>();
        }

        private static IEnumerable<DistributionCenter> GetAllDistributions()
        {
            return new List<DistributionCenter>()
                {
                    new DistributionCenter
                    {
                        Id = 123,
                        Name = "Example Distribution Center Uno",
                        Address = "123 Example Street, Boston MA, 12345",
                        ImageUrl = "",
                        Telephone = "123-456-7890"
                    },

                    new DistributionCenter
                    {
                        Id = 321,
                        Name = "Example Distribution Center Dos",
                        Address = "666 Devil Street, Upstate NY, 666666",
                        ImageUrl = "",
                        Telephone = "666-888-1234"
                    },

                    new DistributionCenter
                    {
                        Id = 543,
                        Name = "Example Warehouse",
                        Address = "777 Paradise Lane, Houston TX, 456321",
                        ImageUrl = "",
                        Telephone = "418-937-4930"
                    }
                };
        }

        private static DistributionCenter GetDistributionCenter()
        {
            return new DistributionCenter
            {
                Id = 123,
                Name = "Example Distribution Center Uno",
                Address = "123 Example Street, Boston MA, 12345",
                ImageUrl = "",
                Telephone = "123-456-7890"
            };
        }
    }
}
