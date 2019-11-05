using RentalData.Models;
using System;
using System.Collections.Generic;

namespace RentalData
{
    public interface IRental
    {
        IEnumerable<Rental> GetAll();
        Rental GetById(int rentalId);

        Rental GetRecentRental(int rentalId);

        void Add(Rental newRental);

        void RentProduct(int assetId, int subscriberId);

        void ReturnProduct(int assetId);

        IEnumerable<RentalHistory> GetRentalHistory(int id);

        IEnumerable<string> GetCurrentRentalSubs(int assetid);

        void PlaceHold(int assetId, int SubscriberId);
        string GetCurrentHoldSubscriberName(int id); //Should be multiple
        DateTime GetCurrentHoldPlaced(int id); 
        IEnumerable<Hold> GetCurrentHolds(int id);


    }
}
