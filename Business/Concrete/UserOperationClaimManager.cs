using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal _userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        [SecuredOperation("admin",false)]
        public IResult Add(UserOperationClaim claim)
        {
            var isRoleAlreadyExisting = _userOperationClaimDal.GetAll(x=> x.UserId==claim.UserId && x.OperationClaimId==claim.OperationClaimId).Any();
            if (isRoleAlreadyExisting)
            {
                return new ErrorResult(Messages.UserOperationClaimAlreadyExisting);
            }
            _userOperationClaimDal.Add(claim);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        [SecuredOperation("admin", false)]
        public IResult Delete(UserOperationClaim claim)
        {
            _userOperationClaimDal.Delete(claim);
            return new SuccessResult(Messages.UserOperationClaimDeleted);
        }

        [SecuredOperation("admin", false)]
        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll(),Messages.UserOperationClaimListed);
        }

        [SecuredOperation("admin", false)]
        public IDataResult<List<UserOperationClaimDto>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaimDto>>(_userOperationClaimDal.GetByUserId(userId),Messages.UserOperationClaimListed);
        }

        [SecuredOperation("admin", false)]
        public IResult Update(UserOperationClaim claim)
        {
            _userOperationClaimDal.Update(claim);
            return new SuccessResult(Messages.UserOperationClaimUpdated);
        }
    }
}
