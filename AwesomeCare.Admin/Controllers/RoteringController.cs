using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.ClientRota;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.Admin.ViewModels.Rotering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AwesomeCare.Admin.Controllers
{
    public class RoteringController : BaseController
    {
        IClientRotaTypeService _clientRotaTypeService;
        IClientRotaService _clientRotaService;
        IRotaTaskService _rotaTaskService;
        public RoteringController(IRotaTaskService rotaTaskService,IClientRotaTypeService clientRotaTypeService, IClientRotaService clientRotaService)
        {
            _clientRotaTypeService = clientRotaTypeService;
            _clientRotaService = clientRotaService;
            _rotaTaskService = rotaTaskService;
        }
        public async Task<IActionResult> Index()
        {
            RoteringViewModel model =new RoteringViewModel();
            var rotaTypes = await _clientRotaTypeService.Get();
            var rotas = await _clientRotaService.Get();
            var rotaTasks = await _rotaTaskService.Get();
            model.Rotas = rotas.Select(r => new SelectListItem { Text = r.RotaName, Value = r.RotaId.ToString() }).ToList();
            model.RotaTasks = rotaTasks.Select(r => new SelectListItem { Text = r.TaskName, Value = r.RotaTaskId.ToString() }).ToList();
            model.RotaTypes = rotaTypes;
            return View(model);
        }
    }
}