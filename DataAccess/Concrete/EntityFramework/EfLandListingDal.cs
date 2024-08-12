using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfLandListingDal : EfRepositoryBase<ReaContext, LandListing>, ILandListingDal
    {

        public List<LandListingDto> GetAllByFilter(LandListingFilterObject filter)
        {
            using (ReaContext context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;
                var query = context.LandListings
                            .Join(context.Listings,
                                  landListing => landListing.ListingId,
                                  listing => listing.ListingId,
                                  (landListing, listing) => new { landListing, listing })
                            .Join(context.Users,
                                  landListingListing => landListingListing.listing.UserId,
                                  user => user.Id,
                                  (landListingListing, user) => new { landListingListing.landListing, landListingListing.listing, user })
                            .Join(context.Cities,
                                  landListingUser => landListingUser.listing.CityId,
                                  city => city.Id,
                                  (landListingUser, city) => new { landListingUser.landListing, landListingUser.listing, landListingUser.user, city })
                            .Join(context.Districts,
                                  landListingCity => landListingCity.listing.DistrictId,
                                  district => district.Id,
                                  (landListingCity, district) => new { landListingCity.landListing, landListingCity.listing, landListingCity.user, landListingCity.city, district })
                            .Join(context.ListingTypes,
                                  landListingDistrict => landListingDistrict.listing.ListingTypeId,
                                  listingType => listingType.Id,
                                  (landListingDistrict, listingType) => new { landListingDistrict.landListing, landListingDistrict.listing, landListingDistrict.user, landListingDistrict.city, landListingDistrict.district, listingType })
                            .GroupJoin(context.ListingImages,
                                       landListingTypeInfo => landListingTypeInfo.listing.ListingId,
                                       image => image.ListingId,
                                       (landListingTypeInfo, images) => new { landListingTypeInfo, images });

                query = query.Where(dto => dto.landListingTypeInfo.listing.Status == true && dto.landListingTypeInfo.user.Status == true);


                if (filter != null)
                {
                    if (!filter.SearchText.IsNullOrEmpty())
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.ListingId.ToString() == filter.SearchText || l.landListingTypeInfo.listing.Title.Contains(filter.SearchText));
                    }
                    if (filter.ListingTypeId.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.ListingTypeId == filter.ListingTypeId);
                    }
                    if (filter.CityId.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.CityId == filter.CityId);
                    }
                    if (filter.DistrictId.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.DistrictId == filter.DistrictId);
                    }
                    if (filter.MinPrice.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.Price >= filter.MinPrice);
                    }
                    if (filter.MaxPrice.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.Price <= filter.MaxPrice);
                    }
                    if (filter.MinSquareMeter.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.SquareMeter >= filter.MinSquareMeter);
                    }
                    if (filter.MaxSquareMeter.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.SquareMeter <= filter.MaxSquareMeter);
                    }
                    if (filter.FloorEquivalent.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.landListing.FloorEquivalent == filter.FloorEquivalent);
                    }
                }

                var result = query.SelectMany(landListingWithImages => landListingWithImages.images.DefaultIfEmpty(),
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
                        })
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
                    Status = group.First().Status,


                    // ImagePath'i bulmak için grup içerisindeki ilk geçerli (null olmayan) değeri seçin
                    ImagePath = group.Select(dto => dto.ImagePath).FirstOrDefault(image => image != null) ?? PathConstants.DefaultListingImagePath
                })
                .OrderByDescending(dto => dto.Date)
                .ToList();
                return result;
            }

        }

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
                    .Join(context.Users, // Assuming there's a Users table
                          houseListingType => houseListingType.listing.UserId,
                          user => user.Id,
                          (houseListingType, user) => new { houseListingType.landListing, houseListingType.listing, houseListingType.city, houseListingType.district, houseListingType.listingType, user })
                    .GroupJoin(context.ListingImages,
                               houseListingWithImages => houseListingWithImages.listing.ListingId,
                               image => image.ListingId,
                               (houseListingWithImages, images) => new { houseListingWithImages, images })
                    .SelectMany(houseListingWithImages => houseListingWithImages.images.DefaultIfEmpty(),
                        (houseListingWithImages, image) => new LandListingDto
                        {
                            Id = houseListingWithImages.houseListingWithImages.landListing.Id,
                            ListingId = houseListingWithImages.houseListingWithImages.listing.ListingId,
                            SquareMeter = houseListingWithImages.houseListingWithImages.listing.SquareMeter,
                            CityName = houseListingWithImages.houseListingWithImages.city.CityName,
                            DistrictName = houseListingWithImages.houseListingWithImages.district.DistrictName,
                            Title = houseListingWithImages.houseListingWithImages.listing.Title,
                            Description = houseListingWithImages.houseListingWithImages.listing.Description,
                            Price = houseListingWithImages.houseListingWithImages.listing.Price,
                            ListingTypeName = houseListingWithImages.houseListingWithImages.listingType.ListingTypeName,
                            ImagePath = image != null ? image.ImagePath : defaultImagePath,
                            Date = houseListingWithImages.houseListingWithImages.listing.Date,
                            ParcelNo = houseListingWithImages.houseListingWithImages.landListing.ParcelNo,
                            IslandNo = houseListingWithImages.houseListingWithImages.landListing.IslandNo,
                            SheetNo = houseListingWithImages.houseListingWithImages.landListing.SheetNo,
                            FloorEquivalent = houseListingWithImages.houseListingWithImages.landListing.FloorEquivalent,
                            Address = houseListingWithImages.houseListingWithImages.listing.Address,
                            Status = houseListingWithImages.houseListingWithImages.listing.Status,
                            UserStatus = houseListingWithImages.houseListingWithImages.user.Status,
                        });

                query = query.Where(dto => dto.Status == true && dto.UserStatus == true);

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

        public List<LandListingDto> GetPaginatedListingsWithFilterAndSorting(LandListingFilterObject filter, SortingObject sorting, int pageNumber, int pageSize)
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
                    .Join(context.Users, // Join user table
                          houseListingType => houseListingType.listing.UserId,
                          user => user.Id,
                          (houseListingType, user) => new { houseListingType.landListing, houseListingType.listing, houseListingType.city, houseListingType.district, houseListingType.listingType, user })
                    .GroupJoin(context.ListingImages,
                               landListingTypeInfo => landListingTypeInfo.listing.ListingId,
                               image => image.ListingId,
                               (landListingTypeInfo, images) => new { landListingTypeInfo, images });

                // Filtreleme
                query = query.Where(dto => dto.landListingTypeInfo.listing.Status == true && dto.landListingTypeInfo.user.Status == true);


                if (filter != null)
                {
                    if (!filter.SearchText.IsNullOrEmpty())
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.ListingId.ToString() == filter.SearchText || l.landListingTypeInfo.listing.Title.Contains(filter.SearchText));
                    }
                    if (filter.ListingTypeId.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.ListingTypeId == filter.ListingTypeId);
                    }
                    if (filter.CityId.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.CityId == filter.CityId);
                    }
                    if (filter.DistrictId.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.DistrictId == filter.DistrictId);
                    }
                    if (filter.MinPrice.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.Price >= filter.MinPrice);
                    }
                    if (filter.MaxPrice.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.Price <= filter.MaxPrice);
                    }
                    if (filter.MinSquareMeter.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.SquareMeter >= filter.MinSquareMeter);
                    }
                    if (filter.MaxSquareMeter.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.listing.SquareMeter <= filter.MaxSquareMeter);
                    }
                    if (filter.FloorEquivalent.HasValue)
                    {
                        query = query.Where(l => l.landListingTypeInfo.landListing.FloorEquivalent == filter.FloorEquivalent);
                    }
                }

                //Sıralama
                if (sorting != null && !string.IsNullOrEmpty(sorting.SortBy))
                {
                    switch (sorting.SortBy.ToLower())
                    {
                        case "date":
                            query = sorting.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(l => l.landListingTypeInfo.listing.Date) :
                                query.OrderByDescending(l => l.landListingTypeInfo.listing.Date);
                            break;
                        case "price":
                            query = sorting.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(l => l.landListingTypeInfo.listing.Price) :
                                query.OrderByDescending(l => l.landListingTypeInfo.listing.Price);
                            break;
                        case "squaremeter":
                            query = sorting.SortDirection == SortDirection.Ascending ?
                            query.OrderBy(l => l.landListingTypeInfo.listing.SquareMeter) :
                            query.OrderByDescending(l => l.landListingTypeInfo.listing.SquareMeter);
                            break;
                        // Diğer sıralama seçenekleri buraya eklenebilir
                        default:
                            // Varsayılan olarak belirli bir sütuna göre sırala
                            query = query.OrderByDescending(l => l.landListingTypeInfo.listing.Date);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(l => l.landListingTypeInfo.listing.Date);
                }


                var result = query
                .AsEnumerable()
                .GroupBy(dto => dto.landListingTypeInfo.listing.ListingId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(group => new LandListingDto
                {
                    Id = group.First().landListingTypeInfo.landListing.Id,
                    Title = group.First().landListingTypeInfo.listing.Title,
                    Description = group.First().landListingTypeInfo.listing.Description,
                    CityName = group.First().landListingTypeInfo.city.CityName,
                    DistrictName = group.First().landListingTypeInfo.district.DistrictName,
                    ListingTypeName = group.First().landListingTypeInfo.listingType.ListingTypeName,
                    Price = group.First().landListingTypeInfo.listing.Price,
                    Date = group.First().landListingTypeInfo.listing.Date,
                    SquareMeter = group.First().landListingTypeInfo.listing.SquareMeter,
                    ParcelNo = group.First().landListingTypeInfo.landListing.ParcelNo,
                    Address = group.First().landListingTypeInfo.listing.Address,
                    FloorEquivalent = group.First().landListingTypeInfo.landListing.FloorEquivalent,
                    IslandNo = group.First().landListingTypeInfo.landListing.IslandNo,
                    ListingId = group.First().landListingTypeInfo.listing.ListingId,
                    SheetNo = group.First().landListingTypeInfo.landListing.SheetNo,
                    Status = group.First().landListingTypeInfo.listing.Status,

                    // ImagePath'i bulmak için grup içerisindeki ilk geçerli (null olmayan) değeri seçin
                    ImagePath = group.SelectMany(dto => dto.images).Select(img => img.ImagePath).FirstOrDefault() ?? PathConstants.DefaultListingImagePath
                })
                .ToList();
                return result;
            }
        }

        public int GetPassiveLandListingCount()
        {
            using (ReaContext context = new ReaContext())
            {
                var query = from landListing in context.LandListings
                            join listing in context.Listings on landListing.ListingId equals listing.ListingId
                            join user in context.Users on listing.UserId equals user.Id
                            where !(user.Status == true && listing.Status == true)
                            select landListing;

                return query.Count();
            }
        }

        public int GetActiveLandListingCount()
        {
            using (ReaContext context = new ReaContext())
            {
                var query = from landListing in context.LandListings
                            join listing in context.Listings on landListing.ListingId equals listing.ListingId
                            join user in context.Users on listing.UserId equals user.Id
                            where (user.Status == true && listing.Status == true)
                            select landListing;

                return query.Count();
            }
        }
    }
}
