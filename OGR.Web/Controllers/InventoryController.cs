using Microsoft.AspNetCore.Mvc;
using OnlineGuitarRentals.Models.Inventory;
using RentalData;
using System.Linq;

namespace OnlineGuitarRentals.Controllers
{
    public class InventoryController : Controller
    {
        private IInventory _inventory;

        public InventoryController(IInventory inventory)
        {
            _inventory = inventory;
        }

        public IActionResult Index()
        {
            var inventory = _inventory.GetAll().Select(inv => new InventoryDetailModel
            {
                Id = inv.Id,
                RentalAssetId = inv.Id,
                AssetBrand = _inventory.GetAssetBrand(inv.Id),
                AssetName = _inventory.GetAssetName(inv.Id),
                AssetType = _inventory.GetAssetType(inv.Id), 
                AssetStyle = _inventory.GetAssetStyle(inv.Id),
                TotalStock = inv.Stock,
                TotalAssetValue = inv.Price,
            });

            var model = new InventoryIndexModel()
            {
                Inventory = inventory
            };

            return View(model);
        }

    }
}
