using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<List<OperationClaim>> GetAll();
        IDataResult<OperationClaim> GetById(int id);
        IResult Add(OperationClaim claim);
        IResult Delete(OperationClaim claim);
        IResult Update(OperationClaim claim);
    }
}
