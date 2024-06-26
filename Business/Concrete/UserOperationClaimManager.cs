﻿using Business.Abstract;
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
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal _userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }
        public IResult Add(UserOperationClaim claim)
        {
            _userOperationClaimDal.Add(claim);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        public IResult Delete(UserOperationClaim claim)
        {
            _userOperationClaimDal.Delete(claim);
            return new SuccessResult(Messages.UserOperationClaimDeleted);
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll(),Messages.UserOperationClaimListed);
        }

        public IDataResult<List<UserOperationClaim>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll(uoc=> uoc.UserId==userId),Messages.UserOperationClaimListed);
        }

        public IResult Update(UserOperationClaim claim)
        {
            _userOperationClaimDal.Update(claim);
            return new SuccessResult(Messages.UserOperationClaimUpdated);
        }
    }
}
