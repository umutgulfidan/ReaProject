using Business.Dtos.Requests.ListingComplaintReq;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IListingComplaintService
    {
        IDataResult<List<ListingComplaint>> GetAll();
        IResult Add(AddListingComplaintReq addListingComplaintReq,int userId);
        IResult Delete(ListingComplaint listingComplaint);
        IResult Update(UpdateListingComplaintReq updateListingComplaintReq);
        IDataResult<List<ListingComplaintDto>> GetPaginatedListingComplaints(ListingComplaintFilterObject? filter, SortingObject? sorting, int pageNumber, int pageSize);
    }


}
