using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineGuitarRentals.Models.Distribution
{
    public class DistributionIndexModel
    {
        public IEnumerable<DistributionDetailModel> DistributionCenters { get; set; }
    }
}
