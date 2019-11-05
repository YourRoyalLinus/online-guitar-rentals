using System;
using System.ComponentModel.DataAnnotations;

namespace RentalData.Models
{
    public class RentalHistory
    {
        public int Id { get; set; }

        [Required]
        public RentalAsset RentalAsset { get; set; }

        [Required]
        public Subscriber Subscriber { get; set; }

        [Required]
        public DistributionCenter DistributionCenter { get; set; }

        [Required]
        public DateTime RentedOut { get; set; }

        public DateTime? Returned { get; set; }

    }
}
