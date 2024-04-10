using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class ReaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ReaDb;Trusted_Connection=true");
        }

        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingType> ListingTypes { get; set; }
        public DbSet<HouseListing> HouseListings { get; set; }
        public DbSet<ListingImage> ListingImages { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<HouseType> HouseTypes { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

        //User

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
