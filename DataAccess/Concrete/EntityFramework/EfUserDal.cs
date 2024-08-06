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
                var result = (from user in context.Users
                                   join userImages in (
                                       from ui in context.UserImages
                                       where ui.Status == true
                                       orderby ui.Date descending
                                       select ui
                                   ) on user.Id equals userImages.UserId into userImageGroup
                                   from userImage in userImageGroup.DefaultIfEmpty()
                                   where user.Id == id
                                   select new UserDetailDto
                                   {
                                       Id = user.Id,
                                       FirstName = user.FirstName,
                                       LastName = user.LastName,
                                       Email = user.Email,
                                       RegisterDate = user.RegisterDate,
                                       ImagePath = userImage != null ? userImage.ImagePath : string.Empty
                                   }).First();


                return result;
            }
        }

        public List<UserDetailDto> GetLatestUsers(int userCount)
        {
            using (var context = new ReaContext())
            {
                var userImagesQuery = from ui in context.UserImages
                                      where ui.Status == true
                                      orderby ui.Date descending
                                      select ui;

                var result = (from user in context.Users
                              join userImage in userImagesQuery on user.Id equals userImage.UserId into userImageGroup
                              from userImage in userImageGroup.DefaultIfEmpty()
                              orderby user.RegisterDate descending
                              select new UserDetailDto
                              {
                                  Id = user.Id,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  Email = user.Email,
                                  RegisterDate = user.RegisterDate,
                                  ImagePath = userImage != null ? userImage.ImagePath : Constants.PathConstants.DefaultUserImagePath
                              }).Take(userCount).ToList();

                return result;
            }
        }

    }
}
