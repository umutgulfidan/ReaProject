using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserComplaintReasonService
    {
        IDataResult<List<UserComplaintReason>> GetAll();
        IResult Add(UserComplaintReason userComplaintReason);
        IResult Delete(UserComplaintReason userComplaintReason);
        IResult Update(UserComplaintReason userComplaintReason);
    }


}
