using RentalData.Models;
using System.Collections.Generic;

namespace OnlineGuitarRentals.Models.Product
{
    public class AssetDetailModel
    {
        public int AssetId { get; set; }

        public string Brand { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }

        public bool Available { get; set; }

        public string ImageUrl { get; set; } 

        public string AltImageUrl1 { get; set; }
        public string AltImageUrl2 { get; set; }
        public string AltImageUrl3 { get; set; }
        public string AltImageUrl4 { get; set; }
        public string AltImageUrl5 { get; set; }

        public DistributionCenter ShippingLocation { get; set; }

        public Rental LatestRental { get; set; }

        public IEnumerable<RentalHistory> RentalHistory { get; set; }

        public IEnumerable<AssetHoldModel> CurrentHolds { get; set; }
    }

    public class AssetHoldModel
    {
        public string Subscriber { get; set; }
        public string HoldPlace { get; set; }

    }
}
