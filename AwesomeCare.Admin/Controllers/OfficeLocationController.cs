using AwesomeCare.Admin.Services.OfficeLocation;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class OfficeLocationController : BaseController
    {
        private readonly ILogger<OfficeLocationController> logger;
        private readonly IOfficeLocationService officeLocationService;

        public OfficeLocationController(ILogger<OfficeLocationController> logger,
            IFileUpload fileUpload,
            IOfficeLocationService officeLocationService) :base(fileUpload)
        {
            this.logger = logger;
            this.officeLocationService = officeLocationService;
        }
        public async Task<IActionResult> Index()
        {
            var offices = await officeLocationService.Get();
            return View(offices);
        }

        public IActionResult NewOffice()
        {
            return View();
        }
    }
}
