using Microsoft.EntityFrameworkCore;
using RentalData;
using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalServices
{
    public class RentalService : IRental
    {
        private RentalContext _context;

        public RentalService(RentalContext context)
        {
            _context = context;
        }

        public void Add(Rental newRental)
        {
            _context.Add(newRental);
            _context.SaveChanges();
        }

        public IEnumerable<Rental> GetAll()
        {
            return _context.Rentals;
        }

        public Rental GetById(int rentalId)
        {
            return GetAll()
                .FirstOrDefault(rental => rental.Id == rentalId);
        }

        public DateTime GetCurrentHoldPlaced(int id)
        {
            return _context.Holds
                .Include(h => h.RentalAsset)
                .Include(h => h.Subscriber)
                .FirstOrDefault(h => h.Id == id)
                .HoldPlaced;
        }

        public IEnumerable<Hold> GetCurrentHolds(int id)
        {
            return _context.Holds
                .Include(h => h.RentalAsset)
                .Where(h => h.RentalAsset.Id == id);
        }

        public string GetCurrentHoldSubscriberName(int id)
        {
            var hold = _context.Holds
                .Include(h => h.RentalAsset)
                .Include(h => h.Subscriber)
                .FirstOrDefault(h => h.Id == id);

            return hold.Subscriber?.FirstName + " " + hold.Subscriber?.LastName;
        }

        public Rental GetRecentRental(int rentalId)
        {
            return _context.Rentals
                .Where(r => r.RentalAsset.Id == rentalId)
                .OrderByDescending(c => c.Since)
                .FirstOrDefault();
        }

        public IEnumerable<RentalHistory> GetRentalHistory(int id)
        {
            return _context.RentalHistories
                .Include(h=> h.RentalAsset)
                .Include(h=> h.Subscriber)
                .Where(h => h.RentalAsset.Id == id);
        }

        public IEnumerable<string> GetCurrentRentalSubs(int assetid)
        {
            List<string> rentalSubs = new List<string>();

            var rentals = _context.Rentals
                .Include(r => r.RentalAsset)
                .Include(r => r.Subscriber)
                .Where(r => r.RentalAsset.Id == assetid);

            if (rentals == null)
            {
                return null;
            }

            foreach(var rental in rentals)
            {
                rentalSubs.Add(rental.Subscriber?.FirstName + " " + rental.Subscriber?.LastName);   
            }

            return rentalSubs;


        }

        public void PlaceHold(int assetId, int SubscriberId)
        {
            var now = DateTime.Now;

            var asset = _context.RentalAssets
                .FirstOrDefault(a => a.Id == assetId);

            var subscriber = _context.Subscribers
                .FirstOrDefault(s => s.Id == SubscriberId);

            if(asset.Available == 1)
            {
                UpdateAssetAvail(assetId, false);
            }

            var hold = new Hold
            {
                RentalAsset = asset,
                Subscriber = subscriber,
                DistributionCenter = (DistributionCenter)subscriber.ShippingRegion.DistributionCenter,
                HoldPlaced = now
            };

            _context.Add(hold);
            _context.SaveChanges();

        }

        public void RentProduct(int assetId, int subscriberId)
        {
            var product = _context.RentalAssets
                .FirstOrDefault(a => a.Id == assetId); //Refactor into Helper

            if(IsAvailable(assetId))
            {
                return;
                //Handle feedback to user
            }

            UpdateAssetAvail(assetId, false);

            var subscriber = _context.Subscribers
                .Include(r => r.ShippingRegion)
                .FirstOrDefault(s => s.Id == subscriberId); //Helper

            var distributionCenter = _context.DistributionCenters
                .Include(dc => dc.ShippingRegion)
                .FirstOrDefault(c => c.ShippingRegion == subscriber.ShippingRegion); //Helper

            var now = DateTime.Now;

            var rental = new Rental
            {
                RentalAsset = product,
                Subscriber = subscriber, //Needs to handle for non-subs (null)
                DistributionCenter = distributionCenter,  
                Since = now,
                Until = GetDefaultRentalPeriod(now)
            };

            _context.Add(rental);

            var rentalHistory = new RentalHistory
            {
                RentedOut = now,
                RentalAsset = product,
                Subscriber = subscriber,
                DistributionCenter = distributionCenter
            };

            _context.Add(rentalHistory); //Could be helper
            _context.SaveChanges();
        }

        public void ReturnProduct(int assetId)
        {
            var now = DateTime.Now;

            var product = _context.RentalAssets
                .FirstOrDefault(a => a.Id == assetId);

            //remove any existing Rentals on the product
            RemoveExistingRentals(assetId);

            //close any existing checkout history
            CloseExistingRentalHistory(assetId, now);

            //look for existing holds on the item
            //if there are enough holds and enough stock update to the n
            //subscribers with the earliest holds
            var currentHolds = _context.Holds
              .Include(h => h.RentalAsset)
              .Include(h => h.Subscriber)
              .Where(h => h.RentalAsset.Id == assetId); //Refactor into helper

            if (currentHolds.Any())
            {
                RentoutToEarliestHolds(assetId, currentHolds);
                return;
            }

            //otherwise update the item to available
            UpdateAssetAvail(assetId, true);

            _context.SaveChanges();
        }

        // Helper
        // Methods
        // Below

        private void RentalAssetHelper()
        {
            return;
        }
       
        private void RemoveExistingRentals(int assetId)
        {
            var rental = _context.Rentals
                .FirstOrDefault(r => r.RentalAsset.Id == assetId);

            if (rental != null)
            {
                _context.Remove(rental);
            }
        }

        private void CloseExistingRentalHistory(int assetid, DateTime now)
        {
            var history = _context.RentalHistories
                .FirstOrDefault(h => h.RentalAsset.Id == assetid && h.Returned == null);

            if (history != null)
            {
                _context.Update(history);
                history.RentedOut = now;
            }
        }

        private void RentoutToEarliestHolds(int assetId, IQueryable<Hold> currentHolds)
        {
            var inv = _context.Inventory
                .Include(i => i.RentalAsset)
                .FirstOrDefault(i => i.RentalAsset.Id == assetId);

            var earliestHolds = currentHolds.OrderBy(holds => holds.HoldPlaced)
                .Take(inv.Stock);  //WIP

            foreach(var hold in earliestHolds)
            {
                _context.Remove(hold.Subscriber);
                RentProduct(assetId, hold.Subscriber.Id);
            }
                

            _context.SaveChanges();

        }

        private void HoldsHelper()
        {
            return;
        }

        private bool IsAvailable(int assetId)
        {
            return _context.Rentals
                .Where(r => r.RentalAsset.Id == assetId)
                .Any();
        }

        private void UpdateAssetAvail(int assetId, bool b)
        {
            var product = _context.RentalAssets
                .FirstOrDefault(a => a.Id == assetId);

            _context.Update(product);

            product.Available = b == true ? 1: 0;
        }

        private DateTime GetDefaultRentalPeriod(DateTime now)
        {
            return now.AddDays(30);
        }
    }
}

   
