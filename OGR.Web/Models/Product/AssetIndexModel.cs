using System.Collections.Generic;

namespace OnlineGuitarRentals.Models.Product
{
    public class AssetIndexModel
    {
        public IEnumerable<AssetIndexListingModel> Assets { get; set; }
    }
}
