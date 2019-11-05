using RentalData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalData
{
    public interface IDistribution
    {
        IEnumerable<DistributionCenter> GetAll();
        IEnumerable<Subscriber> GetSubscribers(int centerId);
        IEnumerable<Inventory> GetAssets(int centerId, int assetId); 
        IEnumerable<string> GetDeliveryHours(int centerId);
        IEnumerable<Courier> GetCouriers(int centerId);
        DistributionCenter Get(int centerId);
        void Add(DistributionCenter newCenter);

        string GetDeliveryStates(int centerId);

        string GetDeliveryRegion(int centerId);
        string GetCourierNames(int centerId);
        bool IsDelivering(int centerId);
        double GetTotalAssetValue(int centerId);

        int GetTotalStock(int centerId);

        double GetAssetPrice(int centerId, int assetId);

        int GetAssetStock(int centerId, int assetId);






    }
}
