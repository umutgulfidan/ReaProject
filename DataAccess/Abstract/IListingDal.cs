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
    public interface IListingDal : IEntityRepository<Listing>
    {
        List<ListingDto> GetListingDetails();
        List<ListingDto> GetListingDetailsByUserId(int id);
        List<ListingDto> GetListingsByFilter(ListingFilterObject filter);
    }
}
