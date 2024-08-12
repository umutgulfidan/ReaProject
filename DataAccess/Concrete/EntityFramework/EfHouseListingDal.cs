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
                    .Join(context.Users, // Kullanıcı tablosu ile birleştirme
                          houseListingListing => houseListingListing.listing.UserId,
                          user => user.Id,
                          (houseListingListing, user) => new { houseListingListing.houseListing, houseListingListing.listing, user })
                    .Join(context.Cities,
                          houseListingUser => houseListingUser.listing.CityId,
                          city => city.Id,
                          (houseListingUser, city) => new { houseListingUser.houseListing, houseListingUser.listing, houseListingUser.user, city })
                    .Join(context.Districts,
                          houseListingCity => houseListingCity.listing.DistrictId,
                          district => district.Id,
                          (houseListingCity, district) => new { houseListingCity.houseListing, houseListingCity.listing, houseListingCity.user, houseListingCity.city, district })
                    .Join(context.HouseTypes,
                          houseListingDistrict => houseListingDistrict.houseListing.TypeId,
                          houseType => houseType.Id,
                          (houseListingDistrict, houseType) => new { houseListingDistrict.houseListing, houseListingDistrict.listing, houseListingDistrict.user, houseListingDistrict.city, houseListingDistrict.district, houseType })
                    .Join(context.ListingTypes,
                          houseListingType => houseListingType.listing.ListingTypeId,
                          listingType => listingType.Id,
                          (houseListingType, listingType) => new { houseListingType.houseListing, houseListingType.listing, houseListingType.user, houseListingType.city, houseListingType.district, houseListingType.houseType, listingType })
                    .GroupJoin(context.ListingImages,
                               houseListingTypeInfo => houseListingTypeInfo.listing.ListingId,
                               image => image.ListingId,
                               (houseListingTypeInfo, images) => new { houseListingTypeInfo, images });

                // Sadece aktif listing ve kullanıcıları dahil et
                query = query.Where(dto => dto.houseListingTypeInfo.listing.Status == true && dto.houseListingTypeInfo.user.Status == true);

                // Filtre uygulamaları
                if (filter != null)
                {
                    if (filter.RoomCount.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.RoomCount == filter.RoomCount.Value);
                    }

                    if (filter.BathroomCount.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.BathroomCount == filter.BathroomCount.Value);
                    }

                    if (filter.LivingRoomCount.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.LivingRoomCount == filter.LivingRoomCount.Value);
                    }

                    if (filter.HouseTypeId.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.TypeId == filter.HouseTypeId);
                    }

                    if (filter.CityId.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.city.Id == filter.CityId);
                    }

                    if (filter.DistrictId.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.district.Id == filter.DistrictId);
                    }

                    if (filter.MaxPrice.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.Price <= filter.MaxPrice.Value);
                    }

                    if (filter.MinPrice.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.Price >= filter.MinPrice.Value);
                    }

                    if (filter.MaxBuildAge.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.BuildingAge <= filter.MaxBuildAge.Value);
                    }

                    if (filter.HasGarden.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasGarden == filter.HasGarden.Value);
                    }

                    if (filter.HasElevator.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasElevator == filter.HasElevator.Value);
                    }

                    if (filter.HasFurniture.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasFurniture == filter.HasFurniture.Value);
                    }

                    if (filter.HasParking.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasParking == filter.HasParking.Value);
                    }

                    if (filter.HasBalcony.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasBalcony == filter.HasBalcony.Value);
                    }

                    if (filter.IsInGatedCommunity.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.IsInGatedCommunity == filter.IsInGatedCommunity.Value);
                    }

                    if (filter.MinSquareMeter.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.SquareMeter >= filter.MinSquareMeter.Value);
                    }

                    if (filter.MaxSquareMeter.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.SquareMeter <= filter.MaxSquareMeter.Value);
                    }

                    if (filter.ListingTypeId.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.ListingTypeId == filter.ListingTypeId);
                    }

                    if (!string.IsNullOrEmpty(filter.SearchText))
                    {
                        var searchText = filter.SearchText.ToLower();
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.ListingId.ToString() == searchText || dto.houseListingTypeInfo.listing.Title.ToLower().Contains(searchText));
                    }
                }

                // ListingId'ye göre gruplama ve ilk kaydı seçme işlemi
                var result = query
                    .AsEnumerable()
                    .GroupBy(dto => dto.houseListingTypeInfo.listing.ListingId)
                    .Select(group => new HouseListingDto
                    {
                        Id = group.First().houseListingTypeInfo.houseListing.HouseListingId,
                        BuildingAge = group.First().houseListingTypeInfo.houseListing.BuildingAge,
                        HasBalcony = group.First().houseListingTypeInfo.houseListing.HasBalcony,
                        HasElevator = group.First().houseListingTypeInfo.houseListing.HasElevator,
                        HasFurniture = group.First().houseListingTypeInfo.houseListing.HasFurniture,
                        HasGarden = group.First().houseListingTypeInfo.houseListing.HasGarden,
                        HasParking = group.First().houseListingTypeInfo.houseListing.HasParking,
                        IsInGatedCommunity = group.First().houseListingTypeInfo.houseListing.IsInGatedCommunity,
                        ListingId = group.First().houseListingTypeInfo.houseListing.ListingId,
                        Title = group.First().houseListingTypeInfo.listing.Title,
                        Description = group.First().houseListingTypeInfo.listing.Description,
                        CityName = group.First().houseListingTypeInfo.city.CityName,
                        DistrictName = group.First().houseListingTypeInfo.district.DistrictName,
                        ListingTypeName = group.First().houseListingTypeInfo.listingType.ListingTypeName,
                        Price = group.First().houseListingTypeInfo.listing.Price,
                        Date = group.First().houseListingTypeInfo.listing.Date,
                        BathroomCount = group.First().houseListingTypeInfo.houseListing.BathroomCount,
                        LivingRoomCount = group.First().houseListingTypeInfo.houseListing.LivingRoomCount,
                        RoomCount = group.First().houseListingTypeInfo.houseListing.RoomCount,
                        HouseTypeName = group.First().houseListingTypeInfo.houseType.Name,
                        SquareMeter = group.First().houseListingTypeInfo.listing.SquareMeter,

                        // ImagePath'i bulmak için grup içerisindeki ilk geçerli (null olmayan) değeri seçin
                        ImagePath = group.SelectMany(dto => dto.images).Select(img => img.ImagePath).FirstOrDefault() ?? PathConstants.DefaultListingImagePath
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
                    .Join(context.Users, // Kullanıcı tablosu ile birleştirme
                          houseListingListing => houseListingListing.listing.UserId,
                          user => user.Id,
                          (houseListingListing, user) => new { houseListingListing.houseListing, houseListingListing.listing, user })
                    .Join(context.Cities,
                          houseListingUser => houseListingUser.listing.CityId,
                          city => city.Id,
                          (houseListingUser, city) => new { houseListingUser.houseListing, houseListingUser.listing, houseListingUser.user, city })
                    .Join(context.Districts,
                          houseListingCity => houseListingCity.listing.DistrictId,
                          district => district.Id,
                          (houseListingCity, district) => new { houseListingCity.houseListing, houseListingCity.listing, houseListingCity.user, houseListingCity.city, district })
                    .Join(context.HouseTypes,
                          houseListingDistrict => houseListingDistrict.houseListing.TypeId,
                          houseType => houseType.Id,
                          (houseListingDistrict, houseType) => new { houseListingDistrict.houseListing, houseListingDistrict.listing, houseListingDistrict.user, houseListingDistrict.city, houseListingDistrict.district, houseType })
                    .Join(context.ListingTypes,
                          houseListingType => houseListingType.listing.ListingTypeId,
                          listingType => listingType.Id,
                          (houseListingType, listingType) => new { houseListingType.houseListing, houseListingType.listing, houseListingType.user, houseListingType.city, houseListingType.district, houseListingType.houseType, listingType })
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
                            Status = houseListingWithImages.houseListingTypeInfo.listing.Status,
                            UserStatus = houseListingWithImages.houseListingTypeInfo.user.Status // Kullanıcı durumunu ekliyoruz
                        });

                // Sadece aktif listing ve kullanıcıları dahil et
                query = query.Where(dto => dto.Status == true && dto.UserStatus == true);

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
                                //HouseType
                                HouseTypeName = houseType.Name

                            };

                return query.First();
            }



        }

        public List<HouseListingDto> GetPaginatedListingsWithFilterAndSorting(HouseFilterObject filter, SortingObject sorting, int pageNumber, int pageSize)
        {
            using (ReaContext context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;

                var query = context.HouseListings
                    .Join(context.Listings,
                          houseListing => houseListing.ListingId,
                          listing => listing.ListingId,
                          (houseListing, listing) => new { houseListing, listing })
                    .Join(context.Users, // Kullanıcıyla birleştirme
                          houseListingListing => houseListingListing.listing.UserId,
                          user => user.Id,
                          (houseListingListing, user) => new { houseListingListing.houseListing, houseListingListing.listing, user })
                    .Join(context.Cities,
                          houseListingUser => houseListingUser.listing.CityId,
                          city => city.Id,
                          (houseListingUser, city) => new { houseListingUser.houseListing, houseListingUser.listing, houseListingUser.user, city })
                    .Join(context.Districts,
                          houseListingCity => houseListingCity.listing.DistrictId,
                          district => district.Id,
                          (houseListingCity, district) => new { houseListingCity.houseListing, houseListingCity.listing, houseListingCity.user, houseListingCity.city, district })
                    .Join(context.HouseTypes,
                          houseListingDistrict => houseListingDistrict.houseListing.TypeId,
                          houseType => houseType.Id,
                          (houseListingDistrict, houseType) => new { houseListingDistrict.houseListing, houseListingDistrict.listing, houseListingDistrict.user, houseListingDistrict.city, houseListingDistrict.district, houseType })
                    .Join(context.ListingTypes,
                          houseListingType => houseListingType.listing.ListingTypeId,
                          listingType => listingType.Id,
                          (houseListingType, listingType) => new { houseListingType.houseListing, houseListingType.listing, houseListingType.user, houseListingType.city, houseListingType.district, houseListingType.houseType, listingType })
                    .GroupJoin(context.ListingImages,
                               houseListingTypeInfo => houseListingTypeInfo.listing.ListingId,
                               image => image.ListingId,
                               (houseListingTypeInfo, images) => new { houseListingTypeInfo, images });

                // Sadece aktif listeleri ve kullanıcıları göster
                query = query.Where(dto => dto.houseListingTypeInfo.listing.Status == true && dto.houseListingTypeInfo.user.Status == true);

                // Filtre uygulamaları
                if (filter != null)
                {

                    if (filter.RoomCount.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.RoomCount == filter.RoomCount.Value);
                    }

                    if (filter.BathroomCount.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.BathroomCount == filter.BathroomCount.Value);
                    }

                    if (filter.LivingRoomCount.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.LivingRoomCount == filter.LivingRoomCount.Value);
                    }

                    if (filter.HouseTypeId.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.TypeId == filter.HouseTypeId);
                    }

                    if (filter.CityId.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.city.Id == filter.CityId);
                    }

                    if (filter.DistrictId.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.district.Id == filter.DistrictId);
                    }

                    if (filter.MaxPrice.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.Price <= filter.MaxPrice.Value);
                    }

                    if (filter.MinPrice.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.Price >= filter.MinPrice.Value);
                    }

                    if (filter.MaxBuildAge.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.BuildingAge <= filter.MaxBuildAge.Value);
                    }

                    if (filter.HasGarden.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasGarden == filter.HasGarden.Value);
                    }

                    if (filter.HasElevator.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasElevator == filter.HasElevator.Value);
                    }

                    if (filter.HasFurniture.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasFurniture == filter.HasFurniture.Value);
                    }

                    if (filter.HasParking.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasParking == filter.HasParking.Value);
                    }

                    if (filter.HasBalcony.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.HasBalcony == filter.HasBalcony.Value);
                    }

                    if (filter.IsInGatedCommunity.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.houseListing.IsInGatedCommunity == filter.IsInGatedCommunity.Value);
                    }
                    if (filter.MinSquareMeter.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.SquareMeter >= filter.MinSquareMeter.Value);
                    }
                    if (filter.MaxSquareMeter.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.SquareMeter <= filter.MaxSquareMeter.Value);
                    }
                    if (filter.ListingTypeId.HasValue)
                    {
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.ListingTypeId == filter.ListingTypeId);
                    }
                    if (!filter.SearchText.IsNullOrEmpty())
                    {
                        var searchText = filter.SearchText.ToLower();
                        query = query.Where(dto => dto.houseListingTypeInfo.listing.ListingId.ToString() == searchText || dto.houseListingTypeInfo.listing.Title.Contains(searchText));
                    }

                }


                if (sorting != null && !string.IsNullOrEmpty(sorting.SortBy))
                {
                    switch (sorting.SortBy.ToLower())
                    {
                        case "date":
                            query = sorting.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(l => l.houseListingTypeInfo.listing.Date) :
                                query.OrderByDescending(l => l.houseListingTypeInfo.listing.Date);
                            break;
                        case "price":
                            query = sorting.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(l => l.houseListingTypeInfo.listing.Price) :
                                query.OrderByDescending(l => l.houseListingTypeInfo.listing.Price);
                            break;
                        case "squaremeter":
                            query = sorting.SortDirection == SortDirection.Ascending ?
                            query.OrderBy(l => l.houseListingTypeInfo.listing.SquareMeter) :
                            query.OrderByDescending(l => l.houseListingTypeInfo.listing.SquareMeter);
                            break;
                        // Diğer sıralama seçenekleri buraya eklenebilir
                        default:
                            // Varsayılan olarak belirli bir sütuna göre sırala
                            query = query.OrderByDescending(l => l.houseListingTypeInfo.listing.Date);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(l => l.houseListingTypeInfo.listing.Date);
                }

                var result = query
                .AsEnumerable()
                .GroupBy(dto => dto.houseListingTypeInfo.listing.ListingId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(group => new HouseListingDto
                {
                    Id = group.First().houseListingTypeInfo.houseListing.HouseListingId,
                    BuildingAge = group.First().houseListingTypeInfo.houseListing.BuildingAge,
                    HasBalcony = group.First().houseListingTypeInfo.houseListing.HasBalcony,
                    HasElevator = group.First().houseListingTypeInfo.houseListing.HasElevator,
                    HasFurniture = group.First().houseListingTypeInfo.houseListing.HasFurniture,
                    HasGarden = group.First().houseListingTypeInfo.houseListing.HasGarden,
                    HasParking = group.First().houseListingTypeInfo.houseListing.HasParking,
                    IsInGatedCommunity = group.First().houseListingTypeInfo.houseListing.IsInGatedCommunity,
                    ListingId = group.First().houseListingTypeInfo.houseListing.ListingId,
                    Title = group.First().houseListingTypeInfo.listing.Title,
                    Description = group.First().houseListingTypeInfo.listing.Description,
                    CityName = group.First().houseListingTypeInfo.city.CityName,
                    DistrictName = group.First().houseListingTypeInfo.district.DistrictName,
                    ListingTypeName = group.First().houseListingTypeInfo.listingType.ListingTypeName,
                    Price = group.First().houseListingTypeInfo.listing.Price,
                    Date = group.First().houseListingTypeInfo.listing.Date,
                    BathroomCount = group.First().houseListingTypeInfo.houseListing.BathroomCount,
                    LivingRoomCount = group.First().houseListingTypeInfo.houseListing.LivingRoomCount,
                    RoomCount = group.First().houseListingTypeInfo.houseListing.RoomCount,
                    HouseTypeName = group.First().houseListingTypeInfo.houseType.Name,
                    SquareMeter = group.First().houseListingTypeInfo.listing.SquareMeter,

                    // ImagePath'i bulmak için grup içerisindeki ilk geçerli (null olmayan) değeri seçin
                    ImagePath = group.SelectMany(dto => dto.images).Select(img => img.ImagePath).FirstOrDefault() ?? PathConstants.DefaultListingImagePath
                })
                .ToList();


                return result;
            }
        }

        public int GetActiveHouseListingCount()
        {
            using(ReaContext context =  new ReaContext())
            {
                var query = from houseListing in context.HouseListings
                            join listing in context.Listings on houseListing.ListingId equals listing.ListingId
                            join user in context.Users on listing.UserId equals user.Id
                            where user.Status == true && listing.Status == true
                            select houseListing;

                return query.Count();
            }
        }

        public int GetPassiveHouseListingCount()
        {
            using (ReaContext context = new ReaContext())
            {
                var query = from houseListing in context.HouseListings
                            join listing in context.Listings on houseListing.ListingId equals listing.ListingId
                            join user in context.Users on listing.UserId equals user.Id
                            where !(user.Status == true && listing.Status == true)
                            select houseListing;

                return query.Count();
            }
        }
    }
}
