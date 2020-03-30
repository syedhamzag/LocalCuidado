using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;

namespace AwesomeCare.Web
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _context;
        private readonly HttpClient _httpClient;

        public AuthenticatedHttpClientHandler(IHttpContextAccessor context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var accessToken = await _context.HttpContext.GetTokenAsync("access_token");
            request.SetBearerToken(accessToken);
            //var refreshToken = await _context.HttpContext.GetTokenAsync("refresh_token");
            //  request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
