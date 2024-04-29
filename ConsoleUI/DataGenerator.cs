using Bogus;
using Business.Dtos.Requests;
using Business.Dtos.Requests.LandListingReq;
using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class DataGenerator
    {
        Faker<User> userModel;
        Faker<CreateLandListingReq> landListingModel;
        Faker<CreateHouseListingReq> houseListingModel;
        public DataGenerator()
        {
            Randomizer.Seed = new Random(546578921);

            userModel = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.PasswordHash, f => Encoding.ASCII.GetBytes(f.Random.Word()))
                .RuleFor(u => u.PasswordSalt, f => Encoding.ASCII.GetBytes(f.Random.Word()))
                .RuleFor(u=>u.Status,f=>f.Random.Bool());

            landListingModel = new Faker<CreateLandListingReq>()
                .RuleFor(l=>l.IslandNo, f=>f.Random.Int(1000,5000))
                .RuleFor(l=>l.SheetNo,f=>f.Random.Int(1000,5000))
                .RuleFor(l=>l.Status,f=>f.Random.Bool())
                .RuleFor(l=>l.Address,f=>f.Address.FullAddress())
                .RuleFor(l=>l.CityId,f=>f.Random.Int(1,81))
                .RuleFor(l=>l.DistrictId,f=>f.Random.Int(1,973))
                .RuleFor(l=>l.SquareMeter,f=>f.Random.Int(70,250))
                .RuleFor(l=>l.Date,f=>f.Date.Between(new DateTime(2021,1,1),DateTime.Now))
                .RuleFor(l=>l.Description,f=>f.Lorem.Sentences())
                .RuleFor(l=>l.FloorEquivalent,f=>f.Random.Bool())
                .RuleFor(l=>l.ListingTypeId,f=>f.Random.Int(1,2))
                .RuleFor(l=>l.ParcelNo,f=>f.Random.Int(1000,5000))
                .RuleFor(l=>l.Price,f=>f.Random.Int(100000,9000000))
                .RuleFor(l=>l.Title,f=>f.Lorem.Sentence());

            houseListingModel = new Faker<CreateHouseListingReq>()
                .RuleFor(l => l.Status, f => f.Random.Bool())
                .RuleFor(l => l.Address, f => f.Address.FullAddress())
                .RuleFor(l => l.CityId, f => f.Random.Int(1, 81))
                .RuleFor(l => l.DistrictId, f => f.Random.Int(1, 973))
                .RuleFor(l => l.SquareMeter, f => f.Random.Int(70, 250))
                .RuleFor(l => l.Date, f => f.Date.Between(new DateTime(2021, 1, 1), DateTime.Now))
                .RuleFor(l => l.Description, f => f.Lorem.Sentences())
                .RuleFor(l => l.ListingTypeId, f => f.Random.Int(1, 2))
                .RuleFor(l => l.Price, f => f.Random.Int(100000, 9000000))
                .RuleFor(l => l.Title, f => f.Lorem.Sentence())
                .RuleFor(l=>l.BathroomCount,f=>f.Random.Int(1,15))
                .RuleFor(l=>l.LivingRoomCount,f=>f.Random.Int(1,15))
                .RuleFor(l=>l.BuildingAge,f=>f.Random.Int(0,150))
                .RuleFor(l=>l.RoomCount,f=>f.Random.Int(1,15))
                .RuleFor(l=>l.CurrentFloor,f=>f.Random.Int(1,90))
                .RuleFor(l=>l.FloorCount,f=>f.Random.Int(1,90))
                .RuleFor(l=>l.HasBalcony,f=>f.Random.Bool())
                .RuleFor(l => l.HasElevator, f => f.Random.Bool())
                .RuleFor(l => l.HasFurniture, f => f.Random.Bool())
                .RuleFor(l => l.IsInGatedCommunity, f => f.Random.Bool())
                .RuleFor(l => l.HasGarden, f => f.Random.Bool())
                .RuleFor(l => l.HasParking, f => f.Random.Bool())
                .RuleFor(l=>l.TypeId,f=>f.Random.Int(1,6))
                ;
        }
        public CreateLandListingReq GenerateLandListing(int userId)
        {
            var item = landListingModel.Generate();
            item.UserId = userId;
            return item;
        }
        public List<CreateLandListingReq> GenerateLandListings(int count,int userId)
        {
            List<CreateLandListingReq> landListings = new List<CreateLandListingReq>();
            for(int i = 0; i < count; i++)
            {
                landListings.Add(GenerateLandListing(userId));
            }
            return landListings;
        }

        public CreateHouseListingReq GenerateHouseListing(int userId)
        {
            var item = houseListingModel.Generate();
            item.UserId = userId;
            return item;
        }
        public List<CreateHouseListingReq> GenerateHouseListings(int count, int userId)
        {
            List<CreateHouseListingReq> houseListings = new List<CreateHouseListingReq>();
            for (int i = 0; i < count; i++)
            {
                houseListings.Add(GenerateHouseListing(userId));
            }
            return houseListings;
        }
        public User GenerateUser()
        {
            return userModel.Generate();    
        }
        public List<User> GenerateUsers(int count)
        {
            List<User> users = new List<User>();
            for(int i = 0; i < count; i++)
            {
                users.Add(GenerateUser());
            }
            return users;
        }
    }
}
