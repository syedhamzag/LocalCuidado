using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.Model.Models;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AwesomeCare.IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AwesomeCareDbContext _dbContext;
        public ProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, AwesomeCareDbContext dbContext)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _dbContext = dbContext;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            // Add custom claims in token here based on user properties or any other source
            var staffPersonalInfoEntity = _dbContext.Set<StaffPersonalInfo>();

            var staffPersonalInfo = staffPersonalInfoEntity.FirstOrDefault(u => u.ApplicationUserId == sub);
            bool hasStaffInfo = staffPersonalInfo != null;
            IList<string> roles = await _userManager.GetRolesAsync(user);
            claims.Add(new Claim("hasStaffInfo", hasStaffInfo.ToString()));
            claims.Add(new Claim("staffPersonalInfoId", hasStaffInfo ? staffPersonalInfo.StaffPersonalInfoId.ToString() : ""));
            claims.Add(new Claim(JwtClaimTypes.Role, string.Join(',', roles)));
            claims.Add(new Claim(JwtClaimTypes.Email, user?.Email));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
