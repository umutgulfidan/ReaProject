using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserComplaintReasonDal : EfRepositoryBase<ReaContext, UserComplaintReason>, IUserComplaintReasonDal
    {
    }
}
