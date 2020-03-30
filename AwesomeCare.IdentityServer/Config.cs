// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AwesomeCare.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("awesomecareapi", "AwesomeCare API")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                 new Client
                {
                    AccessTokenLifetime = 300,//i.e 5 mins
                    AllowOfflineAccess = true,//to make refresh token expire 
                    ClientName = "Awesome Care Web",
                    ClientId = "awesomecareweb",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireConsent=false,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44362/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>{
                        "https://localhost:44362/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "awesomecareapi"
                    },
                    ClientSecrets =
                    {
                        new Secret("1234567890".Sha256())
                    }
                },
            new Client
                {
                    ClientName = "AwesomeCare Admin",
                ClientId = "mvcapp1",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RedirectUris = new List<string>
                {
                    "https://localhost:44384/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>{
                    "https://localhost:44384/signout-callback-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                }
                },
            };
    }
}