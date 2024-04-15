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
            using (ReaContext context = new ReaContext())
            {
                var defaultImagePath = PathConstants.DefaultListingImagePath; // Default resim yolunu buraya ekleyin.

                var result = (from listing in context.Listings
                              join city in context.Cities on listing.CityId equals city.Id
                              join district in context.Districts on listing.DistrictId equals district.Id
                              join type in context.ListingTypes on listing.ListingTypeId equals type.Id
                              join image in context.ListingImages
                                  .OrderBy(img => img.Id)
                                  .Take(1)
                                  .DefaultIfEmpty() on listing.ListingId equals image.ListingId into images
                              from image in images.DefaultIfEmpty()
                              select new ListingDto
                              {
                                  Id = listing.ListingId,
                                  CityName = city.CityName,
                                  Date = listing.Date,
                                  Description = listing.Description,
                                  DistrictName = district.DistrictName,
                                  Price = listing.Price,
                                  SquareMeter = listing.SquareMeter,
                                  Title = listing.Title,
                                  TypeName = type.ListingTypeName,
                                  ImagePath = image != null ? image.ImagePath : defaultImagePath
                              }).ToList(); // ToList() metodu ile sorguyu bir liste olarak döndürün

                return result; // result değişkenini dönün
            }
        }
    }
}
