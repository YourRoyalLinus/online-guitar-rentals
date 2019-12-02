using Microsoft.EntityFrameworkCore;
using RentalData;
using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentalServices
{
    public class RentalAssetService : IRentalAsset
    {
        private RentalContext _context;

        public RentalAssetService(RentalContext context)
        {
            _context = context;
        }

        public void Add(RentalAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<RentalAsset> GetAll()
        {
            return _context.RentalAssets;
        }

        public RentalAsset GetById(int id)
        {
            return GetAll().FirstOrDefault(asset => asset.Id == id);
        }

        public string GetBrand(int id)
        {
            return GetById(id).Brand;
        }

        public bool GetAvailable(int id)
        {
            return GetById(id).Available == 1 ? true : false;
        }

        public string GetDescription(int id)
        {
            return GetById(id).Description;
        }

        public string GetName(int id)
        {
            return GetById(id).Name;
        }

        public float GetRating(int id)
        {
            return GetById(id).Rating;
        }

        public double GetPrice(int id)
        {
            var inventory = _context.Inventory
                .Include(i => i.RentalAsset)
                .FirstOrDefault(i => i.RentalAsset.Id == id);

            return inventory.Price;
        }

        public int GetStock(int id) //All stock vs Local user stock
        {
            var invAsset = _context.Inventory
                            .Include(i => i.RentalAsset)
                            .GroupBy(i => i.RentalAsset.Id)
                            .Select(inv => new
                            {
                                Id = inv.Key,
                                Price = inv.Sum(i => i.Price),
                                Stock = inv.Sum(i => i.Stock)
                            })
                            .FirstOrDefault(i => i.Id == id);

            return invAsset.Stock;
        }
    }
}
