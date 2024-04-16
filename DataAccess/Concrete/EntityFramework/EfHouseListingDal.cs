using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Constants;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfHouseListingDal : EfRepositoryBase<ReaContext, HouseListing>, IHouseListingDal
    {
        public List<HouseListingDto> GetHouseListings()
        {
            using (var context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath;

                var result = (from houseListing in context.HouseListings
                              join listing in context.Listings on houseListing.ListingId equals listing.ListingId
                              join city in context.Cities on listing.CityId equals city.Id
                              join district in context.Districts on listing.DistrictId equals district.Id
                              join houseType in context.HouseTypes on houseListing.TypeId equals houseType.Id
                              join listingType in context.ListingTypes on listing.ListingTypeId equals listingType.Id
                              join image in context.ListingImages
                                  .OrderBy(img => img.Id)
                                    .Take(1)
                                  .DefaultIfEmpty() on listing.ListingId equals image.ListingId into images
                              from image in images.DefaultIfEmpty()
                              select new HouseListingDto
                              {
                                  Id = houseListing.HouseListingId,
                                  ListingId = listing.ListingId,
                                  CityName = city.CityName,
                                  DistrictName = district.DistrictName,
                                  Title = listing.Title,
                                  Description = listing.Description,
                                  Price = listing.Price,
                                  BathroomCount = houseListing.BathroomCount,
                                  LivingRoomCount = houseListing.LivingRoomCount,
                                  HouseTypeName = houseType.Name,
                                  ListingTypeName = listingType.ListingTypeName,
                                  RoomCount = houseListing.RoomCount,
                                  ImagePath = image != null ? image.ImagePath : defaultImagePath,
                                  Date = listing.Date
                              }
                              ).OrderByDescending(dto => dto.Date).ToList();

                return result;
            }
        }
    }
}
