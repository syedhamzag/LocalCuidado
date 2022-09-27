using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.IdentityServer
{
    public class PasswordResetTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public PasswordResetTokenProvider(IDataProtectionProvider dataProtectionProvider,
       IOptions<PasswordResetTokenProviderOptions> options,
       ILogger<DataProtectorTokenProvider<TUser>> logger)
                                         : base(dataProtectionProvider, options, logger)
        {

        }
    }

    public class PasswordResetTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public PasswordResetTokenProviderOptions()
        {
            Name = "PasswordResetDataProtectorTokenProvider";
            TokenLifespan = TimeSpan.FromHours(1);
        }
    }
}
