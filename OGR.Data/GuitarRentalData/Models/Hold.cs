using System;

namespace RentalData.Models
{
    public class Hold
    {
        public int Id { get; set; }

        public RentalAsset RentalAsset { get; set; }

        public Subscriber Subscriber { get; set; }

        public DistributionCenter DistributionCenter { get; set; }

        public DateTime HoldPlaced { get; set; }
    }
}
