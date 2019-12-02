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
                .Include(h => h.Subscriber)
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

        public void PlaceHold(int assetId, int subscriberId)
        {
            var now = DateTime.Now;
            var asset = GetRentalAsset(assetId);
            var subscriber = GetSubscriber(subscriberId);
            var center = GetDistributionCenter(subscriber);
            var inventory = GetInventory(assetId, center.Id);

            if(inventory.Stock >= 1)
            {
                UpdateAssetAvail(inventory.Id);
            }

            var hold = new Hold
            {
                RentalAsset = asset,
                Subscriber = subscriber,
                DistributionCenter = GetDistributionCenter(subscriber),
                HoldPlaced = now
            };

            _context.Add(hold);
            _context.SaveChanges();

        }

        public void RentProduct(int assetId, int subscriberId)
        {
            var now = DateTime.Now;
            var product = GetRentalAsset(assetId);
            var subscriber = GetSubscriber(subscriberId);
            var distributionCenter = GetDistributionCenter(subscriber);
            var inventory = GetInventory(assetId, distributionCenter.Id);

            if (subscriber == null)
            {
                return;
                //Handle Feedback to user on Frontend
            }

            if (!IsAvailable(assetId, subscriberId))
            {
                return;
                //Handle Feedback to user on Frontend
            }

            UpdateAssetAvail(inventory.Id);

            var rental = new Rental
            {
                RentalAsset = product,
                Subscriber = subscriber, 
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

            _context.Add(rentalHistory); 
            _context.SaveChanges();
        }

        public void ReturnProduct(int assetId, int subscriberId)
        {
            var now = DateTime.Now;
            var subscriber = GetSubscriber(subscriberId);
            var distributionCenter = GetDistributionCenter(subscriber);
            var inventory = GetInventory(assetId, distributionCenter.Id);
            var product = GetRentalAsset(assetId);

            _context.Update(product);

            //remove any existing Rentals on the product
            RemoveExistingRentals(assetId);

            //close any existing checkout history
            CloseExistingRentalHistory(assetId, now);

            //look for existing holds on the item
            //if there are enough holds and enough stock update to the n
            //subscribers with the earliest holds
            var currentHolds = GetCurrentHolds(assetId);

            if (currentHolds.Any())
            {
                RentoutToEarliestHolds(assetId, currentHolds);
                return;
            }

            //otherwise add the item to the distcenter's stock of the asset
            _context.Update(inventory);
            inventory.Stock += 1;

            _context.SaveChanges();
        }

        // Helper
        // Methods
        // Below

        private RentalAsset GetRentalAsset(int assetId)
        {
             return  _context.RentalAssets
                .FirstOrDefault(a => a.Id == assetId);
        }

        private Subscriber GetSubscriber(int subscriberId)
        {
           return  _context.Subscribers
                .Include(r => r.ShippingRegion)
                .FirstOrDefault(s => s.Id == subscriberId);
        }

        private DistributionCenter GetDistributionCenter(Subscriber subscriber)
        {
            return _context.DistributionCenters
                .Include(dc => dc.ShippingRegion)
                .FirstOrDefault(c => c.ShippingRegion == subscriber.ShippingRegion);
        }
        
        private Inventory GetInventory(int assetId, int centerId)
        {
                return _context.Inventory
                   .Include(i => i.DistributionCenter)
                   .Include(i => i.RentalAsset)
                   .FirstOrDefault(i => i.DistributionCenter.Id == centerId && i.RentalAsset.Id == assetId);
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
                .Include(h => h.DistributionCenter)
                .FirstOrDefault(h => h.RentalAsset.Id == assetid && h.Returned == null);

            var inventory = GetInventory(assetid, history.DistributionCenter.Id);


            if (history != null)
            {
                _context.Update(history);
                _context.Update(inventory);
                history.Returned = now;
                inventory.Stock += 1;
            }
        }

        private void RentoutToEarliestHolds(int assetId, IEnumerable<Hold> currentHolds)
        {
            var inv = _context.Inventory
                            .Include(i => i.RentalAsset)
                            .GroupBy(i => i.RentalAsset.Id)
                            .Select(inv => new
                            {
                                Id = inv.Key,
                                Stock = inv.Sum(i => i.Stock)
                            })
                            .FirstOrDefault(i => i.Id == assetId);

            var earliestHolds = currentHolds.OrderBy(holds => holds.HoldPlaced)
                .Take(inv.Stock);  

            foreach(var hold in earliestHolds)
            {
                var inventory = GetInventory(assetId, hold.DistributionCenter.Id);

                if (inventory.Stock >= 1)
                {
                    _context.Remove(hold.Subscriber);
                    _context.Update(inventory);
                    inventory.Stock -= 1;

                    RentProduct(assetId, hold.Subscriber.Id);
                }
                else
                {
                    continue;
                }
            }

            _context.SaveChanges();

        }

        private bool IsAvailable(int assetId, int subscriberId) 
        {
            var subscriber = GetSubscriber(subscriberId);

            var center = GetDistributionCenter(subscriber);

            var inventory = GetInventory(assetId, center.Id);

            if (inventory.Stock >= 1)
                return true;
            else
                return false;
        }

        private void UpdateAssetAvail(int inventoryId)
        {
            var inventory = _context.Inventory
                .FirstOrDefault(i => i.Id == inventoryId);

            var product = GetRentalAsset(inventory.RentalAsset.Id);

            _context.Update(product);
            _context.Update(inventory);


            product.Available = inventory.Stock - 1 >= 1 ? 1: 0;
            inventory.Stock -= 1;//CHECK IF DOUBLE DEDUCTED
        }

        private DateTime GetDefaultRentalPeriod(DateTime now)
        {
            return now.AddDays(30);
        }
    }
}

   
