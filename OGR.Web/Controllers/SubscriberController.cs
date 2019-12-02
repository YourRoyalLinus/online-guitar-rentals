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

        public IActionResult Index(string sortOrder)
        {
            ViewData["ActiveSortParam"] = string.IsNullOrEmpty(sortOrder) ? "active_desc" : "";
            ViewData["LastNameSortParam"] = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            ViewData["FirstNameSortParam"] = sortOrder == "FirstName" ? "firstname_desc" : "FirstName";
            ViewData["RenewalSortParam"] = sortOrder == "Renewal" ? "renewal_desc" : "Renewal";
            ViewData["ExpirationSortParam"] = sortOrder == "Expiration" ? "expiration_desc" : "Expiration";

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
            });


            switch (sortOrder)
            {
                case "active_desc":
                    subscriberModels = subscriberModels.OrderByDescending(s => s.Active);
                    break;
                case "LastName":
                    subscriberModels = subscriberModels.OrderBy(s => s.LastName);
                    break;
                case "lastname_desc":
                    subscriberModels = subscriberModels.OrderByDescending(s => s.LastName);
                    break;
                case "FirstName":
                    subscriberModels = subscriberModels.OrderBy(s => s.FirstName);
                    break;
                case "firstname_desc":
                    subscriberModels = subscriberModels.OrderByDescending(s => s.FirstName);
                    break;
                case "Renewal":
                    subscriberModels = subscriberModels.OrderBy(s => s.RenewalDate);
                    break;
                case "renewal_desc":
                    subscriberModels = subscriberModels.OrderByDescending(s => s.RenewalDate);
                    break;
                case "Expiration":
                    subscriberModels = subscriberModels.OrderBy(s => s.ExpirationDate);
                    break;
                case "expiration_desc":
                    subscriberModels = subscriberModels.OrderByDescending(s => s.ExpirationDate);
                    break;
                default:
                    subscriberModels = subscriberModels.OrderBy(s => s.Active);
                    break;
            }
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
