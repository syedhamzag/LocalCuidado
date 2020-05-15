using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.IdentityServer.Quickstart.Client
{
    public class ClientController : Controller
    {
        public IActionResult Registration()
        {
            return View();
        }
    }
}