using Business.Abstract;
using Business.Constants;
using Business.Dtos.Requests.UserImageReq;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserImageManager : IUserImageService
    {
        IUserImageDal _userImageDal;
        IFileHelper _fileHelper;
        public UserImageManager(IUserImageDal userImageDal,IFileHelper fileHelper)
        {
            _userImageDal = userImageDal;
            _fileHelper = fileHelper;
        }

        public IResult Add(IFormFile file, CreateUserImageReq req)
        {
            UserImage userImage = new UserImage();
            userImage.UserId = req.UserId;
            userImage.Status = req.Status;
            userImage.Date = req.Date;

            userImage.ImagePath = _fileHelper.Upload(file,PathConstants.UserImagePath);

            _userImageDal.Add(userImage);
            return new SuccessResult(Messages.UserImageAdded);
        }
        public IResult Delete(DeleteUserImageReq req) 
        {
            UserImage userImage = _userImageDal.Get(ui=> ui.Id==req.Id);

            _fileHelper.Delete(PathConstants.UserImagePath+userImage.ImagePath);
            _userImageDal.Delete(userImage);

            return new SuccessResult(Messages.UserImageDeleted);
        }

        public IDataResult<List<UserImage>> GetAll()
        {
            return new SuccessDataResult<List<UserImage>>(_userImageDal.GetAll(ui=>ui.Status==true),Messages.UserImageListed);
        }

        public IDataResult<List<UserImage>> GetAllByUserId(int userId)
        {
            return new SuccessDataResult<List<UserImage>>(_userImageDal.GetAll(ui=>ui.UserId==userId&&ui.Status==true),Messages.UserImageListed);
        }

        public IResult Update(IFormFile formFile, UserImage userImage)
        {
            userImage.ImagePath = _fileHelper.Update(formFile,PathConstants.UserImagePath+userImage.ImagePath,PathConstants.UserImagePath);
            _userImageDal.Update(userImage);
            return new SuccessResult(Messages.UserImageUpdated);

        }
    }
}
