using RentalData.Models;
using System;
using System.Collections.Generic;

namespace OnlineGuitarRentals.Models.Subscriber
{
    public class SubscriberDetailModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get => FirstName + " " + LastName;
            
        }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RenewalDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Active { get; set; }

        public IEnumerable<Rental> AssetsRented { get; set; }

        public IEnumerable<RentalHistory> RentalHistory { get; set; }

        public IEnumerable<Hold> Holds { get; set; }


    }
}
