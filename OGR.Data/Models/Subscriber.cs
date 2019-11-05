using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalData.Models
{
    public class Subscriber : User 
    {
        public ShippingRegion ShippingRegion { get; set; }

        [Required]
        public DateTime RenewalDate { get; set; }

        [Required]
        public DateTime ExperationDate { get; set; }

        [Required]
        public int Active { get; set; }

        public virtual IEnumerable<RentalHistory> RentalHistory { get; set; }
    
    }
}
