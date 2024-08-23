using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ListingComplaintReasonManager : IListingComplaintReasonService
    {
        IListingComplaintReasonDal _listingComplaintReasonDal;
        public ListingComplaintReasonManager(IListingComplaintReasonDal listingComplaintReasonDal)
        {
            _listingComplaintReasonDal = listingComplaintReasonDal;
        }

        public IResult Add(ListingComplaintReason listingComplaintReason)
        {
            _listingComplaintReasonDal.Add(listingComplaintReason);
            return new SuccessResult(Messages.ListingComplaintReasonAdded);
        }

        public IResult Delete(ListingComplaintReason listingComplaintReason)
        {
            _listingComplaintReasonDal.Delete(listingComplaintReason);
            return new SuccessResult(Messages.ListingComplaintReasonDeleted);
        }

        public IDataResult<List<ListingComplaintReason>> GetAll()
        {
            return new SuccessDataResult<List<ListingComplaintReason>>(_listingComplaintReasonDal.GetAll(),Messages.ListingComplaintReasonListed);
        }

        public IResult Update(ListingComplaintReason listingComplaintReason)
        {
            _listingComplaintReasonDal.Update(listingComplaintReason);
            return new SuccessResult(Messages.ListingComplaintReasonUpdated);
        }
    }
}
