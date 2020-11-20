using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using AwesomeCare.Admin.Extensions;
using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.Admin.Services.ShiftBooking;
using AwesomeCare.Admin.Models;
using AwesomeCare.Services.Services;
using AwesomeCare.Admin.Services.StaffWorkTeam;
using AwesomeCare.DataTransferObject.DTOs.StaffWorkTeam;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking;
using Newtonsoft.Json;
using AwesomeCare.DataTransferObject.DTOs.ShiftBookingBlockedDays;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;

namespace AwesomeCare.Admin.Controllers
{
    // [Route("[Controller]")]
    public class ShiftBookingController : BaseController
    {
        private IStaffService _staffService;
        private ILogger<ShiftBookingController> _logger;
        private IShiftBookingService _shiftBookingService;
        private IStaffWorkTeamService _staffWorkTeamService;
        private IClientRotaNameService _clientRotaNameService;
        private readonly IEmailService emailService;

        public ShiftBookingController(IStaffService staffService,
            IClientRotaNameService clientRotaNameService,
            IStaffWorkTeamService staffWorkTeamService,
            IFileUpload fileUpload, IShiftBookingService shiftBookingService,
            ILogger<ShiftBookingController> logger,
            IEmailService emailService) : base(fileUpload)
        {
            _staffService = staffService;
            _logger = logger;
            _shiftBookingService = shiftBookingService;
            _staffWorkTeamService = staffWorkTeamService;
            _clientRotaNameService = clientRotaNameService;
            this.emailService = emailService;
        }
        public async Task<IActionResult> Index()
        {
            var entities = await _shiftBookingService.Get();

            return View(entities);
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateShiftBooking();
            var staffs = await _staffService.GetStaffs();
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

            var teams = await _staffWorkTeamService.Get();
            model.WorkTeams = teams.Select(s => new SelectListItem(s.WorkTeam, s.StaffWorkTeamId.ToString())).ToList();

            var rotas = await _clientRotaNameService.Get();
            model.Rotas = rotas.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();

            HttpContext.Session.Set<List<GetStaffs>>("staffs", staffs);
            HttpContext.Session.Set<List<GetStaffWorkTeam>>("workteams", teams);
            HttpContext.Session.Set<List<GetClientRotaName>>("rotas", rotas);


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShiftBooking model)
        {
            //ensure to check to if the selected rota does not exit for that month

            model.DriverRequired = string.Equals(model.RequiresDriver, "yes", StringComparison.InvariantCultureIgnoreCase) ? true : false;
            if (!ModelState.IsValid)
            {
                model.Staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs").Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                model.WorkTeams = HttpContext.Session.Get<List<GetStaffWorkTeam>>("workteams").Select(s => new SelectListItem(s.WorkTeam, s.StaffWorkTeamId.ToString())).ToList();
                model.Rotas = HttpContext.Session.Get<List<GetClientRotaName>>("rotas").Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();

                return View(ModelState);
            }

            var postShiftBooking = Mapper.Map<PostShiftBooking>(model);
            var result = await _shiftBookingService.Post(postShiftBooking);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New Shift Booking successfully created" : "An Error Occurred" });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var entity = await _shiftBookingService.Get(id);
            return View(entity);
        }


        [Route("/ShiftBooking/View-Shift", Name = "ViewShift")]
        public async Task<IActionResult> ViewShift(string month)
        {
            var model = new ViewShiftViewModel();
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            model.DaysInMonth = daysInMonth;
            string selectedMonth;
            if (string.IsNullOrEmpty(month))
                selectedMonth = model.Months[DateTime.Now.Month - 1].Text;
            else
                selectedMonth = month;

            model.SelectedMonth = selectedMonth;

            var months = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var monthId = Array.IndexOf(months, selectedMonth) + 1;

            var rotas = await _clientRotaNameService.Get();
            model.Rotas = rotas?.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
            var rotaId = rotas?.FirstOrDefault()?.RotaId;
            HttpContext.Session.Set<List<GetClientRotaName>>("rotas", rotas);

            var staffShiftBookings = await _shiftBookingService.GetStaffShiftBookingsByMonth(monthId, rotaId);
            if (staffShiftBookings != null)
                model.Staffs = staffShiftBookings.Staffs;

            HttpContext.Session.Set<ViewShiftViewModel>("shiftModel", model);
            return View(model);
        }


        [Route("/ShiftBooking/View-Shift", Name = "ViewShift")]
        [HttpPost]
        public async Task<IActionResult> ViewShift(ViewShiftViewModel model)
        {
            List<GetClientRotaName> rotas;
            var months = DateTimeFormatInfo.CurrentInfo.MonthNames;
            var monthId = Array.IndexOf(months, model.SelectedMonth) + 1;
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, monthId);
            model.DaysInMonth = daysInMonth;

            rotas = HttpContext.Session.Get<List<GetClientRotaName>>("rotas");
            if (rotas == null)
            {
                rotas = await _clientRotaNameService.Get();
            }
            model.Rotas = rotas?.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();

            var staffShiftBookings = await _shiftBookingService.GetStaffShiftBookingsByMonth(monthId, model.Rota);
            if (staffShiftBookings != null)
                model.Staffs = staffShiftBookings.Staffs;

            HttpContext.Session.Set<ViewShiftViewModel>("shiftModel", model);
            return View(model);
        }

        public IActionResult DownloadShift()
        {
            var model = HttpContext.Session.Get<ViewShiftViewModel>("shiftModel");
            if (model == null)
                return null;

            (string sheetName, MemoryStream stream) shift = GetShiftForAttachment(model);

            string filename = $"{shift.sheetName}.xlsx";
            var content = shift.stream.ToArray();

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                filename);

        }

        [HttpPost]
        public IActionResult EmailShift(IFormCollection formCollection)
        {
            try
            {
                var model = HttpContext.Session.Get<ViewShiftViewModel>("shiftModel");
                if (model != null)
                {
                    var emailAddressesValue = formCollection["txtEmailAddresses"];
                    var messageValue = formCollection["txtMessage"];
                    var message = messageValue.FirstOrDefault();
                    var emailAddresses = emailAddressesValue.FirstOrDefault()?.Split(",");

                    if (emailAddresses == null || emailAddresses.Count() == 0 || string.IsNullOrEmpty(message))
                    {
                        SetOperationStatus(new OperationStatus { IsSuccessful = false, Message = "all fields are required" });
                        return RedirectToAction("ViewShift");
                    }

                    (string sheetName, MemoryStream stream) shift = GetShiftForAttachment(model);

                    string filename = $"{shift.sheetName}.xlsx";

                    emailService.SendAsync(emailAddresses.ToList(), "Shift Booking", message, shift.stream.ToArray(), filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                SetOperationStatus(new OperationStatus { IsSuccessful = false, Message = "an error occurred" });
            }

            return RedirectToAction("ViewShift");
        }

        public (string, MemoryStream) GetShiftForAttachment(ViewShiftViewModel model)
        {
            var rota = model.Rotas.FirstOrDefault(r => r.Value == model.Rota.ToString());
            string sheetName = $"shifts_{rota?.Text}_{model.SelectedMonth}";


            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add(sheetName);

            workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            //Header
            for (int i = 1; i <= model.WeekDays.Length; i++)
            {
                workSheet.Cells[1, i].Value = model.WeekDays[i - 1];
            }

            //Content/Body
            var totalDays = 7;
            var remainder = model.DaysInMonth % 7;
            var totalRows = remainder > 0 ? (model.DaysInMonth / totalDays) + 1 : model.DaysInMonth / totalDays;
            var start = 1;
            bool isLastDay = false;
            bool isEnded = false;
            int row = 2;
            for (int r = 1; r <= 6; r++)
            {
                for (int d = start; d <= model.DaysInMonth; d++)
                {
                    foreach (string wday in model.WeekDays)
                    {
                        if (d.IsSameDay(wday, model.SelectedMonth))
                        {
                            var dayIndex = Array.IndexOf(model.WeekDays, wday) + 1;
                            var currentDay = d.ToString("D2");

                            if (model.Staffs != null && model.Staffs.Count > 0)
                            {
                                var cell = workSheet.Cells[row, dayIndex];
                                cell.Style.WrapText = true;
                                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                var r1 = cell.RichText.Add(currentDay + "\r\n");
                                r1.Bold = true;

                                foreach (var staff in model.Staffs)
                                {
                                    if (staff.BookedDays.Any(c => c.Day.Equals(d.ToString("D2"))))
                                    {
                                        var staffname = staff.StaffName.Replace(" ", "-");
                                        var driver = staff.IsStaffDriver ? " (D)" : "";
                                        var staffandDriver = staffname + driver;

                                        var r2 = cell.RichText.Add(staffandDriver + "\r\n");
                                        r2.Bold = false;
                                    }
                                }

                            }
                            else
                            {
                                workSheet.Cells[row, dayIndex].Value = currentDay;
                            }


                            if (wday.Equals("Saturday", StringComparison.InvariantCultureIgnoreCase))
                            {
                                isLastDay = true;
                            }
                            else
                            {
                                if (d == model.DaysInMonth)
                                {
                                    isEnded = true;
                                    break;
                                }
                                else
                                {
                                    ++d;
                                }

                            }
                        }

                    }

                    if (isEnded)
                    {
                        break;
                    }
                    else
                    {
                        if (isLastDay)
                        {
                            start = d + 1;
                            break;
                        }
                    }

                }
                ++row;
                if (isEnded)
                {
                    break;
                }
            }

            //AutoFit all Columns
            for (int i = 1; i <= model.WeekDays.Length; i++)
            {
                workSheet.Column(i).AutoFit();
            }
            string filename = $"{sheetName}.xlsx";
            using (var stream = new MemoryStream())
            {
                excel.SaveAs(stream);

                return (sheetName, stream);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> DeleteStaffShift(ViewShiftViewModel model, IFormCollection formCollection)
        {
            var itemsToDelete = new DeleteStaffShiftBookingDay();


            foreach (var item in formCollection)
            {
                if (item.Key.Contains("day_"))
                {
                    if (item.Value.Contains("on"))
                    {
                        itemsToDelete.StaffShiftBookingDayId.Add(int.Parse(item.Key.Split('_')[1]));
                    }

                }
            }

            var result = await _shiftBookingService.DeleteStaffShiftBooking(itemsToDelete);
            var content = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = true, Message = $"{content} items deleted successfully" });
            else
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = "An error occurred" });

            return RedirectToAction("ViewShift", new { month = model.SelectedMonth });
        }
        public async Task<IActionResult> CreateStaffShift()
        {
            var model = new CreateStaffShiftViewModel();
            List<GetStaffs> staffs;

            var currentMonthName = DateTime.Now.ToString("MMMM");
            string selectedMonth = model.Months.FirstOrDefault(m => m.Text == currentMonthName)?.Text;
            model.SelectedMonth = selectedMonth;

            var selectedMonthId = (Array.IndexOf<string>(DateTimeFormatInfo.CurrentInfo.MonthNames, selectedMonth) + 1).ToString("D2");
            model.SelectedMonthId = int.Parse(selectedMonthId);
            var rotas = await _clientRotaNameService.Get();
            model.Rotas = rotas?.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
            var rotaId = rotas?.FirstOrDefault()?.RotaId;
            var shiftBooked = await _shiftBookingService.GetShiftByMonthAndYear(selectedMonthId, DateTime.Now.Year.ToString(), rotaId);
            staffs = await _staffService.GetStaffs();


            HttpContext.Session.Set<List<GetStaffs>>("staffs", staffs);
            HttpContext.Session.Set<List<GetClientRotaName>>("rotas", rotas);

            if (shiftBooked == null)
            {
                var selectedRota = rotas.FirstOrDefault(r => r.RotaId == model.Rota);
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = $"No shift scheduled for the month of {selectedMonth} for Rota {selectedRota?.RotaName}" });
                //staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs");
                if (staffs != null)
                    model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

                return View(model);
            }


            model.ShiftBookingId = shiftBooked.ShiftBookingId;

            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.ShiftBooked = shiftBooked;

            HttpContext.Session.Set("shiftBooked", shiftBooked);
            HttpContext.Session.Set("staffs", staffs);

            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, int.Parse(selectedMonthId));
            model.DaysInMonth = daysInMonth;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchShiftBooking(CreateStaffShiftViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<GetStaffs> staffs;
            var selectedMonthId = (Array.IndexOf<string>(DateTimeFormatInfo.CurrentInfo.MonthNames, model.SelectedMonth) + 1).ToString("D2");
            model.SelectedMonthId = int.Parse(selectedMonthId);
            var shiftBooked = await _shiftBookingService.GetShiftByMonthAndYear(selectedMonthId, DateTime.Now.Year.ToString(), model.Rota);
            var rotas = HttpContext.Session.Get<List<GetClientRotaName>>("rotas");
            model.Rotas = rotas?.Select(s => new SelectListItem(s.RotaName, s.RotaId.ToString())).ToList();
            if (shiftBooked == null)
            {
                var selectedRota = rotas.FirstOrDefault(r => r.RotaId == model.Rota);
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = $"No shift scheduled for the month of {model.SelectedMonth} for Rota {selectedRota?.RotaName}" });
                staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs");

                model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

                return View("CreateStaffShift", model);
            }
            staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs");

            model.ShiftBooked = shiftBooked;
            model.ShiftBookingId = shiftBooked.ShiftBookingId;
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();

            model.Staff = staffs.FirstOrDefault(s => s.StaffPersonalInfoId.ToString() == model.SelectedStaff);
            model.CanUserDrive = model.Staff == null ? false : model.Staff.CanDrive;

            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, int.Parse(selectedMonthId));
            model.DaysInMonth = daysInMonth;

            return View("CreateStaffShift", model);
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateShift(CreateStaffShiftViewModel model, IFormCollection formCollection)
        {
            var staffPersonalInfoId = model.SelectedStaff;

            var staffShiftBooking = new PostStaffShiftBooking()
            {
                ShiftBookingId = model.ShiftBookingId,
                StaffPersonalInfoId = int.Parse(staffPersonalInfoId)
            };
            //PostStaffShiftBookingDay
            for (int i = 1; i <= model.DaysInMonth; i++)
            {
                var dt = i.ToString("D2");
                var selectedDate = formCollection[dt];
                string dayweek = $"{dt}_day";
                var selectedDay = formCollection[dayweek];
                if (selectedDate.Count > 0 && selectedDay.Count > 0)
                {
                    staffShiftBooking.Days.Add(new PostStaffShiftBookingDay
                    {
                        Day = dt,
                        WeekDay = selectedDay.FirstOrDefault()
                    }); ;
                }
            }

            if (staffShiftBooking.Days.Count == 0)
                return RedirectToAction("CreateStaffShift");

            var kk = JsonConvert.SerializeObject(staffShiftBooking);
            var result = await _shiftBookingService.CreateBooking(staffShiftBooking);
            var content = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = true, Message = "Your booking was successful" });
                return RedirectToAction("ViewShift");
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = content });
            }
            else
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = "An error occurred" });
            }
            var shiftBooked = HttpContext.Session.Get<GetShiftBookedByMonthYear>("shiftBooked");
            model.ShiftBooked = shiftBooked;

            var staffs = HttpContext.Session.Get<List<GetStaffs>>("staffs");
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.Staff = staffs.FirstOrDefault(s => s.StaffPersonalInfoId.ToString() == model.SelectedStaff);

            return View("CreateStaffShift", model);
        }

        public IActionResult BlockDays(int month, int bookingId)
        {
            if (month < DateTime.Now.Month)
                return RedirectToAction("Index");

            var model = new CreateShiftBookingBlockedDays();

            var monthArray = DateTimeFormatInfo.CurrentInfo.MonthNames;
            model.SelectedMonth = monthArray[month - 1];
            model.ShiftBookingId = bookingId;

            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, month);
            model.DaysInMonth = daysInMonth;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockDays(CreateShiftBookingBlockedDays model, IFormCollection formCollection)
        {

            var blockedDays = new List<PostShiftBookingBlockedDays>();

            for (int i = 1; i <= model.DaysInMonth; i++)
            {
                var dt = i.ToString("D2");
                var selectedDate = formCollection[dt];
                string dayweek = $"{dt}_day";
                var selectedDay = formCollection[dayweek];
                if (selectedDate.Count > 0 && selectedDay.Count > 0)
                {
                    blockedDays.Add(new PostShiftBookingBlockedDays
                    {
                        ShiftBookingId = model.ShiftBookingId,
                        Day = dt,
                        WeekDay = selectedDay.FirstOrDefault()
                    }); ;
                }
            }

            if (blockedDays.Count == 0)
                return View(model);

            var result = await _shiftBookingService.BlockDays(blockedDays);
            var content = await result.Content.ReadAsStringAsync();


            if (result.IsSuccessStatusCode)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = "Operation Successful" });
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogInformation(content);
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = "An error occurred" });
                return View(model);
            }



        }


        public async Task<IActionResult> DeleteShift(int shiftId)
        {
            var request = await _shiftBookingService.Delete(shiftId);
            // var content = await request.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = request.IsSuccessStatusCode, Message = request.IsSuccessStatusCode ? "Shift successfully deleted" : "Could not delete shift, please try again later" });

            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> CreatShift()
        //{
        //    var model = new CreateStaffShiftViewModel();
        //    string selectedMonth = model.Months[DateTime.Now.Month - 1].Text;
        //    model.SelectedMonth = selectedMonth;

        //    var shiftBooked = await _shiftBookingService.GetShiftByMonthAndYear(selectedMonth, DateTime.Now.Year.ToString());
        //    var staffs = await _staffService.GetStaffs();

        //    model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
        //    model.ShiftBooked = shiftBooked;

        //    HttpContext.Session.Set("shiftBooked", shiftBooked);
        //    HttpContext.Session.Set("staffs", staffs);

        //    var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        //    model.DaysInMonth = daysInMonth;
        //    return View(model);
        //}

    }
}