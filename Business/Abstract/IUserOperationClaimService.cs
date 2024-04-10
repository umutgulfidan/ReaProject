using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<UserOperationClaim>> GetAll();
        IDataResult<List<UserOperationClaim>> GetByUserId(int userId);
        IResult Add(UserOperationClaim claim);
        IResult Delete(UserOperationClaim claim);
        IResult Update(UserOperationClaim claim);

    }
}
