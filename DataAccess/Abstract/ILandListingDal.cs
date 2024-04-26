using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ILandListingDal : IEntityRepository<LandListing>
    {
        List<LandListingDto> GetLandListings();
        LandListingDetailDto GetLandListingDetail(int listingId);
        List<LandListingDto> GetAllByFilter(LandListingFilterObject filter);
    }
}
