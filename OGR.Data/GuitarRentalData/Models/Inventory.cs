using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalData.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        public RentalAsset RentalAsset { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }

        public virtual DistributionCenter DistributionCenter { get; set; }
    }
}
