using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineGuitarRentals.Models.Product
{
    public class AssetIndexListingModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        public bool Available { get; set; } = false;

        public string ImageUrl { get; set; }
    }
}
