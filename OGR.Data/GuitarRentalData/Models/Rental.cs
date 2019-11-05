using System;
using System.ComponentModel.DataAnnotations;

namespace RentalData.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public RentalAsset RentalAsset { get; set; }

        [Required]
        public Subscriber Subscriber { get; set; }

        [Required]
        public DistributionCenter DistributionCenter { get; set; }

        [Required]
        public DateTime Since { get; set; }

        [Required]
        public DateTime Until { get; set; }
    }
}
