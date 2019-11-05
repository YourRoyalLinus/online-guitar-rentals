using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalData.Models
{
    public class ShippingRegion
    {
        public int Id { get; set; }
        
        [Required]
        public string Abbrv { get; set; }

        public string Region { get; set; }

        public string States { get; set; }

        public virtual IEnumerable<Subscriber> Subscribers { get; set; }
        public virtual IEnumerable<DistributionCenter> DistributionCenter{ get; set; }

    }
}
