using AwesomeCare.IdentityServer.ViewModels;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.IdentityServer.Quickstart.Resources
{
   // [Authorize(Policy = "SuperAdminPolicy")]
    public class IdentityResourcesController : Controller
    {
        private ConfigurationDbContext _dbContext;
        private ILogger<IdentityResourcesController> _logger;

        public IdentityResourcesController(ConfigurationDbContext dbContext, ILogger<IdentityResourcesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var identityResources = _dbContext.IdentityResources.ToList();
            return View(identityResources);
        }

        [HttpGet("IdentityResource/Add", Name = "AddIdentityResource")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost("IdentityResource/Add", Name = "AddIdentityResource")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IdentityResourceViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var identityResource = new IdentityServer4.EntityFramework.Entities.IdentityResource
                {
                    Description = model.Description,
                    DisplayName = model.DisplayName,
                    Enabled = true,
                    Name = model.Name,
                    Emphasize = true,
                    NonEditable = true,
                    Required = true
                };

                _dbContext.IdentityResources.Add(identityResource);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "An error occurred");
                _logger.LogError(ex, "");
                return View(model);
            }


        }
    }
}
