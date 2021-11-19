using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffInterviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
