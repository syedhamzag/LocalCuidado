using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace AwesomeCare.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor context;

        public AccountController(IHttpContextAccessor context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.context = context;
        }
        public async Task<IActionResult> Logout()
        {
            var idpClient = httpClientFactory.CreateClient("IdpClient");
            //get the discovery document
            var discoveryDoc = await idpClient.GetDiscoveryDocumentAsync();

            var idtoken = await context.HttpContext.GetTokenAsync("id_token");

            if (!discoveryDoc.IsError)
            {
                var url = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value; 
                var requestUrl = new RequestUrl(discoveryDoc.EndSessionEndpoint);
                var endSessionUrl = requestUrl.CreateEndSessionUrl(idtoken, url);
                return Redirect(endSessionUrl);
            }
            return RedirectToAction("Error", "Home");
        }
    }
}
