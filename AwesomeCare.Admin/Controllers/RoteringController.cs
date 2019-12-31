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
using AwesomeCare.Admin.Services.RotaDayofWeek;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaDays;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaTask;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using AwesomeCare.Admin.Services.ClientRota;
using AwesomeCare.Admin.Models;

namespace AwesomeCare.Admin.Controllers
{
    public class RoteringController : BaseController
    {
        IClientRotaTypeService _clientRotaTypeService;
        IClientRotaNameService _clientRotaNameService;
        IRotaTaskService _rotaTaskService;
        IRotaDayofWeekService _rotaDayOfWeekService;
        IClientRotaService _clientRotaService;
        public RoteringController(IClientRotaService clientRotaService, IRotaDayofWeekService rotaDayOfWeekService, IRotaTaskService rotaTaskService, IClientRotaTypeService clientRotaTypeService, IClientRotaNameService clientRotaNameService)
        {
            _clientRotaTypeService = clientRotaTypeService;
            _clientRotaNameService = clientRotaNameService;
            _rotaTaskService = rotaTaskService;
            _rotaDayOfWeekService = rotaDayOfWeekService;
            _clientRotaService = clientRotaService;
        }
        public async Task<IActionResult> Index(int client)
        {
            RoteringViewModel model = new RoteringViewModel();
            model.ClientId = client;
            var rotaTypes = await _clientRotaTypeService.Get();
            var rotas = await _clientRotaNameService.Get();
            var rotaTasks = await _rotaTaskService.Get();
            var weekDays = await _rotaDayOfWeekService.Get();

            model.Rotas = rotas.Select(r => new SelectListItem { Text = r.RotaName, Value = r.RotaId.ToString() }).ToList();
            model.RotaTasks = rotaTasks.Select(r => new SelectListItem { Text = r.TaskName, Value = r.RotaTaskId.ToString() }).ToList();
            model.RotaTypes = rotaTypes;
            model.WeekDays = weekDays;
            HttpContext.Session.Set<List<GetRotaDayofWeek>>("weekDays", model.WeekDays);
            HttpContext.Session.Set<List<GetClientRotaType>>("rotaTypes", rotaTypes);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RoteringViewModel model,IFormCollection formsCollection)
        {
            List<GetClientRotaType> rotaTypes = HttpContext.Session.Get<List<GetClientRotaType>>("rotaTypes");
            List<GetRotaDayofWeek> weekDays = HttpContext.Session.Get<List<GetRotaDayofWeek>>("weekDays");

            var clientRotas = new List<CreateClientRota>();

            foreach (var rotaType in rotaTypes)
            {
                var rotatype = formsCollection[rotaType.RotaType];
                if (rotatype.Count > 0 && rotatype[0].ToString().Equals("on", StringComparison.InvariantCultureIgnoreCase))
                {
                    var clientRota = new CreateClientRota();
                    clientRota.ClientId = model.ClientId;
                    clientRota.ClientRotaTypeId = rotaTypes.FirstOrDefault(r => r.RotaType.Equals(rotaType.RotaType)).ClientRotaTypeId;
                    var rotaDays = new List<CreateClientRotaDays>();
                    foreach (var weekDay in weekDays)
                    {
                        //Weekdays
                        var rotaDay = new CreateClientRotaDays();

                        string rotaId = $"{rotaType.RotaType}-{weekDay.DayofWeek}-rota";
                        string tastId = $"{rotaType.RotaType}-{weekDay.DayofWeek}-rotaTask";
                        string startTimeId = $"{rotaType.RotaType}-{weekDay.DayofWeek}-StartTime";
                        string stopTimeId = $"{rotaType.RotaType}-{weekDay.DayofWeek}-StopTime";
                        string weekDayId = $"{rotaType.RotaType}-{weekDay.DayofWeek}-Day";

                        var rota = formsCollection[rotaId];
                        var task = formsCollection[tastId];
                        var startTime = formsCollection[startTimeId];
                        var stopTime = formsCollection[stopTimeId];
                        var weekday = formsCollection[weekDayId];

                        rotaDay.StartTime = startTime[0].ToString();
                        rotaDay.StopTime = stopTime[0].ToString();
                        rotaDay.RotaDayofWeekId = int.Parse(weekday);
                        //Count the number of Task
                        var tasks = new List<CreateClientRotaTask>();
                        for (int i = 0; i < task.Count; i++)
                        {
                            var newtask = new CreateClientRotaTask();
                            newtask.RotaTaskId = int.Parse(task[i].ToString());
                            rotaDay.RotaTasks.Add(newtask);
                        }
                        clientRota.ClientRotaDays.Add(rotaDay);
                    }
                    clientRotas.Add(clientRota);
                }

            }

            if (clientRotas.Count > 0)
            {
                var result = await _clientRotaService.CreateRota(clientRotas);
                SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New Rota successfully registered" : "An Error Occurred" });
                var content = await result.Content.ReadAsStringAsync();
            }
            return RedirectToAction("Index");
        }
    }
}