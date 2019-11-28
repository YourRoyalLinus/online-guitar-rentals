using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineGuitarRentals.Models.Product
{
    public class GuitarIndexListingModel : AssetIndexListingModel
    {
        public string Type { get; set; }

        public string Style { get; set; }

        public int NumberOfStrings { get; set; }
    }
}
