using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Constants;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfLandListingDal : EfRepositoryBase<ReaContext, LandListing>, ILandListingDal
    {
        public LandListingDetailDto GetLandListingDetail(int listingId)
        {
            using (ReaContext context = new ReaContext())
            {

                var query = from landListing in context.LandListings
                            join listing in context.Listings on landListing.ListingId equals listing.ListingId
                            join city in context.Cities on listing.CityId equals city.Id
                            join district in context.Districts on listing.DistrictId equals district.Id
                            join listingType in context.ListingTypes on listing.ListingTypeId equals listingType.Id
                            join user in context.Users on listing.UserId equals user.Id

                            where listing.ListingId == listingId
                            && listing.Status == true
                            select new LandListingDetailDto
                            {
                                //LandListing
                                Id = landListing.Id,
                                IslandNo = landListing.IslandNo,
                                ParcelNo = landListing.ParcelNo,
                                SheetNo = landListing.SheetNo,
                                FloorEquivalent = landListing.FloorEquivalent,

                                Address = listing.Address,
                                //City
                                CityName = city.CityName,
                                //District
                                DistrictName = district.DistrictName,
                                //Listing
                                ListingId = listing.ListingId,
                                Title = listing.Title,
                                Description = listing.Description,
                                Date = listing.Date,
                                Price = listing.Price,
                                SquareMeter = listing.SquareMeter,
                                //User
                                UserId = user.Id,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                UserEmail = user.Email,
                                //ListingType
                                ListingTypeName = listingType.ListingTypeName,
                                

                            };

                return query.First();
            }
        }

        public List<LandListingDto> GetLandListings()
        {
            using (ReaContext context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;

                var query = context.LandListings
                    .Join(context.Listings,
                          landListing => landListing.ListingId,
                          listing => listing.ListingId,
                          (landListing, listing) => new { landListing, listing })
                    .Join(context.Cities,
                          houseListingListing => houseListingListing.listing.CityId,
                          city => city.Id,
                          (houseListingListing, city) => new { houseListingListing.landListing, houseListingListing.listing, city })
                    .Join(context.Districts,
                          houseListingCity => houseListingCity.listing.DistrictId,
                          district => district.Id,
                          (houseListingCity, district) => new { houseListingCity.landListing, houseListingCity.listing, houseListingCity.city, district })
                    .Join(context.ListingTypes,
                          houseListingType => houseListingType.listing.ListingTypeId,
                          listingType => listingType.Id,
                          (houseListingType, listingType) => new { houseListingType.landListing, houseListingType.listing, houseListingType.city, houseListingType.district, listingType })
                    .GroupJoin(context.ListingImages,
                               landListingTypeInfo => landListingTypeInfo.listing.ListingId,
                               image => image.ListingId,
                               (landListingTypeInfo, images) => new { landListingTypeInfo, images })
                    .SelectMany(landListingWithImages => landListingWithImages.images.DefaultIfEmpty(),
                        (landListingWithImages, image) => new LandListingDto
                        {
                            Id = landListingWithImages.landListingTypeInfo.landListing.Id,
                            ListingId = landListingWithImages.landListingTypeInfo.listing.ListingId,
                            SquareMeter = landListingWithImages.landListingTypeInfo.listing.SquareMeter,
                            CityName = landListingWithImages.landListingTypeInfo.city.CityName,
                            DistrictName = landListingWithImages.landListingTypeInfo.district.DistrictName,
                            Title = landListingWithImages.landListingTypeInfo.listing.Title,
                            Description = landListingWithImages.landListingTypeInfo.listing.Description,
                            Price = landListingWithImages.landListingTypeInfo.listing.Price,
                            ListingTypeName = landListingWithImages.landListingTypeInfo.listingType.ListingTypeName,
                            ImagePath = image != null ? image.ImagePath : defaultImagePath,
                            Date = landListingWithImages.landListingTypeInfo.listing.Date,
                            ParcelNo = landListingWithImages.landListingTypeInfo.landListing.ParcelNo,
                            IslandNo = landListingWithImages.landListingTypeInfo.landListing.IslandNo,
                            SheetNo = landListingWithImages.landListingTypeInfo.landListing.SheetNo,
                            FloorEquivalent = landListingWithImages.landListingTypeInfo.landListing.FloorEquivalent,
                            Address = landListingWithImages.landListingTypeInfo.listing.Address,
                            Status = landListingWithImages.landListingTypeInfo.listing.Status
                        });

                query = query.Where(dto=>dto.Status==true);

                var result = query
               .AsEnumerable()
                .GroupBy(dto => dto.ListingId)
                .Select(group => new LandListingDto
                {
                    Id = group.First().Id,
                    ListingId = group.First().ListingId,
                    Title = group.First().Title,
                    Description = group.First().Description,
                    CityName = group.First().CityName,
                    DistrictName = group.First().DistrictName,
                    ListingTypeName = group.First().ListingTypeName,
                    Price = group.First().Price,
                    Date = group.First().Date,
                    SquareMeter = group.First().SquareMeter,
                    FloorEquivalent = group.First().FloorEquivalent,
                    IslandNo = group.First().IslandNo,
                    ParcelNo = group.First().ParcelNo,
                    SheetNo = group.First().SheetNo,
                    Address = group.First().Address,


                    // ImagePath'i bulmak için grup içerisindeki ilk geçerli (null olmayan) değeri seçin
                    ImagePath = group.Select(dto => dto.ImagePath).FirstOrDefault(image => image != null) ?? PathConstants.DefaultListingImagePath
                })
                .OrderByDescending(dto => dto.Date)
                 .ToList();


                return result;
            }


        }
    }
}
