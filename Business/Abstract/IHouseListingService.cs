﻿using Business.Dtos.Requests;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IHouseListingService
    {
        void AddHouseListing(HouseListing houseListing);
        IDataResult<HouseListing> Add(CreateHouseListingReq req);
        IResult Delete(DeleteHouseListingReq req);
        IResult Update(UpdateHouseListingReq req);

        void UpdateHouseListing(HouseListing houseListing);
        IDataResult<HouseListing> GetById(int id);
        IDataResult<List<HouseListing>> GetAll();
        IDataResult<List<HouseListingDto>> GetHouseListingDtos();
        IDataResult<HouseListingDetailDto> GetHouseListingDetail(int listingId);

        IDataResult<List<HouseListingDto>> GetAllByFilter(HouseFilterObject filter);
        IDataResult<List<HouseListingDto>> GetPaginatedListingsWithFilterAndSorting(HouseFilterObject filter, SortingObject sorting, int pageNumber, int pageSize);

        IDataResult<int> GetActiveHouseListingCount();
        IDataResult<int> GetPassiveHouseListingCount();
    }
}
