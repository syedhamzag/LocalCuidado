using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.IdentityServer.ViewModels;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.IdentityServer.Quickstart.Resources
{
    public class ResourcesController : Controller
    {
        private ConfigurationDbContext _dbContext;
        private ILogger<ResourcesController> _logger;

        public ResourcesController(ConfigurationDbContext dbContext, ILogger<ResourcesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult ApiResource()
        {
            var apiResources = _dbContext.ApiResources.ToList();
            return View(apiResources);
        }

        [HttpGet("ApiResource/Add",Name = "AddApiResource")]
        public IActionResult AddApiResource()
        {
            return View();
        }

        [HttpPost("ApiResource/Add", Name = "AddApiResource")]
        public IActionResult AddApiResource(ApiResourceViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var apiResource = new IdentityServer4.EntityFramework.Entities.ApiResource
                {
                    Description = model.Description,
                    DisplayName = model.DisplayName,
                    Enabled = true,
                    Name = model.Name,
                    Scopes = new List<IdentityServer4.EntityFramework.Entities.ApiScope>
                {
                    new IdentityServer4.EntityFramework.Entities.ApiScope
                    {
                        Name = model.Name,
                        DisplayName = model.DisplayName,
                        Description = model.Description,
                        ShowInDiscoveryDocument = true
                    }
                }
                };

                _dbContext.ApiResources.Add(apiResource);
                _dbContext.SaveChanges();
                return RedirectToAction("ApiResource");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "An error occurred");
                _logger.LogError(ex, "Resources_AddApiResource");
                return View(model);
            }

            
        }
    }
}