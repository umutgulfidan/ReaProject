﻿using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        UserDetailDto GetUserDetail(int id);
        List<UserDetailDto> GetLatestUsers(int userCount);

        List<UserDetailDto> GetPaginatedUsers(UserFilterObject? filterObject, SortingObject? sortingObject, int pageNumber, int pageSize);
    }
}
