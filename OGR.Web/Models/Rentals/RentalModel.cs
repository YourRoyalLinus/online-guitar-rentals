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

        public string AltImageUrl1 { get; set; }

        public string AltImageUrl2 { get; set; }

        public string AltImageUrl3 { get; set; }

        public string AltImageUrl4 { get; set; }

        public string AltImageUrl5 { get; set; }

        public int HoldCount { get; set; }

        public bool OutOfStock { get; set; }
    }
}
