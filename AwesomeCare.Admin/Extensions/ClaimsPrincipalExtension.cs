using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtension
    {
        /// <summary>
        /// Application UserId i.e sub claim in Identity Server
        /// </summary>
        /// <returns></returns>
        public static string  SubClaim(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            var sub = claimsPrincipal.FindFirst("sub")?.Value;
            return sub;
        }
    }
}
