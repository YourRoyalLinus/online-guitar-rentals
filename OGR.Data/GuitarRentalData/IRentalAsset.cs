using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalData
{
    public interface IRentalAsset
    {
        IEnumerable<RentalAsset> GetAll();

        RentalAsset GetById(int id);

        string GetBrand(int id);

        string GetName(int id);

        string GetDescription(int id);

        float GetRating(int id);

        bool GetAvailable(int id);

        void Add(RentalAsset newAsset);

        double GetPrice(int id);

        int GetStock(int id);

    }
}
