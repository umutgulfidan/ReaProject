using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Constants;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfHouseListingDal : EfRepositoryBase<ReaContext, HouseListing>, IHouseListingDal
    {
        public List<HouseListingDto> GetHouseListings()
        {
            using (var context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;

                var query = context.HouseListings
                    .Join(context.Listings,
                          houseListing => houseListing.ListingId,
                          listing => listing.ListingId,
                          (houseListing, listing) => new { houseListing, listing })
                    .Join(context.Cities,
                          houseListingListing => houseListingListing.listing.CityId,
                          city => city.Id,
                          (houseListingListing, city) => new { houseListingListing.houseListing, houseListingListing.listing, city })
                    .Join(context.Districts,
                          houseListingCity => houseListingCity.listing.DistrictId,
                          district => district.Id,
                          (houseListingCity, district) => new { houseListingCity.houseListing, houseListingCity.listing, houseListingCity.city, district })
                    .Join(context.HouseTypes,
                          houseListingDistrict => houseListingDistrict.houseListing.TypeId,
                          houseType => houseType.Id,
                          (houseListingDistrict, houseType) => new { houseListingDistrict.houseListing, houseListingDistrict.listing, houseListingDistrict.city, houseListingDistrict.district, houseType })
                    .Join(context.ListingTypes,
                          houseListingType => houseListingType.listing.ListingTypeId,
                          listingType => listingType.Id,
                          (houseListingType, listingType) => new { houseListingType.houseListing, houseListingType.listing, houseListingType.city, houseListingType.district, houseListingType.houseType, listingType })
                    .GroupJoin(context.ListingImages,
                               houseListingTypeInfo => houseListingTypeInfo.listing.ListingId,
                               image => image.ListingId,
                               (houseListingTypeInfo, images) => new { houseListingTypeInfo, images })
                    .SelectMany(houseListingWithImages => houseListingWithImages.images.DefaultIfEmpty(),
                        (houseListingWithImages, image) => new HouseListingDto
                        {
                            Id = houseListingWithImages.houseListingTypeInfo.houseListing.HouseListingId,
                            ListingId = houseListingWithImages.houseListingTypeInfo.listing.ListingId,
                            CityName = houseListingWithImages.houseListingTypeInfo.city.CityName,
                            DistrictName = houseListingWithImages.houseListingTypeInfo.district.DistrictName,
                            Title = houseListingWithImages.houseListingTypeInfo.listing.Title,
                            Description = houseListingWithImages.houseListingTypeInfo.listing.Description,
                            Price = houseListingWithImages.houseListingTypeInfo.listing.Price,
                            BathroomCount = houseListingWithImages.houseListingTypeInfo.houseListing.BathroomCount,
                            LivingRoomCount = houseListingWithImages.houseListingTypeInfo.houseListing.LivingRoomCount,
                            HouseTypeName = houseListingWithImages.houseListingTypeInfo.houseType.Name,
                            ListingTypeName = houseListingWithImages.houseListingTypeInfo.listingType.ListingTypeName,
                            RoomCount = houseListingWithImages.houseListingTypeInfo.houseListing.RoomCount,
                            ImagePath = image != null ? image.ImagePath : defaultImagePath,
                            Date = houseListingWithImages.houseListingTypeInfo.listing.Date
                        });

                // ListingId'ye göre gruplama ve ilk kaydı seçme işlemi
                var result = query
                    .GroupBy(dto => dto.ListingId)
                    .Select(group => new HouseListingDto
                    {
                        Id = group.Key,
                        ListingId = group.First().ListingId,
                        Title = group.First().Title,
                        Description = group.First().Description,
                        CityName = group.First().CityName,
                        DistrictName = group.First().DistrictName,
                        ListingTypeName = group.First().ListingTypeName,
                        Price = group.First().Price,
                        Date = group.First().Date,
                        BathroomCount = group.First().BathroomCount,
                        LivingRoomCount = group.First().LivingRoomCount,
                        RoomCount = group.First().RoomCount,
                        HouseTypeName = group.First().HouseTypeName,
                        ImagePath = group.Select(dto => dto.ImagePath).FirstOrDefault(image => image != null) ?? PathConstants.DefaultListingImagePath
                    })
                    .OrderByDescending(dto => dto.Date)
                    .ToList();

                return result;
            }
        }


        public HouseListingDetailDto GetHouseListingDetails(int listingId)
        {
            using(ReaContext context = new ReaContext()) {

                var query = from houseListing in context.HouseListings
                            join listing in context.Listings on houseListing.ListingId equals listing.ListingId
                            join city in context.Cities on listing.CityId equals city.Id
                            join district in context.Districts on listing.DistrictId equals district.Id
                            join houseType in context.HouseTypes on houseListing.TypeId equals houseType.Id
                            join listingType in context.ListingTypes on listing.ListingTypeId equals listingType.Id
                            join user in context.Users on listing.UserId equals user.Id

                            where listing.ListingId == listingId
                            select new HouseListingDetailDto
                            {
                                //HouseListing
                                Id = houseListing.HouseListingId,
                                RoomCount = houseListing.RoomCount,
                                BathroomCount = houseListing.BathroomCount,
                                LivingRoomCount = houseListing.LivingRoomCount,
                                FloorCount = houseListing.FloorCount,
                                CurrentFloor = houseListing.CurrentFloor,
                                HasGarden = houseListing.HasGarden,
                                HasBalcony = houseListing.HasBalcony,
                                HasElevator = houseListing.HasElevator,
                                HasFurniture = houseListing.HasFurniture,
                                HasParking = houseListing.HasParking,
                                IsInGatedCommunity = houseListing.IsInGatedCommunity,
                                BuildingAge = houseListing.BuildingAge,
                                Address = houseListing.Address,
                                //City
                                CityName = city.CityName,
                                //District
                                DistrictName = district.DistrictName,
                                //Listing
                                ListingId = listingType.Id,
                                Title = listing.Title,
                                Description = listing.Description,
                                Date = listing.Date,
                                Price = listing.Price,
                                //User
                                FirstName =user.FirstName,
                                LastName =user.LastName,
                                UserEmail = user.Email,
                                //ListingType
                                ListingTypeName=listingType.ListingTypeName,
                                //HouseType
                                HouseTypeName = houseType.Name
                            };

                return query.First();
            }



        }
    }
}
