﻿using Core.DataAccess;
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
        List<ListingDetailsView> GetView();
        List<ListingDto> GetListingDetails();
        List<ListingDto> GetListingDetailsByUserId(int id);
        List<ListingDto> GetListingsByFilter(ListingFilterObject filter);
        List<ListingDto> GetPaginatedListingsWithFilterAndSorting(ListingFilterObject filter, SortingObject sorting, int pageNumber, int pageSize);

        int GetActiveListingCount();
        int GetPassiveListingCount();
    }
}
