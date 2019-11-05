using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineGuitarRentals.Models.Rentals
{
    public class RentalModel
    {
        public int SubscriberId { get; set; }
        public string Name { get; set; }

        public int AssetId { get; set;}
        
        public string ImageUrl { get; set; }

        public int HoldCount { get; set; }

        public bool OutOfStock { get; set; }
    }
}
