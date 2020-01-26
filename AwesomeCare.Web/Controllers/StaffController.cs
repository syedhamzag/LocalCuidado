using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Web.ViewModels.Staff;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.Web.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            var model = new CreateStaff();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(CreateStaff model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ensure to re-upload required files");
                return View(model);
            }
            return View();
        }
    }
}