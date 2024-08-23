using Business.Dtos.Requests.UserComplaintReq;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserComplaintService
    {
        IDataResult<List<UserComplaint>> GetAll();
        IResult Add(AddUserComplaintReq addUserComplaintReq,int userId);
        IResult Delete(UserComplaint userComplaint);
        IResult Update(UpdateUserComplaintReq updateUserComplaintReq);
        IResult GetPaginatedUserComplaints(UserComplaintFilterObject? filter,SortingObject? sorting , int pageNumber, int pageSize);
    }


}
