using Microsoft.AspNetCore.Mvc;
using OnlineGuitarRentals.Models.Subscriber;
using RentalData;
using RentalData.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineGuitarRentals.Controllers
{
    public class SubscriberController : Controller
    {
        private ISubscriber _subscriber;
        public SubscriberController(ISubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        public IActionResult Index()
        {
            var allSubs = _subscriber.GetAll();

            var subscriberModels = allSubs.Select(s => new SubscriberDetailModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Address = s.Address,
                Email = s.Email,
                PhoneNumber = s.Telephone,
                RenewalDate = s.RenewalDate,
                ExpirationDate = s.ExperationDate,
                Active = s.Active == 1 ? "Active" : "Inactive"
            }).ToList();

            var model = new SubscriberIndexModel()
            {
                Subscriber = subscriberModels
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var subscriber = _subscriber.Get(id);

            var model = new SubscriberDetailModel
            {
                FirstName = subscriber.FirstName,
                LastName = subscriber.LastName,
                Address = subscriber.Address,
                Email = subscriber.Email,
                PhoneNumber = subscriber.Telephone,
                RenewalDate = subscriber.RenewalDate,
                ExpirationDate = subscriber.ExperationDate,
                Active = subscriber.Active == 1 ? "Active" : "Inactive",
                AssetsRented = _subscriber.GetRentals(id) ?? new List<Rental>(),
                RentalHistory = _subscriber.GetRentalHistories(id),
                Holds = _subscriber.GetHolds(id)
            };

            return View(model);
        }
    }
}
