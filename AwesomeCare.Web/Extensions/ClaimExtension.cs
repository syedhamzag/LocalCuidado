using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Security.Claims
{
    public static class ClaimExtension
    {
        public static string GetClaimValue(this IEnumerable<System.Security.Claims.Claim> claim,string claimType)
        {
            string claimValue = claim.FirstOrDefault(c => c.Type == claimType)?.Value;

            return claimValue;
        }
    }
}
