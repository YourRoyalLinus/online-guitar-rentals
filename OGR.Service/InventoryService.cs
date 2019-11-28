using Microsoft.EntityFrameworkCore;
using RentalData;
using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentalServices
{
    public class InventoryService : IInventory
    {
        private RentalContext _context;

        public InventoryService(RentalContext context)
        {
            _context = context;
        }

        public void Add(Inventory newInventory)
        {
            _context.Add(newInventory);
            _context.SaveChanges();
        }

        public IEnumerable<Inventory> GetAll()
        {
            return _context.Inventory
                .Include(i => i.RentalAsset)
                .Include(i => i.DistributionCenter)
                .GroupBy(i => i.RentalAsset.Id)
                .Select(inv => new Inventory
                {
                    Id = inv.Key,
                    Price = inv.Sum(i => i.Price),
                    Stock = inv.Sum(i => i.Stock)
                });

        }

        public IEnumerable<Inventory> GetAllType(string type)
        {
            var guitars = _context.Guitars
                 .Where(g => g.Type == type);

            return _context.Inventory
                .Include(i => i.RentalAsset)
                .Where(i => guitars.Contains(i.RentalAsset)); 

            
        }

        public string GetAssetBrand(int id)
        {
            return _context.Guitars
           .FirstOrDefault(g => g.Id == id)
           .Brand;
        }

        public string GetAssetName(int id)
        {
            return _context.RentalAssets
                .FirstOrDefault(a => a.Id == id)
                .Name;
        }

        public string GetAssetStyle(int id)
        {
            return _context.Guitars
           .FirstOrDefault(g => g.Id == id)
           .Style;
        }

        public string GetAssetType(int id)
        {
            return _context.Guitars
           .FirstOrDefault(g => g.Id == id)
           .Type;
        }

        public Inventory GetByAssetId(int id)
        {
            var invAsset=   _context.Inventory
                            .Include(i=> i.RentalAsset)
                            .GroupBy(i => i.RentalAsset.Id)
                            .Select(inv => new
                            {
                                Id= inv.Key,
                                Price = inv.Sum(i => i.Price),
                                Stock = inv.Sum(i => i.Stock)
                            })
                            .FirstOrDefault(i=> i.Id == id);

            return _context.Inventory
                .Include(i => i.RentalAsset)
                .FirstOrDefault(i => i.RentalAsset.Id == invAsset.Id);

        }

        public IEnumerable<Inventory> GetByPrice(int price, int start, int end)
        {
            return _context.Inventory
                .Include(i => i.RentalAsset)
                .Include(i => i.DistributionCenter)
                .Where(i => i.Price >= start && i.Price <= end);
        }

        public IEnumerable<Inventory> GetByStock(int stock, int start, int end)
        {
            return _context.Inventory
                .Include(i => i.RentalAsset)
                .Include(i => i.DistributionCenter)
                .Where(i => i.Stock >= start && i.Stock <= end);
        }
    }
}
