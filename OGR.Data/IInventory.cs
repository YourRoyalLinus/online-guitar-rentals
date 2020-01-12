using RentalData.Models;
using System.Collections.Generic;

namespace RentalData
{
    public interface IInventory
    {
        IEnumerable<Inventory> GetAll();

        IEnumerable<Inventory> GetAllType(string type);

        IEnumerable<Inventory> GetByPrice(double price);

        IEnumerable<Inventory> GetByPrice(double start, double end);

        IEnumerable<Inventory> GetByStock(int stock);

        IEnumerable<Inventory> GetByStock(int start, int end);

        Inventory GetByAssetId(int id);

        string GetAssetName(int id);

        string GetAssetBrand(int id);

        string GetAssetStyle(int id);

        string GetAssetType(int id);

        void Add(Inventory newInventory);



        




    }
}
