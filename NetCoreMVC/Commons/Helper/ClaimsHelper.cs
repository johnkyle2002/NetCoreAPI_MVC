using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreMVC.Commons.Helper
{
    public static class ClaimsHelper
    {
        public static string GetJwtToken(ClaimsPrincipal claims)
        {
            return claims.FindFirstValue("jwt");
        }
        public static string GetUserID(ClaimsPrincipal claims)
        {
            return claims.FindFirstValue("userID");
        }
    }
}
