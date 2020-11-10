using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.Razor
{
    public static class RazorPageBaseExtension
    {
        public static string GetClaimValue(this IEnumerable<System.Security.Claims.Claim> claim, string claimType)
        {
            string claimValue = claim.FirstOrDefault(c => c.Type == claimType)?.Value;

            return claimValue;
        }

        /// <summary>
        /// Application UserId i.e sub claim in Identity Server
        /// </summary>
        /// <returns></returns>
        public static string SubClaim(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            
            var sub = claimsPrincipal.FindFirst("sub")?.Value;
            return sub;
        }
        public static string Sample(this Microsoft.AspNetCore.Mvc.Razor.RazorPageBase user)
        {
            return "";
        }
    }
}
