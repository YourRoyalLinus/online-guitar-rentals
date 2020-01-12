using Microsoft.EntityFrameworkCore;
using RentalData;
using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalServices
{
    public class DistributionService : IDistribution
    {
        private RentalContext _context;

        public DistributionService(RentalContext context)
        {
            _context = context;
        }

        public void Add(DistributionCenter newCenter)
        {
            _context.Add(newCenter);
            _context.SaveChanges();
        }

        public DistributionCenter Get(int centerId)
        {
            return _context.DistributionCenters
                .Include(c => c.ShippingRegion)
                .FirstOrDefault(c => c.Id == centerId);
        }

        public IEnumerable<DistributionCenter> GetAll()
        {
            return _context.DistributionCenters
                .Include(c => c.ShippingRegion)
                .Include(c => c.Inventory);
        }

        public double GetAssetPrice(int centerId, int assetId)
        {
            return _context.Inventory
                .Include(r => r.RentalAsset)
                .Include(c => c.DistributionCenter)
                .FirstOrDefault(i => i.RentalAsset.Id == assetId && i.DistributionCenter.Id == centerId)
                .Price;
        }

        public IEnumerable<Inventory> GetAssets(int centerId, int assetId)
        {
            return _context.Inventory
                .Include(a => a.RentalAsset)
                .Include(c => c.DistributionCenter)
                .Where(i => i.DistributionCenter.Id == centerId && i.RentalAsset.Id == assetId);
                
        }

        public int GetAssetStock(int centerId, int assetId)
        {
            return _context.Inventory
                .Include(r => r.RentalAsset)
                .Include(c => c.DistributionCenter)
                .FirstOrDefault(i => i.RentalAsset.Id == assetId && i.DistributionCenter.Id == centerId)
                .Stock;
        }

        public string GetCourierNames(int centerId)
        {
            List<string> names = new List<string>();

            var couriers = GetCouriers(centerId);

            foreach(var courier in couriers)
            {
                names.Add(courier.Name);
            }

            return String.Join(", ", names.Distinct());
        }

        public IEnumerable<Courier> GetCouriers(int centerId)
        {
            return _context.Couriers
                .Include(c => c.DistributionCenter)
                .Where(c => c.DistributionCenter.Id == centerId);
        }

        public IEnumerable<string> GetDeliveryHours(int centerId)
        {
            var hours = new List<string>();
            var times = _context.Couriers.Where(h => h.DistributionCenter.Id == centerId);

            foreach(var time in times)
            {
                var day = Enum.GetName(typeof(DayOfWeek), time.DayOfWeek);
                var open = time.DeliveryStartTime.ToString("HH:mm");
                var close = time.DeliveryEndTime.ToString("HH:mm");

                hours.Add($"{day} {open} to  {close}");
            }

            return hours;
        }

        public string GetDeliveryRegion(int centerId)
        {
            return _context.DistributionCenters
               .Include(c => c.ShippingRegion)
               .FirstOrDefault(c => c.Id == centerId)
               .ShippingRegion.Region;
        }

        public string GetDeliveryStates(int centerId)
        {
            return _context.DistributionCenters
                .Include(c => c.ShippingRegion)
                .FirstOrDefault(c => c.Id == centerId)
                .ShippingRegion.States;    
        }


        public IEnumerable<Subscriber> GetSubscribers(int centerId)
        {
            var center = _context.DistributionCenters
                .Include(c => c.ShippingRegion)
                .FirstOrDefault(c => c.Id == centerId);

            return _context.Subscribers
                .Include(s => s.ShippingRegion)
                .Where(s => s.ShippingRegion.Id == center.ShippingRegion.Id);
        }

        public double GetTotalAssetValue(int centerId)
        {
           var totalAssetsPrice = _context.Inventory
                .Include(i => i.DistributionCenter)
                .Where(i => i.DistributionCenter.Id == centerId)
                .Sum(i => i.Price);

            var totalAssetStock = _context.Inventory
                .Include(i => i.DistributionCenter)
                .Where(i => i.DistributionCenter.Id == centerId)
                .Sum(i => i.Stock);

            return totalAssetsPrice * totalAssetStock;
        }

        public int GetTotalStock(int centerId)
        {
            return _context.Inventory
                .Include(i => i.DistributionCenter)
                .Where(i => i.DistributionCenter.Id == centerId)
                .Sum(i => i.Stock);
        }

        public bool IsDelivering(int centerId)
        {
            var currentTime = DateTime.Now.Hour;
            var currentDay = (int) DateTime.Now.DayOfWeek;
            var times = _context.Couriers.Where(h => h.DistributionCenter.Id == centerId);
            var daysHours = times.FirstOrDefault(h => h.DayOfWeek == currentDay);
            if (daysHours == null)
                return false;
            else
                return currentTime >= daysHours.DeliveryStartTime.Hour && currentTime < daysHours.DeliveryEndTime.Hour;
        }
    }
}
