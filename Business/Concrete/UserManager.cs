﻿using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {

            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<User> GetByMail(string mail)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == mail));
        }

        public IDataResult<UserDetailDto> GetUserDetailsById(int id)
        {
            return new SuccessDataResult<UserDetailDto>(_userDal.GetUserDetail(id));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IDataResult<int> GetUserCount()
        {
            return new SuccessDataResult<int>(_userDal.GetAll().Count);
        }

        public IDataResult<int> GetActiveUserCount()
        {
            return new SuccessDataResult<int>(_userDal.GetAll().Where(u=> u.Status == true).Count());
        }

        public IDataResult<int> GetPassiveUserCount()
        {
            return new SuccessDataResult<int>(_userDal.GetAll().Where(u=>u.Status == false).Count());
        }

        public IDataResult<List<UserDetailDto>> GetLatestUsers(int userCount)
        {
            return new SuccessDataResult<List<UserDetailDto>>(_userDal.GetLatestUsers(userCount),Messages.UsersListed);
        }

        public IDataResult<bool> GetUserStatus(int userId)
        {
            return new SuccessDataResult<bool>(_userDal.Get(u => u.Id == userId).Status);
        }

        public IDataResult<List<UserDetailDto>> GetPaginatedUsers(UserFilterObject? filterObject, SortingObject? sortingObject, int pageNumber, int pageSize)
        {
            return new SuccessDataResult<List<UserDetailDto>>(_userDal.GetPaginatedUsers(filterObject,sortingObject,pageNumber,pageSize), Messages.UsersListed);
        }

        [SecuredOperation("admin", false)]
        public IResult SetUserActive(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);
            if (user != null)
            {
                if(user.Status == false)
                {
                    user.Status = true;
                    _userDal.Update(user);
                    return new SuccessResult(Messages.UserUpdated);
                }
                else
                {
                    return new ErrorResult(Messages.UserAlreadyActive);
                }

            }
            return new ErrorResult(Messages.UserNotFound);
        }

        [SecuredOperation("admin",false)]
        public IResult SetUserInactive(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);
            if (user != null)
            {
                if (user.Status == true)
                {
                    user.Status = false;
                    _userDal.Update(user);
                    return new SuccessResult(Messages.UserUpdated);
                }
                else
                {
                    return new ErrorResult(Messages.UserAlreadyInactive);
                }

            }
            return new ErrorResult(Messages.UserNotFound);
        }
    }
}
