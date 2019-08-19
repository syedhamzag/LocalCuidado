using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult BaseRecord()
        {
            return View();
        }
    }
}