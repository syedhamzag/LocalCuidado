﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AwesomeCare.Web.AppSettings;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AwesomeCare.Web
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticatedHttpClientHandler> logger;
      
        public AuthenticatedHttpClientHandler(IHttpContextAccessor context, IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ILogger<AuthenticatedHttpClientHandler> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            this.logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

          var accessToken=  await GetAccessToken();// await _context.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            //if (string.IsNullOrEmpty(accessToken))
            //{
            //    return Task.FromResult(HttpResponseMessage);
            //    return new HttpResponseMessage
            //    {
            //        StatusCode = System.Net.HttpStatusCode.OK
            //    };
            //}
            request.SetBearerToken(accessToken);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task<string> GetAccessToken()
        {

            var expiresAt = await _context.HttpContext.GetTokenAsync("expires_at");
            var isExpiredAtValid = DateTimeOffset.TryParse(expiresAt, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset expiresAtDateTimeOffset);
            if (isExpiredAtValid && (expiresAtDateTimeOffset.AddSeconds(-60)).ToUniversalTime() > DateTime.UtcNow)
            {
                return await _context.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
               
            }

            var idpClient = _httpClientFactory.CreateClient("IdpClient");
            //get the discovery document
            var discoveryResponse = await idpClient.GetDiscoveryDocumentAsync();
            var refreshToken = await _context.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            logger.LogInformation($"AuthenticatedHttpClientHandler refresh_token {refreshToken}");
            //if (string.IsNullOrEmpty(refreshToken))
            //{
            //    await _context.HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });

            //}
            //else
            //{
            var clientSettings = _configuration.GetSection("IDPClientSettings").Get<IDPClientSettings>();
            var refreshResponse = await idpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = clientSettings.ClientId,
                ClientSecret = clientSettings.ClientSecret,
                RefreshToken = refreshToken
            });

            //Store Tokens 
            var updateTokens = new List<AuthenticationToken>();
            updateTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.IdToken,
                Value = refreshResponse.IdentityToken
            });
            updateTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = refreshResponse.AccessToken
            });
            updateTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = refreshResponse.RefreshToken
            });
            updateTokens.Add(new AuthenticationToken
            {
                Name = "expires_at",
                Value = (DateTime.UtcNow + TimeSpan.FromSeconds(refreshResponse.ExpiresIn)).ToString("o", CultureInfo.InvariantCulture)
            });

            //get authenticated Principal to update the Cookie
            var currentAuthenticateResult = await _context.HttpContext.AuthenticateAsync("Identity.Application");
            //store the updated tokens
            currentAuthenticateResult.Properties.StoreTokens(updateTokens);
            //sign in
            await _context.HttpContext.SignInAsync("Identity.Application",
                currentAuthenticateResult.Principal,
                currentAuthenticateResult.Properties);

            return refreshResponse.AccessToken;
            // }



        }
    }
}
