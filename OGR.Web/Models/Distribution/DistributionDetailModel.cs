using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineGuitarRentals.Models.Distribution
{
    public class DistributionDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Region { get; set; }
        public string StatesServed { get; set; }
        public string Couriers { get; set; }
        public bool IsDelivering { get; set; }
        public int NumberOfSubscribers { get; set; }
        public double TotalAssetValue { get; set; }
        public int TotalStock { get; set; }
        public string ImageUrl { get; set; }

        public IEnumerable<string> HoursOpen { get; set; }

    }
}
