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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.IdentityServer.Quickstart.Client
{
    //[Authorize(Roles =  "SuperAdmin")]
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
            //// var apiResourceEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.ApiResource>();
            ////  var identityResourceEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.IdentityResource>();

            //var model = new IdentityServer4ClientViewModel();
            //model.ClientType = "Web App";//to be changed to a dropdown
            //model.SharedSecret = GetKey(32);
            //model.ProtectedResourcesListItems = _dbContext.ApiResources.Where(r => r.Enabled && !r.NonEditable).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();
            //model.IdentityResourceListItems = _dbContext.IdentityResources.Where(r => r.Enabled && !r.NonEditable).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();
            //return View(model);



            var model = new IdentityServer4ClientViewModel();
            Init(model);

            return View(model);
        }
        [HttpPost]
        public IActionResult Registration(IdentityServer4ClientViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Init(model);
                    return View(model);
                }
                // var clientEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.Client>();
                // var apiResourceEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.ApiResource>();
                // var identityResourceEntity = _dbContext.Set<IdentityServer4.EntityFramework.Entities.IdentityResource>();
                model.ProtectedResourcesListItems = _dbContext.ApiResources.Where(r => r.Enabled && !r.NonEditable).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();
                model.IdentityResourceListItems = _dbContext.IdentityResources.Where(r => r.Enabled && !r.NonEditable).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                string redirectUri = model.Url.EndsWith("/") ? string.Concat(model.Url.Trim(), model.CallbackPath) : string.Concat(model.Url.Trim(), "/", model.CallbackPath);
                string postLogoutRedirectUri = model.Url.EndsWith("/") ? string.Concat(model.Url.Trim(), model.PostlogoutUrl) : string.Concat(model.Url.Trim(), "/", model.PostlogoutUrl); //;// model.CallBackUrl.EndsWith("/") ? string.Concat(model.CallBackUrl.Trim(), "signout-callback-oidc") : string.Concat(model.CallBackUrl.Trim(), "/", "signout-callback-oidc");

                var identityClient = new IdentityServer4.EntityFramework.Entities.Client
                {
                    // AccessTokenLifetime = 3600,//i.e 60 mins
                    AllowOfflineAccess = model.AllowOfflineAccess,//to enable access to the Identity Server using Refresh Token to get a new access Token
                                                                  // RefreshTokenExpiration = TokenExpiration.Sliding,//To get a new Refresh Token after using the previous one i.e if Sliding, Refresh Token lifetime will be renewed
                                                                  // UpdateAccessTokenClaimsOnRefresh = true,
                    ClientName = model.DisplayName,
                    ClientId = model.ClientId,
                    AllowedGrantTypes = new List<IdentityServer4.EntityFramework.Entities.ClientGrantType>()
                    {
                        new IdentityServer4.EntityFramework.Entities.ClientGrantType { GrantType = model.GrantType }

                    },

                    RequirePkce = model.RequirePkce,
                    RequireConsent = model.RequireConsent,
                    Description = model.Description,

                    RedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientRedirectUri> {
                        new IdentityServer4.EntityFramework.Entities.ClientRedirectUri() { RedirectUri = redirectUri }
                    },
                    PostLogoutRedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri>
                    {
                        new IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri() { PostLogoutRedirectUri =postLogoutRedirectUri }
                    },
                    AllowedScopes = model.AllowedScopes.Select(s => new IdentityServer4.EntityFramework.Entities.ClientScope() { Scope = s.ToLower() }).ToList(),

                    //{
                    //    new IdentityServer4.EntityFramework.Entities.ClientCorsOrigin
                    //    {
                    //        Origin = model.CallBackUrl
                    //    }
                    //}).ToList()
                    AllowAccessTokensViaBrowser = model.AllowAccessTokensViaBrowser
                };
                if (!string.IsNullOrEmpty(model.CorsOrigins))
                {
                    identityClient.AllowedCorsOrigins = model.CorsOrigins.Split(",").Select(o => new IdentityServer4.EntityFramework.Entities.ClientCorsOrigin
                    {
                        Origin = o
                    }).ToList();
                }
                if (!model.IsPublicClient)
                {
                    identityClient.RequireClientSecret = true;
                    identityClient.ClientSecrets = new List<IdentityServer4.EntityFramework.Entities.ClientSecret>
                    {
                        new IdentityServer4.EntityFramework.Entities.ClientSecret(){Value=model.SharedSecret.Sha256(),Description=model.Description}
                    };
                }
                else
                {
                    identityClient.RequireClientSecret = false;
                }
                //if (model.ClientType.Equals("SPA.Swagger", StringComparison.InvariantCultureIgnoreCase))
                //{
                //   // identityClient.AllowAccessTokensViaBrowser = true;
                //    identityClient.AllowedGrantTypes.Add(new IdentityServer4.EntityFramework.Entities.ClientGrantType { GrantType = "implicit" });
                //}
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

        void Init(IdentityServer4ClientViewModel model)
        {
            model.SharedSecret = GetKey(32);
            model.ProtectedResourcesListItems = _dbContext.ApiResources.Where(r => r.Enabled).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();
            model.IdentityResourceListItems = _dbContext.IdentityResources.Where(r => r.Enabled ).Select(a => new SelectListItem(a.DisplayName, a.Name)).ToList();

        }

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