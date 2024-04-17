using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }
        public IResult Add(OperationClaim claim)
        {
            _operationClaimDal.Add(claim);
            return new SuccessResult(Messages.OperationClaimAdded);
        }

        public IResult Delete(OperationClaim claim)
        {
            _operationClaimDal.Delete(claim);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll(),Messages.OperationClaimListed);
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(oc=>oc.Id==id),Messages.OperationClaimListed);
        }

        public IResult Update(OperationClaim claim)
        {
            _operationClaimDal.Update(claim);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }
    }
}
