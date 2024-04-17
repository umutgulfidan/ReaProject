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
    public interface IHouseListingDal :IEntityRepository<HouseListing>
    {
        List<HouseListingDto> GetHouseListings();
        List<HouseListingDto> GetHouseListingsByFilter(HouseFilterObject filter);
        HouseListingDetailDto GetHouseListingDetails(int listingId);
    }
}
