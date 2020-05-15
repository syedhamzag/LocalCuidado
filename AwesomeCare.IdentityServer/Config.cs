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
                new IdentityResources.Email(),
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
                     
                    AccessTokenLifetime = 60,//i.e 1 mins
                    AllowOfflineAccess = true,//to enable access to the Identity Server using Refresh Token to get a new access Token
                   // RefreshTokenExpiration = TokenExpiration.Sliding,//To get a new Refresh Token after using the previous one i.e if Sliding, Refresh Token lifetime will be renewed
                   UpdateAccessTokenClaimsOnRefresh = true,
                     ClientName = "Awesome Care Web",
                    ClientId = "ce9e033f-d2db-42ca-a5d0-be45bfab34bf",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireConsent=false,
                    Description="Awesome Care Web",
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
                         AllowOfflineAccess = true,
                         RequireConsent=false,
                         UpdateAccessTokenClaimsOnRefresh = true,
                         ClientName = "AwesomeCare Admin",
                        ClientId = "a33ea42e-90c6-43a0-aa1f-198ddf3c95be",
                         Description="Awesome Care Admin",
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
                            IdentityServerConstants.StandardScopes.Profile,
                             "awesomecareapi"
                        },
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        }
                },
            };
    }
}