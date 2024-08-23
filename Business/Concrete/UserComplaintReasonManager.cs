using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserComplaintReasonManager : IUserComplaintReasonService
    {
        IUserComplaintReasonDal _userComplaintReasonDal;
        public UserComplaintReasonManager(IUserComplaintReasonDal userComplaintReasonDal)
        {
            _userComplaintReasonDal = userComplaintReasonDal;
        }
        public IResult Add(UserComplaintReason userComplaintReason)
        {
            _userComplaintReasonDal.Add(userComplaintReason);
            return new SuccessResult(Messages.UserComplaintReasonAdded);
        }

        public IResult Delete(UserComplaintReason userComplaintReason)
        {
            _userComplaintReasonDal.Delete(userComplaintReason);
            return new SuccessResult(Messages.UserComplaintReasonDeleted);
        }

        public IDataResult<List<UserComplaintReason>> GetAll()
        {
            return new SuccessDataResult<List<UserComplaintReason>>(_userComplaintReasonDal.GetAll(),Messages.UserComplaintReasonListed);
        }

        public IResult Update(UserComplaintReason userComplaintReason)
        {
            _userComplaintReasonDal.Update(userComplaintReason);
            return new SuccessResult(Messages.UserComplaintReasonUpdated);
        }
    }
}
