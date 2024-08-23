using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IListingComplaintReasonService
    {
        IDataResult<List<ListingComplaintReason>> GetAll();
        IResult Add(ListingComplaintReason listingComplaintReason);
        IResult Delete(ListingComplaintReason listingComplaintReason);
        IResult Update(ListingComplaintReason listingComplaintReason);
    }


}
