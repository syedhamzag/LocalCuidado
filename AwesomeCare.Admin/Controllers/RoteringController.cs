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
using AwesomeCare.Services.Services;
using Newtonsoft.Json;
using AwesomeCare.Model.Models;
using AwesomeCare.DataTransferObject.DTOs.StaffRotaPeriod;
using AutoMapper;
using System.Globalization;

namespace AwesomeCare.Admin.Controllers
{
    public class RoteringController : BaseController
    {
        IClientRotaTypeService _clientRotaTypeService;
        IClientRotaNameService _clientRotaNameService;
        IRotaTaskService _rotaTaskService;
        IRotaDayofWeekService _rotaDayOfWeekService;
        IClientRotaService _clientRotaService;
        public RoteringController(IClientRotaService clientRotaService, IFileUpload fileUpload, IRotaDayofWeekService rotaDayOfWeekService, IRotaTaskService rotaTaskService, IClientRotaTypeService clientRotaTypeService, IClientRotaNameService clientRotaNameService) : base(fileUpload)
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

            var clientRotas = await _clientRotaService.GetForEdit(client);

            model.Rotas = rotas.Select(r => new SelectListItem { Text = r.RotaName, Value = r.RotaId.ToString() }).ToList();
            model.RotaTasks = rotaTasks.Select(r => new SelectListItem { Text = r.TaskName, Value = r.RotaTaskId.ToString() }).ToList();
            model.RotaTypes = rotaTypes;
            model.WeekDays = weekDays;
            model.ClientRotas = clientRotas;
            if (clientRotas.Count > 0)
            {
                model.ActionName = "Update";
            }

            HttpContext.Session.Set<List<GetRotaDayofWeek>>("weekDays", model.WeekDays);
            HttpContext.Session.Set<List<GetClientRotaType>>("rotaTypes", rotaTypes);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RoteringViewModel model, IFormCollection formsCollection)
        {

            List<GetClientRotaType> rotaTypes = HttpContext.Session.Get<List<GetClientRotaType>>("rotaTypes");
            List<GetRotaDayofWeek> weekDays = HttpContext.Session.Get<List<GetRotaDayofWeek>>("weekDays");

            var clientRotas = new List<CreateClientRota>();

            foreach (var rotaType in rotaTypes)
            {
                var rotatype = formsCollection[rotaType.RotaType];
                string clientRotaid = $"{rotaType.RotaType}-ClientRotaId";


                if (rotaType != null)
                {
                    var clientrota = formsCollection[clientRotaid];

                    if (rotatype.Count > 0 && rotatype[0].ToString().Equals("on", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var clientRota = new CreateClientRota();
                        clientRota.ClientId = model.ClientId;
                        clientRota.ClientRotaTypeId = rotaTypes.FirstOrDefault(r => r.RotaType.Equals(rotaType.RotaType)).ClientRotaTypeId;
                        clientRota.ClientRotaId = int.TryParse(clientrota, out int clrtId) ? clrtId : 0;
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
                            string clientRotaDay = $"{rotaType.RotaType}-{weekDay.DayofWeek}-DayId";
                            // string clientRotaid = $"{rotaType.RotaType}-{weekDay.DayofWeek}-ClientRotaId";

                            //

                            var rota = formsCollection[rotaId];
                            var task = formsCollection[tastId];
                            var startTime = formsCollection[startTimeId];
                            var stopTime = formsCollection[stopTimeId];
                            var weekday = formsCollection[weekDayId];
                            var clientRotaDayId = formsCollection[clientRotaDay];


                            rotaDay.StartTime = startTime[0].ToString();
                            rotaDay.StopTime = stopTime[0].ToString();
                            rotaDay.RotaDayofWeekId = int.Parse(weekday);
                            rotaDay.RotaId = int.TryParse(rota, out int rtid) ? rtid : 0;
                            rotaDay.ClientRotaDaysId = int.TryParse(clientRotaDayId, out int dayId) ? dayId : 0;
                            // rotaDay.ClientRotaId = int.TryParse(clientRotaid, out int crId) ? crId : 0;
                            //Count the number of Task
                            var tasks = new List<CreateClientRotaTask>();
                            for (int i = 0; i < task.Count; i++)
                            {
                                var newtask = new CreateClientRotaTask();
                                newtask.RotaTaskId = int.Parse(task[i].ToString());
                                newtask.ClientRotaTaskId = 0;
                                rotaDay.RotaTasks.Add(newtask);
                            }
                            clientRota.ClientRotaDays.Add(rotaDay);
                        }
                        clientRotas.Add(clientRota);
                    }

                }

            }

            var kk = JsonConvert.SerializeObject(clientRotas);

            if (clientRotas.Count > 0)
            {
                if (model.ActionName == "Update")
                {
                   
                    var result = await _clientRotaService.EditRota(clientRotas, model.ClientId);
                    SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Rota successfully Updated" : "An Error Occurred" });
                    var content = await result.Content.ReadAsStringAsync();
                }
                else
                {
                    var result = await _clientRotaService.CreateRota(clientRotas);
                    SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New Rota successfully registered" : "An Error Occurred" });
                    var content = await result.Content.ReadAsStringAsync();
                }

            }
            return RedirectToAction("Index", new { client = model.ClientId });
        }
        [HttpGet]
        public async Task<IActionResult> RotaAdmin(string startDate, string stopDate)
        {
            var model = new RotaAdminViewModel();
            model.StartDate = string.IsNullOrEmpty(startDate) ? DateTime.Now.ToString("yyyy-MM-dd") : startDate;
            model.StopDate = string.IsNullOrEmpty(stopDate) ? DateTime.Now.ToString("yyyy-MM-dd") : stopDate;

            var rotaAdmin = await _rotaTaskService.RotaAdmin(model.StartDate, model.StopDate);
            model.RotaAdmin = rotaAdmin;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RotaAdmin(RotaAdminViewModel model)
        {
            var rotaAdmin = await _rotaTaskService.RotaAdmin(model.StartDate, model.StopDate);
            model.RotaAdmin = rotaAdmin;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LiveRota(string searchDate)
        {
           // var date =DateTime.Now.ToString("yyyy-MM-dd");
            var date = string.IsNullOrWhiteSpace(searchDate) ? DateTime.Now.ToString("yyyy-MM-dd") : searchDate;
            var rotaAdmin = await _rotaTaskService.LiveRota(date);

            return View(rotaAdmin);
        }


        [HttpPost]
        public IActionResult LiveRota(IFormCollection formCollection)
        {
            string searchDate = formCollection["searchDate"];
            var date = string.IsNullOrWhiteSpace(searchDate) ? DateTime.Now.ToString("yyyy-MM-dd") : searchDate;
            // var date = DateTime.Now.ToString("yyyy-MM-dd");
            //var rotaAdmin = await _rotaTaskService.LiveRota(date);

            return RedirectToActionPermanent("LiveRota", new { searchDate = date });
           // return View(rotaAdmin);
        }


        [HttpGet("LiveRota/Edit",Name ="LiveRotaEdit")]
        public async Task<IActionResult> EditLiveRota(int staffRotaPeriodId)
        {
            var staffRotaPeriod = await _rotaTaskService.GetStaffRotaPeriod(staffRotaPeriodId);
            var editLiveRota = Mapper.Map<EditStaffRotaPeriod>(staffRotaPeriod);
            return View(editLiveRota);
        }
        [HttpPost("LiveRota/Edit", Name = "LiveRotaEdit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLiveRota(EditStaffRotaPeriod model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var kk = JsonConvert.SerializeObject(model);
            var result = await _rotaTaskService.PatchStaffRotaPeriod(model);
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Live Tracker successfully updated" : "An Error Occurred" });
            if (!result.IsSuccessStatusCode)
                return View(model);

            return RedirectToAction("LiveRota");
        }
    }
}