using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.IdentityServer.Models;
using AwesomeCare.IdentityServer.ViewModels;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.IdentityServer.Quickstart.Client
{
    // [AllowAnonymous]
    public class ClientController : Controller
    {
        private ConfigurationDbContext _dbContext;
        private ILogger<ClientController> _logger;

        public ClientController(ConfigurationDbContext dbContext, ILogger<ClientController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var clients = _dbContext.Clients.ToList();
            return View(clients);
        }
        public IActionResult Registration()
        {
            // var apiResourceEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.ApiResource>();
            //  var identityResourceEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.IdentityResource>();

            var model = new IdentityServer4ClientViewModel();
            model.ClientType = "Web App";//to be changed to a dropdown
            model.SharedSecret = GetKey(32);
            model.ProtectedResourcesListItems = _dbContext.ApiResources.Where(r => r.Enabled && !r.NonEditable).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();
            model.IdentityResourceListItems = _dbContext.IdentityResources.Where(r => r.Enabled && !r.NonEditable).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Registration(IdentityServer4ClientViewModel model)
        {
            try
            {
                // var clientEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.Client>();
                // var apiResourceEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.ApiResource>();
                // var identityResourceEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.IdentityResource>();
                model.ProtectedResourcesListItems = _dbContext.ApiResources.Where(r => r.Enabled && !r.NonEditable).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();
                model.IdentityResourceListItems = _dbContext.IdentityResources.Where(r => r.Enabled && !r.NonEditable).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                string redirectUri = model.CallBackUrl.EndsWith("/") ? string.Concat(model.CallBackUrl.Trim(), "signin-oidc") : string.Concat(model.CallBackUrl.Trim(), "/", "signin-oidc");
                string postLogoutRedirectUri = model.CallBackUrl.EndsWith("/") ? string.Concat(model.CallBackUrl.Trim(), "signout-callback-oidc") : string.Concat(model.CallBackUrl.Trim(), "/", "signout-callback-oidc");

                var identityClient = new IdentityServer4.EntityFramework.Entities.Client
                {
                    AccessTokenLifetime = 3600,//i.e 60 mins
                    AllowOfflineAccess = true,//to enable access to the Identity Server using Refresh Token to get a new access Token
                                              // RefreshTokenExpiration = TokenExpiration.Sliding,//To get a new Refresh Token after using the previous one i.e if Sliding, Refresh Token lifetime will be renewed
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientName = model.DisplayName,
                    ClientId = model.ClientId,
                    AllowedGrantTypes = new List<IdentityServer4.EntityFramework.Entities.ClientGrantType>()
                    {
                        new IdentityServer4.EntityFramework.Entities.ClientGrantType { GrantType = "authorization_code" }
                    },
                    RequirePkce = true,
                    RequireConsent = false,
                    Description = model.Description,
                    RedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientRedirectUri> {
                        new IdentityServer4.EntityFramework.Entities.ClientRedirectUri() { RedirectUri = redirectUri }
                    },
                    PostLogoutRedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri>
                    {
                        new IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri() { PostLogoutRedirectUri = postLogoutRedirectUri }
                    },
                    AllowedScopes = model.AllowedScopes.Select(s => new IdentityServer4.EntityFramework.Entities.ClientScope() { Scope = s }).ToList(),

                    ClientSecrets = new List<IdentityServer4.EntityFramework.Entities.ClientSecret>
                    {
                        new IdentityServer4.EntityFramework.Entities.ClientSecret(){Value="1234567890".Sha256(),Description="AwesomeCare Web Client Secret"}
                    }
                };

                _dbContext.Clients.Add(identityClient);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "An error occurred");
                _logger.LogError(ex, "Client_Registration");
                return View(model);
            }
        }
        //public IActionResult SampleRegistration()
        //{
        //    var clientEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.Client>();
        //    var clients = clientEntity.ToList();
        //    //  var kk = new IdentityServer4.EntityFramework.Entities.Client();
        //    // var k= new  IdentityServer4.EntityFramework.DbContexts.ConfigurationDbContext();
        //    ViewBag.count = clients.Count();
        //    return View();
        //}


        public string GetKey(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[length];
                rng.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }


        }
    }
}