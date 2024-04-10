using Business.Dtos.Requests.UserImageReq;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserImageService
    {
        IDataResult<List<UserImage>> GetAll();
        IDataResult<List<UserImage>> GetAllByUserId(int userId);
        IResult Add(IFormFile formFile,CreateUserImageReq req);
        IResult Delete(DeleteUserImageReq req);
        IResult Update(IFormFile formFile, UserImage userImage);

    }
}
