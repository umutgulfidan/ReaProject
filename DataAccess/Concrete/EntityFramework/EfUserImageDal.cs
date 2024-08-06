using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserImageDal : EfRepositoryBase<ReaContext, UserImage>, IUserImageDal
    {
        public string GetProfileImagePath(int userId)
        {
            using(var context = new ReaContext())
            {
                var query = from userImage in context.UserImages
                             where userImage.Status == true && userImage.UserId == userId
                             orderby userImage.Date descending
                             select userImage.ImagePath;

                var result = query.FirstOrDefault();

                if (result == null)
                {
                    return Constants.PathConstants.DefaultUserImagePath;
                }
                else
                {
                    return result;
                }
            }
        }
    }
}
