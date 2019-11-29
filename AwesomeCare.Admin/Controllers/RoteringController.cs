using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.Admin.ViewModels.Rotering;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Extensions;

namespace AwesomeCare.Admin.Controllers
{
    public class RoteringController : BaseController
    {
        IClientRotaTypeService _clientRotaTypeService;
        IClientRotaNameService _clientRotaService;
        IRotaTaskService _rotaTaskService;
        public RoteringController(IRotaTaskService rotaTaskService, IClientRotaTypeService clientRotaTypeService, IClientRotaNameService clientRotaService)
        {
            _clientRotaTypeService = clientRotaTypeService;
            _clientRotaService = clientRotaService;
            _rotaTaskService = rotaTaskService;
        }
        public async Task<IActionResult> Index()
        {
            RoteringViewModel model = new RoteringViewModel();
            var rotaTypes = await _clientRotaTypeService.Get();
            var rotas = await _clientRotaService.Get();
            var rotaTasks = await _rotaTaskService.Get();
            model.Rotas = rotas.Select(r => new SelectListItem { Text = r.RotaName, Value = r.RotaId.ToString() }).ToList();
            model.RotaTasks = rotaTasks.Select(r => new SelectListItem { Text = r.TaskName, Value = r.RotaTaskId.ToString() }).ToList();
            model.RotaTypes = rotaTypes;
            HttpContext.Session.Set<List<string>>("weekDays", model.WeekDays);
            HttpContext.Session.Set<List<GetClientRotaType>>("rotaTypes", rotaTypes);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormCollection formsCollection)
        {
            List<GetClientRotaType> rotaTypes = HttpContext.Session.Get<List<GetClientRotaType>>("rotaTypes");
            List<string> weekDays = HttpContext.Session.Get<List<string>>("weekDays");

            foreach (var rotaType in rotaTypes)
            {
                var rotatype = formsCollection[rotaType.RotaType];
                if (rotatype.Count > 0 && rotatype[0].ToString().Equals("on", StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (var weekDay in weekDays)
                    {
                        string rotaId = $"{rotaType.RotaType}-{weekDay}-rota";
                        string tastId = $"{rotaType.RotaType}-{weekDay}-rotaTask";
                        string startTimeId = $"{rotaType.RotaType}-{weekDay}-StartTime";
                        string stopTimeId = $"{rotaType.RotaType}-{weekDay}-StopTime";

                        var rota = formsCollection[rotaId];
                        var task = formsCollection[tastId];
                        var startTime = formsCollection[startTimeId];
                        var stopTime = formsCollection[stopTimeId];

                        //Count the number of Task
                        for (int i = 0; i < task.Count; i++)
                        {

                        }

                    }
                }

            }

            return RedirectToAction("Index");
        }
    }
}