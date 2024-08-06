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

var userCount = 0;
var userListingCount = 0;

var generateUserNumber = 1000;
var userList = dataGenarator.GenerateUsers(generateUserNumber);
var userIdList = new List<int>();
foreach (var item in userList)
{
    userManager.Add(item);
    userIdList.Add(item.Id);
    userCount++;
    Console.WriteLine("User Ekleniyor : "+userCount+"/"+generateUserNumber);
}

foreach(var userId in userIdList)
{
    var landListings = dataGenarator.GenerateLandListings(1, userId);
    foreach(var item in landListings)
    {
        landListingManager.Add(item);
    }
    var houseListings = dataGenarator.GenerateHouseListings(1, userId);
    foreach (var item in houseListings)
    {
        houseListingManager.Add(item);
    }
    userListingCount++;
    Console.WriteLine("Userların İlanları Ekleniyor: "+userListingCount+"/"+generateUserNumber);

}
Console.Clear();
Console.WriteLine("İşlem Başarıyla Tamamlandı");
