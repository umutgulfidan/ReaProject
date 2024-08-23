using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IListingComplaintDal : IEntityRepository<ListingComplaint>
    {
        List<ListingComplaintDto> GetPaginatedListingComplaints(ListingComplaintFilterObject? filter, SortingObject? sortingObject, int pageNumber, int pageSize);
    }
}
