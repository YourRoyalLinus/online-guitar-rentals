using Microsoft.EntityFrameworkCore;
using RentalData;
using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalServices
{
    public class SubscriberService : ISubscriber
    {
        private RentalContext _context;

        public SubscriberService(RentalContext context)
        {
            _context = context;
        }
        public void Add(Subscriber newSubscriber)
        {
            _context.Add(newSubscriber);
            _context.SaveChanges();
        }

        public IEnumerable<Subscriber> GetAll()
        {
            return _context.Subscribers
                .Include(s => s.ShippingRegion);
        }

        public Subscriber Get(int id)
        {
            return _context.Subscribers
                .Include(s=> s.ShippingRegion)
                .FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Hold> GetHolds(int subscriberId)
        {
            return _context.Holds
                .Include(h => h.RentalAsset)
                .Include(h => h.DistributionCenter)
                .Where(s => s.Id == subscriberId)
                .OrderByDescending(h => h.HoldPlaced);
        }

        public IEnumerable<RentalHistory> GetRentalHistories(int subscriberId)
        {
            return _context.RentalHistories
                .Include(rh => rh.DistributionCenter)
                .Include(rh => rh.RentalAsset)
                .Include(rh => rh.Subscriber)
                .Where(s => s.Id == subscriberId)
                .OrderByDescending(rh => rh.RentedOut);
        }

        public IEnumerable<Rental> GetRentals(int subscriberId)
        {
            return _context.Rentals
                .Include(r => r.Subscriber)
                .Where(s => s.Id == subscriberId);
        }
    }
}
