using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfListingDal : EfRepositoryBase<ReaContext, Listing>, IListingDal
    {
        public List<ListingDto> GetListingsByFilter(ListingFilterObject filter)
        {
            using (ReaContext context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;

                var query = context.Listings.Where(l => l.Status == true);

                if (filter != null)
                {
                    if (filter.ListingId.HasValue)
                    {
                        query = query.Where(dto => dto.ListingId == filter.ListingId);
                    }
                    if (filter.ListingTypeId.HasValue)
                    {
                        query = query.Where(dto => dto.ListingTypeId == filter.ListingTypeId);
                    }
                    if (filter.CityId.HasValue)
                    {
                        query = query.Where(dto => dto.CityId == filter.CityId);
                    }
                    if (filter.DistrictId.HasValue)
                    {
                        query = query.Where(dto => dto.DistrictId == filter.DistrictId);
                    }
                    if (filter.MaxPrice.HasValue)
                    {
                        query = query.Where(dto => dto.Price <= filter.MaxPrice);
                    }
                    if (filter.MinPrice.HasValue)
                    {
                        query = query.Where(dto => dto.Price >= filter.MinPrice);
                    }
                    if (filter.MaxSquareMeter.HasValue)
                    {
                        query = query.Where(dto => dto.SquareMeter <= filter.MaxSquareMeter);
                    }
                    if (filter.MinSquareMeter.HasValue)
                    {
                        query = query.Where(dto => dto.SquareMeter >= filter.MinSquareMeter);
                    }
                    if (!filter.SearchText.IsNullOrEmpty())
                    {
                        var filterText = filter.SearchText.ToLower();
                        query = query.Where(dto => dto.Title.Contains(filterText) || dto.ListingId.ToString() == filterText);
                    }
                }

                var listings = query
                    .Join(context.Cities,
                         listing => listing.CityId,
                         city => city.Id,
                         (listing, city) => new { listing, city })
                   .Join(context.Districts,
                         listingCity => listingCity.listing.DistrictId,
                         district => district.Id,
                         (listingCity, district) => new { listingCity.listing, listingCity.city, district })
                   .Join(context.ListingTypes,
                         listingDistrict => listingDistrict.listing.ListingTypeId,
                         listingType => listingType.Id,
                         (listingDistrict, listingType) => new { listingDistrict.listing, listingDistrict.city, listingDistrict.district, listingType })
                   .Join(context.PropertyTypes,
                         listingTypeCity => listingTypeCity.listing.PropertyTypeId,
                         propertyType => propertyType.Id,
                         (listingTypeCity, propertyType) => new { listingTypeCity.listing, listingTypeCity.city, listingTypeCity.district, listingTypeCity.listingType, propertyType })
                   .GroupJoin(context.ListingImages,
                              listingInfo => listingInfo.listing.ListingId,
                              image => image.ListingId,
                              (listingInfo, images) => new { listingInfo, images })
                   .SelectMany(listingWithImages => listingWithImages.images.DefaultIfEmpty(),
                    (listingWithImages, image) => new ListingDto
                    {
                        Id = listingWithImages.listingInfo.listing.ListingId,
                        CityName = listingWithImages.listingInfo.city.CityName,
                        Date = listingWithImages.listingInfo.listing.Date,
                        Description = listingWithImages.listingInfo.listing.Description,
                        DistrictName = listingWithImages.listingInfo.district.DistrictName,
                        Price = listingWithImages.listingInfo.listing.Price,
                        SquareMeter = listingWithImages.listingInfo.listing.SquareMeter,
                        Title = listingWithImages.listingInfo.listing.Title,
                        ListingTypeName = listingWithImages.listingInfo.listingType.ListingTypeName,
                        PropertyTypeName = listingWithImages.listingInfo.propertyType.Name,
                        ImagePath = image != null ? image.ImagePath : defaultImagePath,
                        Status = listingWithImages.listingInfo.listing.Status,
                        Address = listingWithImages.listingInfo.listing.Address

                    });




                // Select
                var result = listings
               .GroupBy(dto => dto.Id)
                     .Select(group => new ListingDto
                     {
                         Id = group.Key,
                         Title = group.First().Title,
                         Description = group.First().Description,
                         CityName = group.First().CityName,
                         DistrictName = group.First().DistrictName,
                         ListingTypeName = group.First().ListingTypeName,
                         PropertyTypeName = group.First().PropertyTypeName,
                         Price = group.First().Price,
                         Date = group.First().Date,
                         SquareMeter = group.First().SquareMeter,
                         ImagePath = group.Select(dto => dto.ImagePath).FirstOrDefault(image => image != null) ?? PathConstants.DefaultListingImagePath,
                         Status = group.First().Status,
                         Address = group.First().Address,
                     }).OrderByDescending(dto => dto.Date)
              .ToList();
                return result;

            }




        }
        public List<ListingDto> GetListingDetails()
        {
            using (var context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;

                var query = context.Listings.Where(l => l.Status == true)
                    .Join(context.Cities,
                          listing => listing.CityId,
                          city => city.Id,
                          (listing, city) => new { listing, city })
                    .Join(context.Districts,
                          listingCity => listingCity.listing.DistrictId,
                          district => district.Id,
                          (listingCity, district) => new { listingCity.listing, listingCity.city, district })
                    .Join(context.ListingTypes,
                          listingDistrict => listingDistrict.listing.ListingTypeId,
                          listingType => listingType.Id,
                          (listingDistrict, listingType) => new { listingDistrict.listing, listingDistrict.city, listingDistrict.district, listingType })
                    .Join(context.PropertyTypes,
                          listingTypeCity => listingTypeCity.listing.PropertyTypeId,
                          propertyType => propertyType.Id,
                          (listingTypeCity, propertyType) => new { listingTypeCity.listing, listingTypeCity.city, listingTypeCity.district, listingTypeCity.listingType, propertyType })
                    .GroupJoin(context.ListingImages,
                               listingInfo => listingInfo.listing.ListingId,
                               image => image.ListingId,
                               (listingInfo, images) => new { listingInfo, images })
                    .SelectMany(listingWithImages => listingWithImages.images.DefaultIfEmpty(),
                     (listingWithImages, image) => new ListingDto
                     {
                         Id = listingWithImages.listingInfo.listing.ListingId,
                         CityName = listingWithImages.listingInfo.city.CityName,
                         Date = listingWithImages.listingInfo.listing.Date,
                         Description = listingWithImages.listingInfo.listing.Description,
                         DistrictName = listingWithImages.listingInfo.district.DistrictName,
                         Price = listingWithImages.listingInfo.listing.Price,
                         SquareMeter = listingWithImages.listingInfo.listing.SquareMeter,
                         Title = listingWithImages.listingInfo.listing.Title,
                         ListingTypeName = listingWithImages.listingInfo.listingType.ListingTypeName,
                         PropertyTypeName = listingWithImages.listingInfo.propertyType.Name,
                         ImagePath = image != null ? image.ImagePath : defaultImagePath,
                         Status = listingWithImages.listingInfo.listing.Status
                     });

                query = query.Where(dto => dto.Status == true);


                var result = query
                .GroupBy(dto => dto.Id)
             .Select(group => new ListingDto
             {
                 Id = group.Key,
                 Title = group.First().Title,
                 Description = group.First().Description,
                 CityName = group.First().CityName,
                 DistrictName = group.First().DistrictName,
                 ListingTypeName = group.First().ListingTypeName,
                 PropertyTypeName = group.First().PropertyTypeName,
                 Price = group.First().Price,
                 Date = group.First().Date,
                 SquareMeter = group.First().SquareMeter,
                 ImagePath = group.Select(dto => dto.ImagePath).FirstOrDefault(image => image != null) ?? PathConstants.DefaultListingImagePath
             }).OrderByDescending(dto => dto.Date)
              .ToList();
                return result;
            }
        }

        public List<ListingDto> GetListingDetailsByUserId(int userId)
        {
            using (var context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;

                var query = context.Listings
                    .Where(listing => listing.UserId == userId) // UserId'ye göre ilanları filtrele
                    .Join(context.Cities,
                          listing => listing.CityId,
                          city => city.Id,
                          (listing, city) => new { listing, city })
                    .Join(context.Districts,
                          listingCity => listingCity.listing.DistrictId,
                          district => district.Id,
                          (listingCity, district) => new { listingCity.listing, listingCity.city, district })
                    .Join(context.ListingTypes,
                          listingDistrict => listingDistrict.listing.ListingTypeId,
                          listingType => listingType.Id,
                          (listingDistrict, listingType) => new { listingDistrict.listing, listingDistrict.city, listingDistrict.district, listingType })
                    .Join(context.PropertyTypes,
                          listingTypeCity => listingTypeCity.listing.PropertyTypeId,
                          propertyType => propertyType.Id,
                          (listingTypeCity, propertyType) => new { listingTypeCity.listing, listingTypeCity.city, listingTypeCity.district, listingTypeCity.listingType, propertyType })
                    .GroupJoin(context.ListingImages,
                               listingInfo => listingInfo.listing.ListingId,
                               image => image.ListingId,
                               (listingInfo, images) => new { listingInfo, images })
                    .SelectMany(listingWithImages => listingWithImages.images.DefaultIfEmpty(),
                     (listingWithImages, image) => new ListingDto
                     {
                         Id = listingWithImages.listingInfo.listing.ListingId,
                         CityName = listingWithImages.listingInfo.city.CityName,
                         Date = listingWithImages.listingInfo.listing.Date,
                         Description = listingWithImages.listingInfo.listing.Description,
                         DistrictName = listingWithImages.listingInfo.district.DistrictName,
                         Price = listingWithImages.listingInfo.listing.Price,
                         SquareMeter = listingWithImages.listingInfo.listing.SquareMeter,
                         Title = listingWithImages.listingInfo.listing.Title,
                         ListingTypeName = listingWithImages.listingInfo.listingType.ListingTypeName,
                         PropertyTypeName = listingWithImages.listingInfo.propertyType.Name,
                         ImagePath = image != null ? image.ImagePath : defaultImagePath,
                         Status = listingWithImages.listingInfo.listing.Status
                     });

                query = query.Where(dto => dto.Status == true);



                var result = query
                .GroupBy(dto => dto.Id)
                .Select(group => new ListingDto
                {
                    Id = group.Key,
                    Title = group.First().Title,
                    Description = group.First().Description,
                    CityName = group.First().CityName,
                    DistrictName = group.First().DistrictName,
                    ListingTypeName = group.First().ListingTypeName,
                    PropertyTypeName = group.First().PropertyTypeName,
                    Price = group.First().Price,
                    Date = group.First().Date,
                    SquareMeter = group.First().SquareMeter,
                    ImagePath = group.Select(dto => dto.ImagePath).FirstOrDefault(image => image != null) ?? PathConstants.DefaultListingImagePath
                }).OrderByDescending(dto => dto.Date)
                 .ToList();
                return result;
            }
        }

        public List<ListingDto> GetPaginatedListingsWithFilterAndSorting(ListingFilterObject filter, SortingObject sorting, int pageNumber, int pageSize)
        {
            using (ReaContext context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;
                var skipAmount = (pageNumber - 1) * pageSize;

                var query = context.Listings.Where(l => l.Status == true);

                if (filter != null)
                {
                    if (filter.ListingId.HasValue)
                    {
                        query = query.Where(dto => dto.ListingId == filter.ListingId);
                    }
                    if (filter.ListingTypeId.HasValue)
                    {
                        query = query.Where(dto => dto.ListingTypeId == filter.ListingTypeId);
                    }
                    if (filter.CityId.HasValue)
                    {
                        query = query.Where(dto => dto.CityId == filter.CityId);
                    }
                    if (filter.DistrictId.HasValue)
                    {
                        query = query.Where(dto => dto.DistrictId == filter.DistrictId);
                    }
                    if (filter.MaxPrice.HasValue)
                    {
                        query = query.Where(dto => dto.Price <= filter.MaxPrice);
                    }
                    if (filter.MinPrice.HasValue)
                    {
                        query = query.Where(dto => dto.Price >= filter.MinPrice);
                    }
                    if (filter.MaxSquareMeter.HasValue)
                    {
                        query = query.Where(dto => dto.SquareMeter <= filter.MaxSquareMeter);
                    }
                    if (filter.MinSquareMeter.HasValue)
                    {
                        query = query.Where(dto => dto.SquareMeter >= filter.MinSquareMeter);
                    }
                    if (!filter.SearchText.IsNullOrEmpty())
                    {
                        var filterText = filter.SearchText.ToLower();
                        query = query.Where(dto => dto.Title.Contains(filterText) || dto.ListingId.ToString() == filterText);
                    }
                }

                if (sorting != null && !string.IsNullOrEmpty(sorting.SortBy))
                {
                    switch (sorting.SortBy.ToLower())
                    {
                        case "date":
                            query = sorting.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(l => l.Date) :
                                query.OrderByDescending(l => l.Date);
                            break;
                        case "price":
                            query = sorting.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(l => l.Price) :
                                query.OrderByDescending(l => l.Price);
                            break;
                        case "squaremeter":
                                query = sorting.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(l => l.SquareMeter) :
                                query.OrderByDescending(l => l.SquareMeter);
                            break;
                        // Diğer sıralama seçenekleri buraya eklenebilir
                        default:
                            // Varsayılan olarak belirli bir sütuna göre sırala
                            query = query.OrderByDescending(l => l.Date);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(l => l.Date);
                }

                var paginatedListings = query.Skip(skipAmount).Take(pageSize)
                    .Join(context.Cities,
                          listing => listing.CityId,
                          city => city.Id,
                          (listing, city) => new { listing, city })
                    .Join(context.Districts,
                          listingCity => listingCity.listing.DistrictId,
                          district => district.Id,
                          (listingCity, district) => new { listingCity.listing, listingCity.city, district })
                    .Join(context.ListingTypes,
                          listingDistrict => listingDistrict.listing.ListingTypeId,
                          listingType => listingType.Id,
                          (listingDistrict, listingType) => new { listingDistrict.listing, listingDistrict.city, listingDistrict.district, listingType })
                    .Join(context.PropertyTypes,
                          listingTypeCity => listingTypeCity.listing.PropertyTypeId,
                          propertyType => propertyType.Id,
                          (listingTypeCity, propertyType) => new { listingTypeCity.listing, listingTypeCity.city, listingTypeCity.district, listingTypeCity.listingType, propertyType })
                    .GroupJoin(context.ListingImages,
                               listingInfo => listingInfo.listing.ListingId,
                               image => image.ListingId,
                               (listingInfo, images) => new { listingInfo, images })
                    .SelectMany(listingWithImages => listingWithImages.images.DefaultIfEmpty(),
                     (listingWithImages, image) => new ListingDto
                     {
                         Id = listingWithImages.listingInfo.listing.ListingId,
                         CityName = listingWithImages.listingInfo.city.CityName,
                         Date = listingWithImages.listingInfo.listing.Date,
                         Description = listingWithImages.listingInfo.listing.Description,
                         DistrictName = listingWithImages.listingInfo.district.DistrictName,
                         Price = listingWithImages.listingInfo.listing.Price,
                         SquareMeter = listingWithImages.listingInfo.listing.SquareMeter,
                         Title = listingWithImages.listingInfo.listing.Title,
                         ListingTypeName = listingWithImages.listingInfo.listingType.ListingTypeName,
                         PropertyTypeName = listingWithImages.listingInfo.propertyType.Name,
                         ImagePath = image != null ? image.ImagePath : defaultImagePath,
                         Status = listingWithImages.listingInfo.listing.Status
                     }).ToList();

                return paginatedListings;

            }
        }

    }
}
