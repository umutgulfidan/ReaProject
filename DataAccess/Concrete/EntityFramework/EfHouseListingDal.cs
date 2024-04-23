using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfHouseListingDal : EfRepositoryBase<ReaContext, HouseListing>, IHouseListingDal
    {
        public List<HouseListingDto> GetHouseListingsByFilter(HouseFilterObject filter)
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
                            SquareMeter = houseListingWithImages.houseListingTypeInfo.listing.SquareMeter,
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
                            BuildingAge = houseListingWithImages.houseListingTypeInfo.houseListing.BuildingAge,
                            HasBalcony = (bool)houseListingWithImages.houseListingTypeInfo.houseListing.HasBalcony,
                            HasElevator = (bool)houseListingWithImages.houseListingTypeInfo.houseListing.HasElevator,
                            HasFurniture = (bool)houseListingWithImages.houseListingTypeInfo.houseListing.HasFurniture,
                            HasGarden = (bool)houseListingWithImages.houseListingTypeInfo.houseListing.HasGarden,
                            HasParking = (bool)houseListingWithImages.houseListingTypeInfo.houseListing.HasParking,
                            IsInGatedCommunity = (bool)houseListingWithImages.houseListingTypeInfo.houseListing.IsInGatedCommunity,
                            Date = houseListingWithImages.houseListingTypeInfo.listing.Date,
                            Status = houseListingWithImages.houseListingTypeInfo.listing.Status
                        }) ;

                query = query.Where(dto=>dto.Status == true );

                // Filtre uygulamaları
                if (filter.RoomCount.HasValue)
                {
                    query = query.Where(dto => dto.RoomCount == filter.RoomCount.Value);
                }

                if (filter.BathroomCount.HasValue)
                {
                    query = query.Where(dto => dto.BathroomCount == filter.BathroomCount.Value);
                }

                if (filter.LivingRoomCount.HasValue)
                {
                    query = query.Where(dto => dto.LivingRoomCount == filter.LivingRoomCount.Value);
                }

                if (!filter.HouseTypeName.IsNullOrEmpty())
                {
                    query = query.Where(dto => dto.HouseTypeName == filter.HouseTypeName);
                }

                if (!filter.CityName.IsNullOrEmpty())
                {
                    query = query.Where(dto => dto.CityName == filter.CityName);
                }

                if (!filter.DistrictName.IsNullOrEmpty())
                {
                    query = query.Where(dto => dto.DistrictName == filter.DistrictName);
                }

                if (filter.MaxPrice.HasValue)
                {
                    query = query.Where(dto => dto.Price <= filter.MaxPrice.Value);
                }

                if (filter.MinPrice.HasValue)
                {
                    query = query.Where(dto => dto.Price >= filter.MinPrice.Value);
                }

                if (filter.MaxBuildAge.HasValue)
                {
                    query = query.Where(dto => dto.BuildingAge <= filter.MaxBuildAge.Value);
                }

                if (filter.HasGarden.HasValue)
                {
                    query = query.Where(dto => dto.HasGarden == filter.HasGarden.Value);
                }

                if (filter.HasElevator.HasValue)
                {
                    query = query.Where(dto => dto.HasElevator == filter.HasElevator.Value);
                }

                if (filter.HasFurniture.HasValue)
                {
                    query = query.Where(dto => dto.HasFurniture == filter.HasFurniture.Value);
                }

                if (filter.HasParking.HasValue)
                {
                    query = query.Where(dto => dto.HasParking == filter.HasParking.Value);
                }

                if (filter.HasBalcony.HasValue)
                {
                    query = query.Where(dto => dto.HasBalcony == filter.HasBalcony.Value);
                }

                if (filter.IsInGatedCommunity.HasValue)
                {
                    query = query.Where(dto => dto.IsInGatedCommunity == filter.IsInGatedCommunity.Value);
                }
                if(filter.MinSquareMeter.HasValue) {
                    query = query.Where(dto=>dto.SquareMeter>=filter.MinSquareMeter.Value);
                }
                if (filter.MaxSquareMeter.HasValue)
                {
                    query = query.Where(dto => dto.SquareMeter <= filter.MaxSquareMeter.Value);
                }
                if (!filter.ListingTypeName.IsNullOrEmpty())
                {
                    query = query.Where(dto=>dto.ListingTypeName == filter.ListingTypeName);
                }
                if (!filter.SearchText.IsNullOrEmpty())
                {
                    var searchText = filter.SearchText.ToLower();
                    query = query.Where(dto => dto.ListingId.ToString() == searchText || dto.Title.Contains(searchText));
                }




                // ListingId'ye göre gruplama ve ilk kaydı seçme işlemi
                var result = query
                .AsEnumerable()
                 .GroupBy(dto => dto.ListingId)
              .Select(group => new HouseListingDto
              {
                  Id = group.First().Id,
                  BuildingAge = group.First().BuildingAge,
                  HasBalcony = group.First().HasBalcony,
                  HasElevator = group.First().HasElevator,
                  HasFurniture = group.First().HasFurniture,
                  HasGarden = group.First().HasGarden,
                  HasParking = group.First().HasParking,
                  IsInGatedCommunity = group.First().IsInGatedCommunity,
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
                  SquareMeter = group.First().SquareMeter,


                  // ImagePath'i bulmak için grup içerisindeki ilk geçerli (null olmayan) değeri seçin
                  ImagePath = group.Select(dto => dto.ImagePath).FirstOrDefault(image => image != null) ?? PathConstants.DefaultListingImagePath
              })
     .OrderByDescending(dto => dto.Date)
     .ToList();


                return result;
            }
        }
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
                            Date = houseListingWithImages.houseListingTypeInfo.listing.Date,
                            Status = houseListingWithImages.houseListingTypeInfo.listing.Status

                        });

                query = query.Where(dto=> dto.Status == true);


                // ListingId'ye göre gruplama ve ilk kaydı seçme işlemi
                var result = query
                    .GroupBy(dto => dto.ListingId)
                    .Select(group => new HouseListingDto
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
                        BathroomCount = group.First().BathroomCount,
                        LivingRoomCount = group.First().LivingRoomCount,
                        RoomCount = group.First().RoomCount,
                        HouseTypeName = group.First().HouseTypeName,
                        BuildingAge = group.First().BuildingAge,
                        HasBalcony = group.First().HasBalcony,
                        HasElevator = group.First().HasElevator,
                        HasFurniture = group.First().HasFurniture,
                        HasGarden = group.First().HasGarden,
                        HasParking = group.First().HasParking,
                        IsInGatedCommunity = group.First().IsInGatedCommunity,
                        SquareMeter = group.First().SquareMeter,


                        ImagePath = group.Select(dto => dto.ImagePath).FirstOrDefault(image => image != null) ?? PathConstants.DefaultListingImagePath
                    })
                    .OrderByDescending(dto => dto.Date)
                    .ToList();


                return result;
            }
        }


        public HouseListingDetailDto GetHouseListingDetails(int listingId)
        {
            using (ReaContext context = new ReaContext())
            {

                var query = from houseListing in context.HouseListings
                            join listing in context.Listings on houseListing.ListingId equals listing.ListingId
                            join city in context.Cities on listing.CityId equals city.Id
                            join district in context.Districts on listing.DistrictId equals district.Id
                            join houseType in context.HouseTypes on houseListing.TypeId equals houseType.Id
                            join listingType in context.ListingTypes on listing.ListingTypeId equals listingType.Id
                            join user in context.Users on listing.UserId equals user.Id

                            where listing.ListingId == listingId && listing.Status == true
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
                                //HouseType
                                HouseTypeName = houseType.Name
                                
                            };

                return query.First();
            }



        }
    }
}
