using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfRepositoryBase<ReaContext, UserOperationClaim>, IUserOperationClaimDal
    {
        public List<UserOperationClaimDto> GetByUserId(int userId)
        {
            using (ReaContext context = new ReaContext())
            {
                var query = from userOperationClaim in context.UserOperationClaims
                            join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                            where userOperationClaim.UserId == userId
                            select new UserOperationClaimDto
                            {
                                Id = userOperationClaim.Id,
                                OperationClaimId = operationClaim.Id,
                                OperationClaimName = operationClaim.Name,
                                UserId = userOperationClaim.Id
                            };

                return query.ToList();
            }
        }
    }
}
