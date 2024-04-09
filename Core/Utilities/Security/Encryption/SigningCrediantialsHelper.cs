using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
        public class SigningCrediantialsHelper
        {
            public static SigningCredentials CreateSigningCrediantials(SecurityKey securityKey)
            {
                return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            }
        }
   
}
