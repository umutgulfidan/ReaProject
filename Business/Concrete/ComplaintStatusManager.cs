using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ComplaintStatusManager : IComplaintStatusService
    {
        IComplaintStatusDal _complaintStatusDal;
        public ComplaintStatusManager(IComplaintStatusDal complaintStatusDal)
        {
            _complaintStatusDal = complaintStatusDal;
        }
        public IResult Add(ComplaintStatus complaintStatus)
        {
            _complaintStatusDal.Add(complaintStatus);
            return new SuccessResult(Messages.ComplaintStatusAdded);
        }

        public IResult Delete(ComplaintStatus complaintStatus)
        {
            _complaintStatusDal.Delete(complaintStatus);
            return new SuccessResult(Messages.ComplaintStatusDeleted);
        }

        public IDataResult<List<ComplaintStatus>> GetAll()
        {
            return new SuccessDataResult<List<ComplaintStatus>>(_complaintStatusDal.GetAll(),Messages.ComplaintStatusListed);
        }

        public IResult Update(ComplaintStatus complaintStatus)
        {
            _complaintStatusDal.Update(complaintStatus);
            return new SuccessResult(Messages.ComplaintStatusUpdated);
        }
    }
}
