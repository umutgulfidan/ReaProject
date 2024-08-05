using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        IDataResult<List<User>> GetAll();
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<UserDetailDto> GetUserDetailsById(int id);
        IDataResult<User> GetByMail(string mail);

        IDataResult<int> GetUserCount();
        IDataResult<int> GetActiveUserCount();
        IDataResult<int> GetPassiveUserCount();
    }
}
