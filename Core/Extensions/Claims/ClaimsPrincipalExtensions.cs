using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }

        public static int ClaimUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal?.Claims(ClaimTypes.NameIdentifier);
            return Convert.ToInt32(userId[0]);
        }
    }
}
