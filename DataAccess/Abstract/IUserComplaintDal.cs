using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IUserComplaintDal : IEntityRepository<UserComplaint>
    {
        List<UserComplaintDto> GetPaginatedUserComplaints(UserComplaintFilterObject? filter,SortingObject? sortingObject,int pageNumber,int pageSize);
    }
}
