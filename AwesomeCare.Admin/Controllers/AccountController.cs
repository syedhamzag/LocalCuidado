using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor context;
        private readonly ILogger<AccountController> logger;

        public AccountController(IHttpContextAccessor context,IHttpClientFactory httpClientFactory, IConfiguration configuration,ILogger<AccountController> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.context = context;
            this.logger = logger;
        }
        public async Task<IActionResult> SignOut()
        {
            var idpClient = httpClientFactory.CreateClient("IdpClient");
            //get the discovery document
            var discoveryDoc = await idpClient.GetDiscoveryDocumentAsync();

            var idtoken = await context.HttpContext.GetTokenAsync("id_token");

            if (!discoveryDoc.IsError)
            {
                var url = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;// + "/signout-callback-oidc"; //localhost:44372
                var requestUrl = new RequestUrl(discoveryDoc.EndSessionEndpoint);
                var endSessionUrl = requestUrl.CreateEndSessionUrl(idtoken,url);
                logger.LogInformation($"EndSessionUrl {endSessionUrl}");

              await  this.HttpContext.SignOutAsync("Identity.Application");

                return Redirect(endSessionUrl);
            }
            return RedirectToAction("Error","Home");
        }
    }
}
