using RentalData.Models;
using System.Collections.Generic;

namespace RentalData
{
    public interface ISubscriber
    {
        Subscriber Get(int id);
        IEnumerable<Subscriber> GetAll();
        void Add(Subscriber newSubscriber);
        IEnumerable<RentalHistory> GetRentalHistories(int subscriberId);
        IEnumerable<Hold> GetHolds(int subscriberId);
        IEnumerable<Rental> GetRentals(int subscriberId);
    }
}
