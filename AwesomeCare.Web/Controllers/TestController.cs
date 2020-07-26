using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using AwesomeCare.Web.AppSettings;
using System.Threading.Tasks;
using AwesomeCare.Web.Services.Admin;
using AwesomeCare.Services.Services;

namespace AwesomeCare.Web.Controllers
{
    [AllowAnonymous]
    public class TestController : BaseController
    {
        private readonly IHttpContextAccessor _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TestController> logger;
        private readonly IBaseRecordService baseRecordService;

        public TestController(IHttpContextAccessor context, IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ILogger<TestController> logger,
            IBaseRecordService baseRecordService, IFileUpload fileUpload):base(fileUpload)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            this.logger = logger;
            this.baseRecordService = baseRecordService;
        }
        public async Task Index()
        {

            var records = await this.baseRecordService.GetBaseRecordsWithItems();

              var auth = await HttpContext.AuthenticateAsync("OpenIdConnect");

            if (auth.Succeeded)
            {
                var refresh_Token = auth.Ticket.Properties.GetTokenValue("refresh_token");
                var kk = auth.Ticket.Properties.Items.FirstOrDefault(k => k.Key == ".Token.refresh_token");
            }
            else
            {

                var idpClient = _httpClientFactory.CreateClient("IdpClient");
                //get the discovery document
                var discoveryResponse = await idpClient.GetDiscoveryDocumentAsync();
                var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
                var token = await _context.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                logger.LogInformation(refreshToken);
                if (string.IsNullOrEmpty(refreshToken))
                {
                    await HttpContext.ChallengeAsync(Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });

                }
                else
                {
                    var clientSettings = _configuration.GetSection("IDPClientSettings").Get<IDPClientSettings>();
                    var refreshResponse = await idpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
                    {
                        Address = discoveryResponse.TokenEndpoint,
                        ClientId = clientSettings.ClientId,
                        ClientSecret = clientSettings.ClientSecret,
                        RefreshToken = refreshToken
                    });
                }
            }
        }
    }
}
