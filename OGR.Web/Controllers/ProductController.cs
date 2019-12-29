using Microsoft.AspNetCore.Mvc;
using OnlineGuitarRentals.Models.Product;
using OnlineGuitarRentals.Models.Rentals;
using RentalData;
using System;
using System.Linq;

namespace OnlineGuitarRentals.Controllers
{
    public class ProductController : Controller
    {
        private IRentalAsset _assets;
        private IRental _rentals;
        private IGuitar _guitars;

        public ProductController(IRentalAsset assets, IRental rentals, IGuitar guitars)
        {
            _assets = assets;
            _rentals = rentals;
            _guitars = guitars;
        }

        [Route("/Product/Index")]
        public IActionResult Index(string search)
        {
            ViewData["CurrentFilter"] = "";

            var assetModels = _assets.GetAll();

            var listingResult = assetModels
                .Select(result => new AssetIndexListingModel
                {
                    Id = result.Id,
                    Brand = _assets.GetBrand(result.Id),
                    Name = _assets.GetName(result.Id),
                    Description = _assets.GetDescription(result.Id),
                    Rating = _assets.GetRating(result.Id),
                    Available = _assets.GetAvailable(result.Id),
                    ImageUrl = result.ImageUrl
                });

            if(!string.IsNullOrEmpty(search))
            {
                listingResult = listingResult.Where(r => r.Name.Contains(search) || r.Brand.Contains(search));
            }

            var model = new AssetIndexModel()
            {
                Assets = listingResult
            };

            return View(model);
        }

        [Route("/Product/{type}-Index")]
        public IActionResult TypeIndex(string type, string search)
        {
            ViewData["CurrentFilter"] = "";

            var guitarAssetModels = _guitars.GetAllGuitars();

            var listingResult = guitarAssetModels
                .Where(g => g.Type == type)
                .Select(result => new GuitarIndexListingModel
                {
                    Id = result.Id,
                    Type = _guitars.GetType(result.Id),
                    Brand = _assets.GetBrand(result.Id),
                    Name = _assets.GetName(result.Id),
                    Style = _guitars.GetStyle(result.Id),
                    NumberOfStrings = _guitars.GetNumberStrings(result.Id),
                    Description = _assets.GetDescription(result.Id),
                    Rating = _assets.GetRating(result.Id),
                    Available = _assets.GetAvailable(result.Id),
                    ImageUrl = result.ImageUrl
                });

            if (!string.IsNullOrEmpty(search))
            {
                listingResult = listingResult.Where(r => r.Name.Contains(search) || r.Brand.Contains(search) || r.Style.Contains(search) || r.NumberOfStrings.ToString().Contains(search));
            }

            var model = new GuitarIndexModel()
            {
                Guitars = listingResult
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var asset = _assets.GetById(id);

            var currentHolds = _rentals.GetCurrentHolds(id)
                .Select(r => new AssetHoldModel
                {
                    HoldPlace = _rentals.GetCurrentHoldPlaced(id).ToString("d"),
                    Subscriber = _rentals.GetCurrentHoldSubscriberName(id)
                });

            var model = new AssetDetailModel
            {
                AssetId = id,
                Brand = asset.Brand,
                Name = asset.Name,
                Description = asset.Description,
                Rating = asset.Rating,
                Price = _assets.GetPrice(id),
                Stock = _assets.GetStock(id),
                Available = _assets.GetAvailable(id),
                ImageUrl = asset.ImageUrl,
                AltImageUrl1 = asset.AltImgUrl1,
                AltImageUrl2 = asset.AltImgUrl2,
                AltImageUrl3 = asset.AltImgUrl3,
                AltImageUrl4 = asset.AltImgUrl4,
                AltImageUrl5 = asset.AltImgUrl5,
                RentalHistory = _rentals.GetRentalHistory(id),
                LatestRental = _rentals.GetRecentRental(id),
                CurrentHolds = currentHolds
            };

            return View(model);
        }

        public IActionResult Hold(int id)
        {
            var asset = _assets.GetById(id);

            var model = new RentalModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                AltImageUrl1 = asset.AltImgUrl1,
                AltImageUrl2 = asset.AltImgUrl2,
                AltImageUrl3 = asset.AltImgUrl3,
                AltImageUrl4 = asset.AltImgUrl4,
                AltImageUrl5 = asset.AltImgUrl5,
                Name = asset.Brand + " " + asset.Name,
                SubscriberId = 0,
                OutOfStock = _assets.GetStock(id) > 0,
                HoldCount = _rentals.GetCurrentHolds(id).Count()
            };

            return View(model);
        }
        public IActionResult Rent(int id)
        {
            var asset = _assets.GetById(id);

            var model = new RentalModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                AltImageUrl1 = asset.AltImgUrl1,
                AltImageUrl2 = asset.AltImgUrl2,
                AltImageUrl3 = asset.AltImgUrl3,
                AltImageUrl4 = asset.AltImgUrl4,
                AltImageUrl5 = asset.AltImgUrl5,
                Name = asset.Brand + " " + asset.Name,
                SubscriberId = 0,
                OutOfStock = _assets.GetStock(id) > 0
            };

            return View(model);
        }

        public IActionResult Return(int id)
        {
            var asset = _assets.GetById(id);

            var model = new RentalModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                AltImageUrl1 = asset.AltImgUrl1,
                AltImageUrl2 = asset.AltImgUrl2,
                AltImageUrl3 = asset.AltImgUrl3,
                AltImageUrl4 = asset.AltImgUrl4,
                AltImageUrl5 = asset.AltImgUrl5,
                Name = asset.Brand + " " + asset.Name,
                SubscriberId = 0,
                OutOfStock = _assets.GetStock(id) > 0
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ReturnProduct(int assetId, int subscriberId)
        {
            try
            {
               _rentals.ReturnProduct(assetId, subscriberId);
            }
            catch(InvalidOperationException)
            {
                return StatusCode(401);
            }
            return RedirectToAction("Detail", new { id = assetId });

        }

        [HttpPost]
        public IActionResult RentOutProduct(int assetId, int subscriberId)
        {
            try
            {
                _rentals.RentProduct(assetId, subscriberId);
            }
            catch(InvalidOperationException)
            {
                return StatusCode(401);
            }
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int subscriberId)
        {
            try
            {
                _rentals.PlaceHold(assetId, subscriberId);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(401);
            }
            return RedirectToAction("Detail", new { id = assetId });
        }
    }
}
