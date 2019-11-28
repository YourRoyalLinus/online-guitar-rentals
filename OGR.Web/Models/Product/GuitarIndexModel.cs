using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineGuitarRentals.Models.Product
{
    public class GuitarIndexModel
    {
        public IEnumerable<GuitarIndexListingModel> Guitars { get; set; }
    }
}
