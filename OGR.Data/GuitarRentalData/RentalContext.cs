using Microsoft.EntityFrameworkCore;
using RentalData.Models;

namespace RentalData
{
    public class RentalContext : DbContext
    {
        public RentalContext()
        {

        }

        public RentalContext(DbContextOptions options) : base(options) 
        { 

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Subscriber> Subscribers { get; set; }

        public virtual DbSet<DistributionCenter> DistributionCenters { get; set; }

        public virtual DbSet<ShippingRegion> ShippingRegions { get; set; }

        public virtual DbSet<Courier> Couriers { get; set; }

        public virtual DbSet<Inventory> Inventory { get; set; }

        public virtual DbSet<RentalAsset> RentalAssets { get; set; }

        public virtual DbSet<Guitar> Guitars { get; set; }

        public virtual DbSet<Rental> Rentals { get; set; }

        public virtual DbSet<RentalHistory> RentalHistories { get; set; }

        public virtual DbSet<Hold> Holds { get; set; }

    }
}
