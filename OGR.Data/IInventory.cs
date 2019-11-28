using RentalData.Models;
using System.Collections.Generic;

namespace RentalData
{
    public interface IInventory
    {
        IEnumerable<Inventory> GetAll();

        IEnumerable<Inventory> GetAllType(string type);

        IEnumerable<Inventory> GetByPrice(int price, int start, int end);

        IEnumerable<Inventory> GetByStock(int stock, int start, int end);

        Inventory GetByAssetId(int id);

        string GetAssetName(int id);

        string GetAssetBrand(int id);

        string GetAssetStyle(int id);

        string GetAssetType(int id);

        void Add(Inventory newInventory);



        




    }
}
