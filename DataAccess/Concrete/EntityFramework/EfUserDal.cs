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
                                  ImagePath = userImage != null ? userImage.ImagePath : string.Empty,
                                  Status = user.Status
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
                                  ImagePath = userImage != null ? userImage.ImagePath : Constants.PathConstants.DefaultUserImagePath,
                                  Status = user.Status
                              }).Take(userCount).ToList();

                return result;
            }
        }

        public List<UserDetailDto> GetPaginatedUsers(UserFilterObject? filterObject, SortingObject? sortingObject, int pageNumber, int pageSize)
        {
            using (var context = new ReaContext())
            {
                var skipAmount = (pageNumber - 1) * pageSize;

                var userImagesQuery = from ui in context.UserImages
                                      where ui.Status == true
                                      orderby ui.Date descending
                                      select ui;

                // Kullanıcıları filtrelemek için başlangıç sorgusu
                var query = from user in context.Users
                            join userImage in userImagesQuery on user.Id equals userImage.UserId into userImageGroup
                            from userImage in userImageGroup.DefaultIfEmpty()
                            select new { user, userImage };

                // Filtreleme
                if (filterObject != null)
                {
                    if (filterObject.UserId.HasValue)
                    {
                        query = query.Where(x => x.user.Id == filterObject.UserId.Value);
                    }
                    if (!string.IsNullOrEmpty(filterObject.Email))
                    {
                        query = query.Where(x => x.user.Email.Contains(filterObject.Email));
                    }
                    if (!string.IsNullOrEmpty(filterObject.FirstName))
                    {
                        query = query.Where(x => x.user.FirstName.Contains(filterObject.FirstName));
                    }
                    if (!string.IsNullOrEmpty(filterObject.LastName))
                    {
                        query = query.Where(x => x.user.LastName.Contains(filterObject.LastName));
                    }
                    if (filterObject.Status.HasValue)
                    {
                        query = query.Where(x => x.user.Status == filterObject.Status.Value);
                    }

                    if (!string.IsNullOrEmpty(filterObject.SearchText))
                    {
                        query = query.Where(u => u.user.Id.ToString() == filterObject.SearchText ||
                                                 u.user.Email.Contains(filterObject.SearchText) ||
                                                 u.user.FirstName.Contains(filterObject.SearchText) ||
                                                 u.user.LastName.Contains(filterObject.SearchText));
                    }
                    if (filterObject.MinRegisterDate.HasValue)
                    {
                        query = query.Where(u => u.user.RegisterDate >= filterObject.MinRegisterDate.Value);
                    }
                    if (filterObject.MaxRegisterDate.HasValue)
                    {
                        query = query.Where(u => u.user.RegisterDate <= filterObject.MaxRegisterDate.Value);
                    }
                    if (filterObject.RoleIds != null && filterObject.RoleIds.Any())
                    {
                        query = from q in query
                                join userOperationClaim in context.UserOperationClaims on q.user.Id equals userOperationClaim.UserId
                                where filterObject.RoleIds.Contains(userOperationClaim.OperationClaimId)
                                select q;
                    }

                }

                // Sıralama
                if (sortingObject != null && !string.IsNullOrEmpty(sortingObject.SortBy))
                {
                    switch (sortingObject.SortBy.ToLower())
                    {
                        case "id":
                            query = sortingObject.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(x => x.user.Id) :
                                query.OrderByDescending(x => x.user.Id);
                            break;
                        case "firstname":
                            query = sortingObject.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(x => x.user.FirstName) :
                                query.OrderByDescending(x => x.user.FirstName);
                            break;
                        case "lastname":
                            query = sortingObject.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(x => x.user.LastName) :
                                query.OrderByDescending(x => x.user.LastName);
                            break;
                        case "email":
                            query = sortingObject.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(x => x.user.Email) :
                                query.OrderByDescending(x => x.user.Email);
                            break;
                        case "registerdate":
                            query = sortingObject.SortDirection == SortDirection.Ascending ?
                                query.OrderBy(x => x.user.RegisterDate) :
                                query.OrderByDescending(x => x.user.RegisterDate);
                            break;
                        default:
                            query = query.OrderBy(x => x.user.Id);
                            break;
                    }
                }
                else
                {
                    query = query.OrderBy(x => x.user.Id);
                }

                // Sonuçları topla ve sayfala
                var result = query
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .Select(x => new UserDetailDto
                    {
                        Id = x.user.Id,
                        FirstName = x.user.FirstName,
                        LastName = x.user.LastName,
                        Email = x.user.Email,
                        RegisterDate = x.user.RegisterDate,
                        ImagePath = x.userImage != null ? x.userImage.ImagePath : Constants.PathConstants.DefaultUserImagePath,
                        Status = x.user.Status,
                        OperationClaims = (from operationClaim in context.OperationClaims
                                           join userOperationClaim in context.UserOperationClaims
                                           on operationClaim.Id equals userOperationClaim.OperationClaimId
                                           where userOperationClaim.UserId == x.user.Id
                                           select new OperationClaim
                                           {
                                               Id = operationClaim.Id,
                                               Name = operationClaim.Name
                                           }).ToList()
                    })
                    .ToList();

                return result;
            }
        }


    }
}
