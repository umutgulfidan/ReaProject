// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

ListingManager listingManager = new ListingManager(new EfListingDal());
//listingManager.Add(new Listing
//{
//    Date = DateTime.Now,
//    Description = "Console Test",
//    CityId = 34,
//    DistrictId = 1,
//    Price = 10000,
//    SquareMeter = 122,
//    Title = "Doktordan Tertemiz Müstakil",
//    UserId = 1,
//});

//List<Listing> liste = listingManager.GetAll();
//foreach (var item in liste)
//{
//    Console.WriteLine(item.lis);
//    Console.WriteLine(item.Title);
//    Console.WriteLine(item.UserId);
//}

//Listing item = listingManager.GetById(1);
//Console.WriteLine(item.Title);


//Listing item = listingManager.GetById(1);
//item.Title = "Title";
//item.Description = "Description";

//listingManager.Update(new Listing
//{
//    ListingId = 1,
//    Description = "Description Test",
//});

//Console.WriteLine(listingManager.GetById(1).Data.Description);