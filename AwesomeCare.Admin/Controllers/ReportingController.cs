using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.ClientRota;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Services.Services;
using AwesomeCare.Admin.ViewModels.Reporting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AwesomeCare.Admin.Controllers
{
    public class ReportingController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IMemoryCache _cache;
        private readonly IClientRotaService _clientRotaService;

        public ReportingController(IFileUpload fileUpload, IMemoryCache cache, IClientService clientService,
            IClientRotaService clientRotaService) : base(fileUpload)
        {
            _clientService = clientService;
            _cache = cache;
            _clientRotaService = clientRotaService;
        }
        public async Task<IActionResult> EmptyLog()
        {
            var model = new ReportingViewModel();
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmptyLog(int clientId)
        {
            var model = new ReportingViewModel();
            var client = await _clientService.GetClientDetail();
            model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();

            return View(model);
        }
        public IActionResult FilledLog()
        {
            return View();
        }
        public IActionResult EmptyMarChart()
        {
            return View();
        }
        public IActionResult FilledMarChart()
        {
            return View();
        }
    }
}
