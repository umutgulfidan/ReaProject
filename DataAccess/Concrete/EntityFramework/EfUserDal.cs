using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfRepositoryBase<ReaContext, User>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ReaContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public UserDetailDto GetUserDetail(int id)
        {
            using (var context = new ReaContext())
            {
                var result = from user in context.Users
                             where user.Id == id
                             select new UserDetailDto
                             {
                                 Id = user.Id,
                                 Email = user.Email,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                             };


                return result.First(); // veya result.SingleOrDefault() kullanarak tek bir sonuç alabilirsiniz
            }
        }

    }
}
