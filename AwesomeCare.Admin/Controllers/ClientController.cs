using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : Controller
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
    }
}