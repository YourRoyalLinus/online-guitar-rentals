using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalData.Models
{
    public class DistributionCenter
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Telephone { get; set; }
        
        [Required]
        public ShippingRegion ShippingRegion { get; set; }

        public string ImageUrl { get; set; }

        public virtual IEnumerable<Inventory> Inventory { get; set; }

    }
}
