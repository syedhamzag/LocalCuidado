﻿using System;
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
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using Microsoft.Extensions.Logging;
using AwesomeCare.Admin.Services.Client;

namespace AwesomeCare.Admin.Controllers
{
    public class RoteringController : BaseController
    {
        IClientRotaTypeService _clientRotaTypeService;
        IClientRotaNameService _clientRotaNameService;
        IRotaTaskService _rotaTaskService;
        IRotaDayofWeekService _rotaDayOfWeekService;
        IClientRotaService _clientRotaService;
        IClientService _clientServices;
        private readonly ILogger<RoteringController> logger;

        public RoteringController(ILogger<RoteringController> logger,
            IClientRotaService clientRotaService, IFileUpload fileUpload, IRotaDayofWeekService rotaDayOfWeekService, IRotaTaskService rotaTaskService, IClientRotaTypeService clientRotaTypeService, 
            IClientRotaNameService clientRotaNameService, IClientService clientServices) : base(fileUpload)
        {
            _clientRotaTypeService = clientRotaTypeService;
            _clientRotaNameService = clientRotaNameService;
            _rotaTaskService = rotaTaskService;
            _rotaDayOfWeekService = rotaDayOfWeekService;
            _clientRotaService = clientRotaService;
            this.logger = logger;
            _clientServices = clientServices;
        }
        public async Task<IActionResult> Index(int clientId)
        {
            RoteringViewModel model = new RoteringViewModel();
            model.ClientId = clientId;
            var rotaTypes = await _clientRotaTypeService.Get();
            var rotas = await _clientRotaNameService.Get();
            var rotaTasks = await _rotaTaskService.Get();
            var weekDays = await _rotaDayOfWeekService.Get();

            var clientRotas = await _clientRotaService.GetForEdit(clientId);

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
        public async Task<IActionResult> Index(int clientId, int pin, int pinId)
        {
            var getmodal = await _clientRotaService.GetPin(pinId);
            if (pin != getmodal.Pin)
                return RedirectToAction("HomeCare", "Client"); 
            return RedirectToAction("Index", new { clientId = clientId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePin(int newPin, int oldPin, int pinId)
        {
            var getmodal = await _clientRotaService.GetPin(pinId);
            if (getmodal.Pin != oldPin)
                return RedirectToAction("HomeCare", "Client");
            var model = new PostRotaPin();
            model.PinId = getmodal.PinId;
            model.Pin = newPin;
            model.Key = getmodal.Key;
            var result = await _clientRotaService.ChangePin(model);

            return RedirectToAction("BaseRecord", "Admin");
        }
        public IActionResult AddPin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPin(PostRotaPin model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _clientRotaService.AddPin(model);

            return RedirectToAction("BaseRecord", "Admin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _Index(RoteringViewModel model, IFormCollection formsCollection)
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
                            rotaDay.WeekDay = weekDay.DayofWeek;
                            rotaDay.ClientId = clientRota.ClientId;
                            rotaDay.ClientRotaTypeId = clientRota.ClientRotaTypeId;

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
            return RedirectToAction("Index", new { clientId = model.ClientId });
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
        public async Task<IActionResult> LiveRota(string startDate, string stopDate)
        {
            LiveRota rota = await GetLiveRota(startDate, stopDate);
            return View(rota);
        }
        public IActionResult OfficeAttendance()
        {
            LiveRota rota = new LiveRota();
            return View(rota);
        }
        [HttpPost]
        public async Task<IActionResult> OfficeAttendance(string startDate, string stopDate)
        {
            LiveRota rota = await GetLiveRota(startDate, stopDate);
            return View(rota);
        }
        public async Task<LiveRota> GetLiveRota(string startDate, string stopDate)
        {
            var liveRotaViewModel = new LiveRota();
            var sdate = string.IsNullOrWhiteSpace(startDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : startDate;
            var edate = string.IsNullOrWhiteSpace(stopDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : stopDate;
            var rotaAdmin = await _rotaTaskService.LiveRota(sdate, edate);

            var clockDifferences = new List<LiveRotaClockDifference>();

            var groupedByPeriod = (from rt in rotaAdmin
                                   group rt by rt.Period into rtGrp
                                   select new
                                   {
                                       Period = rtGrp.Key,
                                       Trackers = rtGrp.ToList()
                                   }).ToList();

            foreach (var grp in groupedByPeriod)
            {
                var totalClockDifference = CalculateTotalClockDifference(grp.Trackers);
                clockDifferences.Add(new LiveRotaClockDifference
                {
                    Period = grp.Period,
                    TotalClockDifference = totalClockDifference
                });
            }

            List<GroupLiveRota> groupedRota = null;
            var todaysDate = DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd");
            var currentTime = DateTimeOffset.UtcNow.DateTime.ToPortalDateTime().TimeOfDay;
            groupedRota = (from rt in rotaAdmin
                           group rt by rt.Staff into rtGrp
                           select new GroupLiveRota
                           {
                               StaffName = rtGrp.Key,
                               Trackers = rtGrp.Where(t => TimeSpan.ParseExact(t.StartTime, "h\\:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.TimeSpanStyles.None) <= currentTime).OrderBy(t => TimeSpan.ParseExact(t.StartTime, "h\\:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.TimeSpanStyles.None)).ToList()

                           }).ToList();

            liveRotaViewModel.GroupLiveRotas = groupedRota;
            liveRotaViewModel.ClockDifferences = clockDifferences;



            return liveRotaViewModel;
        }

        [HttpGet]
        public async Task<IActionResult> RotaReport(string startDate, string stopDate)
        {
            var liveRotaViewModel = new LiveRota();
            // var date =DateTime.Now.ToString("yyyy-MM-dd");
            var sdate = string.IsNullOrWhiteSpace(startDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : startDate;
            var edate = string.IsNullOrWhiteSpace(stopDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : stopDate;
            var rotaAdmin = await _rotaTaskService.LiveRota(sdate, edate);

            var clockDifferences = new List<LiveRotaClockDifference>();

            var groupedByPeriod = (from rt in rotaAdmin
                                   group rt by rt.Period into rtGrp
                                   select new
                                   {
                                       Period = rtGrp.Key,
                                       Trackers = rtGrp.ToList()
                                   }).ToList();

            foreach (var grp in groupedByPeriod)
            {
                var totalClockDifference = CalculateTotalClockDifference(grp.Trackers);
                clockDifferences.Add(new LiveRotaClockDifference
                {
                    Period = grp.Period,
                    TotalClockDifference = totalClockDifference
                });
            }


            var groupedRota = (from rt in rotaAdmin
                               group rt by rt.Staff into rtGrp
                               orderby rtGrp.Key
                               select new GroupLiveRota
                               {
                                   StaffName = rtGrp.Key,
                                   Trackers = rtGrp.OrderBy(t => TimeSpan.ParseExact(t.StartTime, "h\\:mm", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.TimeSpanStyles.None)).ToList()

                               }).ToList();

            liveRotaViewModel.GroupLiveRotas = groupedRota;
            liveRotaViewModel.ClockDifferences = clockDifferences;

            return View(liveRotaViewModel);
        }

        [HttpPost]
        public IActionResult RotaReport(IFormCollection formCollection)
        {
            string startDate = formCollection["startDate"];
            string stopDate = formCollection["stopDate"];
            var sdate = string.IsNullOrWhiteSpace(startDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : startDate;
            var edate = string.IsNullOrWhiteSpace(stopDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : stopDate;
            // var date = DateTime.Now.ToString("yyyy-MM-dd");
            //var rotaAdmin = await _rotaTaskService.LiveRota(date);

            return RedirectToActionPermanent("RotaReport", new { startDate = sdate, stopDate = edate });
            // return View(rotaAdmin);
        }


        TimeSpan CalculateTotalClockDifference(List<LiveTracker> Trackers)
        {
            TimeSpan totalTime = new TimeSpan(0, 0, 0);
            try
            {

                foreach (var rota in Trackers)
                {
                    if (rota.ClockInTime.HasValue && rota.ClockOutTime.HasValue)
                    {
                        bool isClockInTimeValid = TimeSpan.TryParseExact(rota.ClockInTime.Value.ToString("hh:mm"), "hh\\:mm", CultureInfo.GetCultureInfo("en-US"), TimeSpanStyles.None, out TimeSpan clockIn);
                        bool isClockOutTimeValid = TimeSpan.TryParseExact(rota.ClockOutTime.Value.ToString("hh:mm"), "hh\\:mm", CultureInfo.GetCultureInfo("en-US"), TimeSpanStyles.None, out TimeSpan clockOut);
                        if (isClockInTimeValid && isClockOutTimeValid)
                        {
                            var clockDiff = clockOut.Subtract(clockIn);
                            totalTime = totalTime.Add(clockDiff);
                        }
                    }
                }

                return totalTime;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return totalTime;
            }
        }

        [HttpPost]
        public IActionResult LiveRota(IFormCollection formCollection)
        {
            string startDate = formCollection["startDate"];
            string stopDate = formCollection["stopDate"];
            var sdate = string.IsNullOrWhiteSpace(startDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : startDate;
            var edate = string.IsNullOrWhiteSpace(stopDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : stopDate;
            // var date = DateTime.Now.ToString("yyyy-MM-dd");
            //var rotaAdmin = await _rotaTaskService.LiveRota(date);

            return RedirectToActionPermanent("LiveRota", new { startDate = sdate, stopDate = edate });
            // return View(rotaAdmin);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteRota(IFormCollection formCollection, string deleteId,string redirectAction)
        {
            //string id = formCollection["deleteId"];
            int staffRotaId = int.TryParse(deleteId, out int rtId) ? rtId : 0;

            var result = await _rotaTaskService.DeleteStaffRotaPeriod(staffRotaId);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Rota successfully deleted" : content });
            return RedirectToActionPermanent(redirectAction);
        }

        [HttpGet("LiveRota/Edit", Name = "LiveRotaEdit")]
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
        [HttpPost]
        public async Task<IActionResult> TrackerReport(string startDate, string stopDate)
        {
            var rotaList = new List<TrackerReport>();
            var rotaAdmin = await _rotaTaskService.LiveRota(startDate, stopDate);


             foreach (var day in rotaAdmin.OrderBy(s => s.RotaDate).GroupBy(d => d.RotaDate).ToList())
            {
                TimeSpan hours = default;
                int missed = 0;
                int late = 0;
                var live = new TrackerReport();

                    foreach (var r in day.ToList())
                    {
                        live.RotaDate = r.RotaDate;
                        live.Staff = r.Staff;
                        live.Rota = r.Rota;
                        live.Remark = r.Remark;
                        bool isClockInTimeValid = false;
                        bool isClockOutTimeValid = false;
                        TimeSpan clockIn = default;
                        TimeSpan clockOut = default;
                        if (r.ClockInTime.HasValue && r.ClockOutTime.HasValue)
                        {
                            if (string.IsNullOrEmpty(live.StartTime))
                                live.StartTime = r.ClockInTime.Value.AddHours(1).ToString("hh:mm");
                            live.StopTime = r.ClockOutTime.Value.AddHours(1).ToString("hh:mm");
                            isClockInTimeValid = TimeSpan.TryParseExact(r.ClockInTime.Value.AddHours(1).ToString("hh:mm"), "hh\\:mm", CultureInfo.GetCultureInfo("en-US"), TimeSpanStyles.None, out clockIn);
                            isClockOutTimeValid = TimeSpan.TryParseExact(r.ClockOutTime.Value.AddHours(1).ToString("hh:mm"), "hh\\:mm", CultureInfo.GetCultureInfo("en-US"), TimeSpanStyles.None, out clockOut);
                            if (isClockInTimeValid && isClockOutTimeValid)
                            {
                                var clockDiff = clockOut.Subtract(clockIn);

                                hours += clockDiff;
                            }
                        }
                        if (r.ClockInTime.HasValue)
                        {
                            var st = TimeSpan.TryParseExact(r.StartTime, "h\\:mm", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan d) ? d : default(TimeSpan);
                            var ct = TimeSpan.TryParseExact(r.ClockInTime.Value.AddHours(1).DateTime.TimeOfDay.ToString(), "hh\\:mm\\:ss", CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan c) ? c : default(TimeSpan);
                            var df = st.Subtract(ct).TotalMinutes;
                            if (df <= 15 && df >= -15)
                            {
                            }
                            else if (df > 15 && df <= 30)
                            {
                            }
                            else if (df >= -30)
                            {

                            }
                            else
                            {
                                late += 1;
                            }
                        }
                        else
                        {
                            missed += 1;
                        }
                    }

                live.Hours = Math.Round(hours.TotalHours,0);
                live.Missed = missed;
                live.Late = late;
                rotaList.Add(live);

            }
            return View(rotaList);
        }

        public IActionResult TrackerReport()
        {
            return View();
        }

        public async Task<IActionResult> Invoice()
        {
            var client = await _clientServices.GetClients();
            var model = new ClientInvoice();
            model.Clients = client.ToList();
            model.ClientList = client.Select(s => new SelectListItem(s.Firstname + " " + s.Surname, s.ClientId.ToString())).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Invoice(ClientInvoice model)
        {
            var client = await _clientServices.GetClients();
            model.Clients = client.ToList();
            model.ClientList = client.Select(s => new SelectListItem(s.Firstname + " " + s.Surname, s.ClientId.ToString())).ToList();
            var sdate = string.IsNullOrWhiteSpace(model.startDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : model.startDate;
            var edate = string.IsNullOrWhiteSpace(model.stopDate) ? DateTime.UtcNow.ToPortalDateTime().ToString("yyyy-MM-dd") : model.stopDate;
            model.Invoices = await _rotaTaskService.LiveRota(sdate, edate);
            return View(model);
        }

    }
}