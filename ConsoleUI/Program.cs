// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using ConsoleUI;
using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

UserManager userManager = new UserManager(new EfUserDal());
LandListingManager landListingManager = new LandListingManager(new EfLandListingDal(),new ListingManager(new EfListingDal()));
HouseListingManager houseListingManager = new HouseListingManager(new EfHouseListingDal(),new ListingManager(new EfListingDal()));
List<User> users = new List<User>();
DataGenerator dataGenarator = new DataGenerator();



var userList = dataGenarator.GenerateUsers(100);
var userIdList = new List<int>();
foreach (var item in userList)
{
    userManager.Add(item);
    userIdList.Add(item.Id);
}

foreach(var userId in userIdList)
{
    var landListings = dataGenarator.GenerateLandListings(5, userId);
    foreach(var item in landListings)
    {
        landListingManager.Add(item);
    }
    var houseListings = dataGenarator.GenerateHouseListings(5, userId);
    foreach (var item in houseListings)
    {
        houseListingManager.Add(item);
    }

}
