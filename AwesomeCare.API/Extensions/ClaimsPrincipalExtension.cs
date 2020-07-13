using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
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
        /// <summary>
        /// StaffPersonal Info Id
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static string StaffPersonalInfoId(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            var sub = claimsPrincipal.FindFirst("staffPersonalInfoId")?.Value;
            return sub;
        }
        
        public static string Email(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.FindFirst("email")?.Value;
            return claim;
        }

        public static string IdenityScopes(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.FindFirst("scope")?.Value;
            return claim;
        }
    }
}
