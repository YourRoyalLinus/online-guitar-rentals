using System.Collections.Generic;

namespace OnlineGuitarRentals.Models.Inventory
{
    public class InventoryIndexModel
    {
        public IEnumerable<InventoryDetailModel> Inventory { get; set; }
    }
}
