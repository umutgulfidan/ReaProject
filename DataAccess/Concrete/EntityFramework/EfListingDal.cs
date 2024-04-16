using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
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
        public List<ListingDto> GetListingDetails()
        {
            using (var context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;

                var query = context.Listings
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
                         ImagePath = image != null ? image.ImagePath : defaultImagePath
                     });


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
    }
}
