using Business.Dtos.Requests;
using Business.Dtos.Requests.LandListingReq;
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
    public interface ILandListingService
    {
        IDataResult<List<LandListingDto>> GetLandListings();
        IDataResult<LandListingDetailDto> GetLandListingDetail(int listingId);
        IDataResult<List<LandListingDto>> GetAllByFilter(LandListingFilterObject filter);
        IDataResult<LandListing> Add(CreateLandListingReq req);
        IResult Delete(DeleteLandListingReq req);
        IResult Update(UpdateLandListingReq req);
        IDataResult<List<LandListing>> GetAll();
        IDataResult<LandListing> GetById(int id);
        IDataResult<List<LandListingDto>> GetPaginatedListingsWithFilterAndSorting(LandListingFilterObject filter, SortingObject sorting, int pageNumber, int pageSize);

        IDataResult<int> GetActiveLandListingCount();
        IDataResult<int> GetPassiveLandListingCount();
    }
}
