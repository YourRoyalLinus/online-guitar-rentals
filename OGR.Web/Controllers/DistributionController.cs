using Microsoft.AspNetCore.Mvc;
using OnlineGuitarRentals.Models.Distribution;
using RentalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineGuitarRentals.Controllers
{
    public class DistributionController : Controller
    {
        private IDistribution _distribution;
        public DistributionController(IDistribution distribution)
        {
            _distribution = distribution;
        }
        public IActionResult Index()
        {
            var centers = _distribution.GetAll().Select(center => new DistributionDetailModel
            {
                Id = center.Id,
                Name = center.Name,
                Address = center.Address,
                Telephone = center.Telephone,
                Region = _distribution.GetDeliveryRegion(center.Id),
                StatesServed = _distribution.GetDeliveryStates(center.Id),
                IsDelivering = _distribution.IsDelivering(center.Id),
                NumberOfSubscribers = _distribution.GetSubscribers(center.Id).Count(),
                TotalAssetValue = _distribution.GetTotalAssetValue(center.Id),
                TotalStock = _distribution.GetTotalStock(center.Id)

            });

            var model = new DistributionIndexModel()
            {
                DistributionCenters = centers
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var center = _distribution.Get(id); //Not getting a

            var model = new DistributionDetailModel
            {
                Id = center.Id,
                Name = center.Name,
                Address = center.Address,
                Telephone = center.Telephone,
                Region = _distribution.GetDeliveryRegion(id),
                StatesServed = _distribution.GetDeliveryStates(id),
                IsDelivering = _distribution.IsDelivering(id),
                NumberOfSubscribers = _distribution.GetSubscribers(id).Count(),
                TotalAssetValue = _distribution.GetTotalAssetValue(id),
                TotalStock = _distribution.GetTotalStock(id),
                Couriers = _distribution.GetCourierNames(id),
                HoursOpen = _distribution.GetDeliveryHours(id)
            };

            return View(model);
        }
    }
}
