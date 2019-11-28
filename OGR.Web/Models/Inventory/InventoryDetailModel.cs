using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineGuitarRentals.Models.Inventory
{
    public class InventoryDetailModel
    {
        public int Id { get; set; }

        public int RentalAssetId { get; set; }

        public string AssetBrand { get; set; } 

        public string AssetName { get; set; }

        public string AssetStyle { get; set; }

        public string AssetType { get; set; }

        public int TotalStock { get; set; }

        public double TotalAssetValue { get; set; }
    }
}
