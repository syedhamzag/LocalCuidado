using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : BaseController
    {
        public IActionResult Register()
        {
            var kk = new SampleModel();
            return View(kk);
        }
        [HttpPost]
        public IActionResult Register(SampleModel model)
        {
           
            return View(model);
        }

        public IActionResult HomeCare()
        {
            var client = new CreateClient();
           
            return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HomeCare(CreateClient model)
        {
            if(model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            return View();
        }

        public IActionResult Test()
        {
            var client = new CreateClient();
            client.AreaCodeId = 4;
            return View(client);
        }
    }
}