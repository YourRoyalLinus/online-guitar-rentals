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

        public IActionResult Index(string sortOrder)
        {
            ViewData["IdSortParam"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["BrandSortParam"] = sortOrder == "Brand" ? "brand_desc" : "Brand";
            ViewData["NameSortParam"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["TypeSortParam"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["StyleSortParam"] = sortOrder == "Style" ? "style_desc" : "Style";
            ViewData["StockSortParam"] = sortOrder == "Stock" ? "stock_desc" : "Stock";
            ViewData["ValueSortParam"] = sortOrder == "Value" ? "value_desc" : "Value";

            var inventory = _inventory.GetAll().Select(inv => new InventoryDetailModel
            {
                Id = inv.Id,
                RentalAssetId = inv.Id,
                AssetBrand = _inventory.GetAssetBrand(inv.Id),
                AssetName = _inventory.GetAssetName(inv.Id),
                AssetType = _inventory.GetAssetType(inv.Id), 
                AssetStyle = _inventory.GetAssetStyle(inv.Id),
                TotalStock = inv.Stock,
                TotalAssetValue = inv.Price * inv.Stock,
            });


            switch (sortOrder)
            {
                case "id_desc":
                    inventory = inventory.OrderByDescending(i => i.Id);
                    break;
                case "Brand":
                    inventory = inventory.OrderBy(i => i.AssetBrand);
                    break;
                case "brand_desc":
                    inventory = inventory.OrderByDescending(i => i.AssetBrand);
                    break;
                case "Name":
                    inventory = inventory.OrderBy(i => i.AssetName);
                    break;
                case "name_desc":
                    inventory = inventory.OrderByDescending(i => i.AssetName);
                    break;
                case "Type":
                    inventory = inventory.OrderBy(i => i.AssetType);
                    break;
                case "type_desc":
                    inventory = inventory.OrderByDescending(i => i.AssetType);
                    break;
                case "Style":
                    inventory = inventory.OrderBy(i => i.AssetStyle);
                    break;
                case "style_desc":
                    inventory = inventory.OrderByDescending(i => i.AssetStyle);
                    break;
                case "Stock":
                    inventory = inventory.OrderBy(i => i.TotalStock);
                    break;
                case "stock_desc":
                    inventory = inventory.OrderByDescending(i => i.TotalStock);
                    break;
                case "Value":
                    inventory = inventory.OrderBy(i => i.TotalAssetValue);
                    break;
                case "value_desc":
                    inventory = inventory.OrderByDescending(i => i.TotalAssetValue);
                    break;
                default:
                    inventory = inventory.OrderBy(i => i.Id);
                    break;
            }

            var model = new InventoryIndexModel()
            {
                Inventory = inventory
            };

            return View(model);
        }

    }
}
