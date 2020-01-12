using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RentalData;
using RentalData.Models;
using OnlineGuitarRentals.Controllers;
using OnlineGuitarRentals.Models.Product;
using OnlineGuitarRentals.Models.Rentals;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTests.Controllers
{
    [TestFixture]
    class ProductControllerShould
    {
        [Test]
        public void Call_ReturnProduct_In_Service_When_ReturnProduct_Called()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            controller.ReturnProduct(66, 1);

            mockRentalService.Verify(s => s.ReturnProduct(66, 1), Times.Once());
        }

        [Test]
        public void Call_Rent_In_Service_When_Rent_Called()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            controller.RentOutProduct(66, 1);

            mockRentalService.Verify(s => s.RentProduct(66, 1), Times.Once());
        }

        [Test]
        public void Call_GetbyId_In_Service_When_Rent_Called()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);
            controller.Rent(66);

            mockRentalAssetService.Verify(s => s.GetById(66), Times.Once());
        }

        [Test]
        public void Call_GetbyId_In_Service_When_Hold_Called()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            controller.Hold(66);

            mockRentalAssetService.Verify(s => s.GetById(66), Times.Once());
        }

        [Test]
        public void Call_PlaceHold_In_Service_When_PlaceHold_Called()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            controller.PlaceHold(66, 1);

            mockRentalService.Verify(s => s.PlaceHold(66, 1), Times.Once());
        }

        [Test]
        public void Redirect_To_Detail_When_Rent_Called()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.RentOutProduct(66, 1);
            var redirectResult = result.Should().BeOfType<RedirectToActionResult>();

            redirectResult.Subject.ActionName.Should().Be("Detail");
        }

        
        [Test]
        public void Redirect_To_Detail_When_PlaceHold_Called()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.PlaceHold(66, 1);
            var redirectResult = result.Should().BeOfType<RedirectToActionResult>();

            redirectResult.Subject.ActionName.Should().Be("Detail");
        }
        
        [Test]
        public void Return_AssetIndexListingModel()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.Index("");
            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<AssetIndexModel>();
        }

        [Test]
        public void Return_ProductDetailModel()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.Detail(66);

            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<AssetDetailModel>();
        }

        [Test]
        public void Return_Product_Index_View()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetAll()).Returns(GetAllAssets());
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.Index("");
            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<AssetIndexModel>();
            viewModel.Subject.Assets.Count().Should().Be(3);
        }

        [Test]
        public void Return_ProductTypeElectric_Index_View()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();

            string type = "Electric";
            mockGuitarService.Setup(r => r.GetAllGuitars()).Returns(GetTypeAssets(type));

            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.TypeIndex("Electric", "");

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<GuitarIndexModel>();
            viewModel.Subject.Guitars.Count().Should().Be(1);
        }

        [Test]
        public void Return_ProductTypeBass_Index_View()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();

            string type = "Bass";
            mockGuitarService.Setup(r => r.GetAllGuitars()).Returns(GetTypeAssets(type));

            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.TypeIndex("Bass", "");

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<GuitarIndexModel>();
            viewModel.Subject.Guitars.Count().Should().Be(1);
        }
        
        [Test]
        public void Return_ProductTypeAcoustic_Index_View()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();

            string type = "Acoustic";
            mockGuitarService.Setup(r => r.GetAllGuitars()).Returns(GetTypeAssets(type));

            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.TypeIndex("Acoustic", "");

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<GuitarIndexModel>();
            viewModel.Subject.Guitars.Count().Should().Be(1);
        }

        [Test]
        public void Return_Return_View()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());

            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.Return(66);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<RentalModel>();
            viewModel.Subject.Name.Should().Be("Urbanizer Cheif Keith"); //Brand + Name
        }

        [Test]
        public void Return_Rent_View()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());

            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.Rent(66);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<RentalModel>();
            viewModel.Subject.Name.Should().Be("Urbanizer Cheif Keith"); //Brand + Name
        }

        [Test]
        public void Return_Hold_View()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());

            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.Hold(66);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<RentalModel>();
            viewModel.Subject.Name.Should().Be("Urbanizer Cheif Keith"); //Brand + Name
        }
        
        
        [Test]
        public void Return_RentalModel()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());

            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.Rent(66);

            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<RentalModel>();
        }

        [Test]
        public void Return_RentalModel_When_Hold_Called()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());

            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.Hold(66);

            var viewResult = result.Should().BeOfType<ViewResult>();
            viewResult.Subject.Model.Should().BeOfType<RentalModel>();
        }

        
        [Test]
        public void Return_DetailView_When_Return_Called()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());

            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.ReturnProduct(66, 1);
            var redirectResult = result.Should().BeOfType<RedirectToActionResult>();

            redirectResult.Subject.ActionName.Should().Be("Detail");
        }

 
        [Test]
        public void Return_RentalAsset_Detail_View()
        {
            var mockRentalAssetService = new Mock<IRentalAsset>();
            var mockRentalService = new Mock<IRental>();
            var mockGuitarService = new Mock<IGuitar>();
            mockRentalAssetService.Setup(r => r.GetById(66)).Returns(GetAsset());

            mockRentalService.Setup(r => r.GetCurrentHoldPlaced(66)).Returns(DateTime.Parse(GetCurrentHold().HoldPlace));
            mockRentalService.Setup(r => r.GetCurrentHoldSubscriberName(66)).Returns(GetCurrentHold().Subscriber);

            mockRentalService.Setup(r => r.GetRentalHistory(66)).Returns(new List<RentalHistory>
            {
                new RentalHistory()
            });

            mockRentalAssetService.Setup(r => r.GetBrand(66)).Returns("Fender");
            mockRentalAssetService.Setup(r => r.GetRating(66)).Returns(4.6F);

            mockRentalService.Setup(r => r.GetRentalHistory(66)).Returns(new List<RentalHistory>
            {
                new RentalHistory()
            });
            mockRentalService.Setup(r => r.GetRecentRental(66)).Returns(new Rental());
            mockRentalService.Setup(r => r.GetCurrentHoldSubscriberName(66)).Returns("Brad");
            var controller = new ProductController(mockRentalAssetService.Object, mockRentalService.Object, mockGuitarService.Object);

            var result = controller.Detail(66);

            var viewResult = result.Should().BeOfType<ViewResult>();
            var viewModel = viewResult.Subject.ViewData.Model.Should().BeAssignableTo<AssetDetailModel>();
            viewModel.Subject.Name.Should().Be("Cheif Keith"); //Brand + Name
        }

        private static IEnumerable<RentalAsset> GetAllAssets()
        {
            return new List<RentalAsset>
            {
                new Guitar
                {
                    Id = 100,
                    Brand = "Metallica",
                    Name = "Shredder 9000",
                    Type = "Electric",
                    Style = "Midnight Black",
                    NumberOfStrings = 6
                },

                new Guitar 
                {
                    Id = 200,
                    Brand = "Fender",
                    Name = "Fender Bender 500",
                    Type = "Bass",
                    Style = "Money Green",
                    NumberOfStrings = 4
                },

                new Guitar
                {
                    Id = 300,
                    Brand = "Urbanizer",
                    Name = "Cheif Keith",
                    Type = "Acoustic",
                    Style = "Country Road Brown",
                    NumberOfStrings = 6,
                    Rating = 4.6F
                }
            };
        }

        private static IEnumerable<Guitar> GetTypeAssets(string type)
        {
            var guitars = new List<Guitar>
            {
                new Guitar
                {
                    Id = 100,
                    Brand = "Metallica",
                    Name = "Shredder 9000",
                    Type = "Electric",
                    Style = "Midnight Black",
                    NumberOfStrings = 6
                },

                new Guitar
                {
                    Id = 200,
                    Brand = "Fender",
                    Name = "Fender Bender 500",
                    Type = "Bass",
                    Style = "Money Green",
                    NumberOfStrings = 4
                },

                new Guitar
                {
                    Id = 300,
                    Brand = "Urbanizer",
                    Name = "Cheif Keith",
                    Type = "Acoustic",
                    Style = "Country Road Brown",
                    NumberOfStrings = 6
                }
            };

            return guitars.Where(g => g.Type == type);
        }
        private static RentalAsset GetAsset()
        {
            return new Guitar
            {
                Id = 300,
                Brand = "Urbanizer",
                Name = "Cheif Keith",
                Type = "Acoustic",
                Style = "Country Road Brown",
                NumberOfStrings = 6,

            };
        }

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

        private static AssetHoldModel GetCurrentHold()
        {
            Subscriber sub = new Subscriber
            {
                Id = 1,
                FirstName = "Brad",
                LastName = "Smith",
            };

            return new AssetHoldModel
            {
                Subscriber = sub.FirstName + " " + sub.LastName,
                HoldPlace = DateTime.Now.ToString()
            };
        }
    }
}
