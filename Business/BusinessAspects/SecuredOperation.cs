using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions.Claims;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        private bool _performUserIdCheck;

        public SecuredOperation(string roles,bool performUserIdCheck)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _performUserIdCheck = performUserIdCheck;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            if (_roles.Any(role => roleClaims.Contains(role)))
            {
                return; // Eğer kullanıcı belirtilen rollerden birine sahipse, işlemi gerçekleştirmesine izin ver
            }


            if (_performUserIdCheck)
            {
                var userId = _httpContextAccessor.HttpContext.User.ClaimUserId();
                if (userId == null || userId == 0)
                {
                    throw new Exception(Messages.AuthorizationDenied);
                }

                if (!IsUserAuthorizedForOperation(invocation, userId))
                {
                    throw new Exception(Messages.AuthorizationDenied);
                }
            }
            else
            {
                throw new Exception(Messages.AuthorizationDenied); // Eğer belirtilen rollerden hiçbiri yoksa ve performUserIdCheck false ise yetkilendirme reddedilir
            }
        }

        private bool IsUserAuthorizedForOperation(IInvocation invocation, int userId)
        {
            foreach (var arg in invocation.Arguments)
            {
                if (arg != null && arg.GetType().GetProperty("UserId") != null)
                {
                    int userIdFromRequest = (int)arg.GetType().GetProperty("UserId").GetValue(arg);
                    if (userIdFromRequest != userId)
                        return false;
                }
            }

            return true;
        }

    }
}
