using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.Medication;
using AwesomeCare.Admin.Services.RotaDayofWeek;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffCommunication;
using AwesomeCare.Admin.Services.StaffHoliday;
using AwesomeCare.Admin.Services.StaffWorkTeam;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.Admin.ViewModels.StaffCommunication;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.Staff.InfectionControl;
using AwesomeCare.DataTransferObject.DTOs.StaffCommunication;
using AwesomeCare.DataTransferObject.DTOs.StaffCompetenceTest;
using AwesomeCare.DataTransferObject.DTOs.StaffHealth;
using AwesomeCare.DataTransferObject.DTOs.StaffInterview;
using AwesomeCare.DataTransferObject.DTOs.StaffPersonalityTest;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using AwesomeCare.DataTransferObject.DTOs.StaffRotaMed;
using AwesomeCare.DataTransferObject.DTOs.StaffShadowing;
using AwesomeCare.DataTransferObject.DTOs.User;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{

    public class StaffController : BaseController
    {
        private IStaffService _staffService;
        private ILogger<StaffController> _logger;
        private IFileUpload _fileUpload;
        private IStaffCommunication _staffCommunication;
        private IMedicationService _medicationServices;
        private IClientRotaTypeService _clientRotaTypeService;
        private IClientRotaNameService _clientRotaNameService;
        private IRotaDayofWeekService _rotaDayofWeekService;
        private IClientService _clientService;
        private IStaffWorkTeamService staffWorkTeamService;
        private IBaseRecordService _baseService;
        private readonly IRotaTaskService rotaTaskService;
        const string profilePixFolder = "staffprofilepix";
        const string drivingFolder = "drivinglicense";
        const string rightToFolder = "righttowork";
        const string dbsFolder = "dbsfolder";
        const string niFolder = "nifolder";
        const string selfpyeFolder = "selfpye";
        const string coverLetterFolder = "coverletter";
        const string cvFolder = "cvfolder";
        const string educationFolder = "staffeductaion";
        const string trainingFolder = "stafftraining";
        const string refereeFolder = "staffreferee";
        private ISetupStaffHolidayService _setupStaff;

        public StaffController(IStaffService staffService,
            IClientService clientService, IMedicationService medicationServices,
            IRotaDayofWeekService rotaDayofWeekService,
            IClientRotaNameService clientRotaNameService,
            ILogger<StaffController> logger,
            IFileUpload fileUpload,
            IStaffCommunication staffCommunication,
            IClientRotaTypeService clientRotaTypeService,
            IRotaTaskService rotaTaskService, IBaseRecordService baseService,
            IStaffWorkTeamService staffWorkTeamService, ISetupStaffHolidayService setupStaff) : base(fileUpload)
        {
            _staffService = staffService;
            _logger = logger;
            _fileUpload = fileUpload;
            _staffCommunication = staffCommunication;
            _clientRotaTypeService = clientRotaTypeService;
            _clientRotaNameService = clientRotaNameService;
            _rotaDayofWeekService = rotaDayofWeekService;
            _clientService = clientService;
            this.staffWorkTeamService = staffWorkTeamService;
            this.rotaTaskService = rotaTaskService;
            _baseService = baseService;
            _setupStaff = setupStaff;
            _medicationServices = medicationServices;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var staffs = await _staffService.GetStaffs();
                var approved = staffs.Where(s => s.Status == "Approved").OrderBy(s => s.StaffPersonalInfoId).ToList();

                return View(approved);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Index");
                throw;
            }

        }
        public async Task<IActionResult> StaffRate()
        {
            var entities = await _staffService.GetAsync();
            return View(entities);
        }
        public async Task<IActionResult> OtherStaff()
        {
            try
            {
                var staffs = await _staffService.GetStaffs();
                var approved = staffs.Where(s => s.Status != "Approved").OrderBy(s => s.StaffPersonalInfoId).ToList();

                return View(approved);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Index");
                throw;
            }

        }

        public async Task<IActionResult> Details(int staffId)
        {
            var profile = await _staffService.Profile(staffId);
            foreach (var item in profile.GetStaffPersonalityTest)
            {
                var answer = _baseService.GetBaseRecordItemById(item.Answer).Result.ValueName;
                item.AnswerName = answer;
                var question = _baseService.GetBaseRecordItemById(item.Question).Result.ValueName;
                item.QuestionName = question;
            }
            
            profile.GetStaffPersonalityTest.Where(s => s.AnswerName == "" && s.QuestionName=="");

            profile.Statuses.ForEach(s =>
            {
                if (s.Value == profile.Status.ToString() || s.Text == profile.Status.ToString())
                    s.Selected = true;
            });
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(StaffDetails staff)
        {
            if (!ModelState.IsValid)
            {
                SetOperationStatus(new OperationStatus { IsSuccessful = false, Message = "All fields in the official section are required" });

                return RedirectToAction("Details", new { staffId = staff.StaffPersonalInfoId });
            }

            var postApproval = new PostStaffApproval
            {
                Comment = staff.Comment,
                Rate = staff.Rate,
                StaffPersonalInfoId = staff.StaffPersonalInfoId,
                Status = staff.Status,
                EmploymentDate = staff.EmploymentDate,
                HasIdCard = staff.HasIdCard.Equals("Yes", StringComparison.InvariantCultureIgnoreCase) ? true : false,
                HasUniform = staff.HasUniform.Equals("Yes", StringComparison.InvariantCultureIgnoreCase) ? true : false,
                IsTeamLeader = staff.IsTeamLeader.Equals("Yes", StringComparison.InvariantCultureIgnoreCase) ? true : false,
            };
            var kk = JsonConvert.SerializeObject(postApproval);
            var result = await _staffService.Approval(postApproval);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Staff Successfully Updated" : "An Error Occurred" });
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Communication()
        {
            var staffComs = await _staffCommunication.GetStaffCommunication();
            return View(staffComs);
        }

        [Route("/Staff/Communication/New")]
        public async Task<IActionResult> NewCommunication()
        {
            var model = new CreateStaffCommunication();
            var staffs = await _staffService.GetStaffs();
            model.Staffs = staffs;
            return View(model);
        }
        [HttpPost]
        [Route("/Staff/NewCommunication", Name = "NewCommunication")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewCommunication(CreateStaffCommunication staffCommunication)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCommunication", staffCommunication);
            }

            if (staffCommunication.FileAttachment != null)
            {
                string attachmentFolder = "staffcommunication";
                var file = await _fileUpload.UploadFile(attachmentFolder, false, string.Concat(attachmentFolder, DateTime.Now.ToString("yyyyMMddhhmmss"), "_", staffCommunication.FileAttachment.FileName), staffCommunication.FileAttachment.OpenReadStream());
                staffCommunication.Attachment = file;
            }
            var model = Mapper.Map<PostStaffCommunication>(staffCommunication);
            var result = await _staffCommunication.Post(model);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Staff Communication Successfully saved" : "An Error Occurred" });

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Communication");
            }
            else
            {
                if (result.StatusCode != System.Net.HttpStatusCode.InternalServerError)
                    ModelState.AddModelError("", content);
                return View("NewCommunication", model);
            }


        }

        [HttpGet]
        [Route("/Staff/Communication/Details/{id}", Name = "CommunicationDetails")]
        public async Task<IActionResult> CommunicationDetails(int id)
        {
            var details = await _staffCommunication.GetStaffCommunication(id);
            return View(details);
        }

        [HttpGet]
        [Route("[Controller]/Rota/Create", Name = "CreateRota")]
        public IActionResult CreateRota()
        {

            var model = new CreateStaffRota();
            return View(model);
        }


        [HttpPost]
        [Route("[Controller]/Rota/Create", Name = "CreateRota")]
        public IActionResult CreateRota(CreateStaffRota model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToRoute("PreviewRota", new { startDate = model.StartDate, endDate = model.StopDate });
        }

        [HttpGet]
        [Route("[Controller]/Rota/Preview", Name = "PreviewRota")]
        public async Task<IActionResult> PreviewRota(string startDate, string endDate)
        {

            PreviewStaffRota model = new PreviewStaffRota();
            model.StartDate = startDate;
            model.StopDate = endDate;
            var staffs = await _staffService.GetStaffs();
            var selections = await _staffService.GetRotaSelections();

            DateTime start = DateTime.TryParseExact(startDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-Us"), DateTimeStyles.None, out DateTime sdate) ? sdate : default;
            DateTime end = DateTime.TryParseExact(endDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-Us"), DateTimeStyles.None, out DateTime edate) ? edate : default;

            var days = end.Subtract(start).Days + 1;
            var dates = Enumerable.Range(0, days).Select(d => start.AddDays(d));

            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.Selections = selections.Select(r => new SelectListItem(r.ItemName, r.StaffRotaDynamicAdditionId.ToString())).ToList();

            await SetPreviewRota(model, dates.ToList());
            return View(model);
        }

        [HttpPost]
        [Route("[Controller]/Rota/Preview", Name = "PreviewRota")]
        public async Task<IActionResult> PreviewRota(PreviewStaffRota model)
        {
            if (model.IsMedication)
            {
                List<PostStaffMedRota> rotas = new List<PostStaffMedRota>();
                var rotaDayofWeeks = await _rotaDayofWeekService.Get();

                foreach (var day in model.RotaDays)
                {
                    string dayOfWeek = day.Date.DayOfWeek.ToString();
                    int? dayOfWeekId = rotaDayofWeeks.FirstOrDefault(d => d.DayofWeek.Equals(dayOfWeek, StringComparison.InvariantCultureIgnoreCase))?.RotaDayofWeekId;


                    var rotaId = day.RotaId;


                    foreach (var staff in day.SelectedStaffs)
                    {
                        if (dayOfWeekId.HasValue)
                        {
                            var rota = new PostStaffMedRota();
                            rota.Remark = day.Remark;
                            rota.ReferenceNumber = DateTime.Now.ToString("yyyyMMddhhmmssms") + day.SelectedStaffs.IndexOf(staff);
                            rota.RotaDate = day.Date;//.ToString("MM/dd/yyyy");
                            rota.RotaId = day.RotaId;
                            rota.RotaDayofWeekId = dayOfWeekId;
                            rota.Staff = staff;
                            rota.DoseGiven = "";
                            rota.Time = "";
                            rota.Measurement = "";
                            rota.Location = "";
                            rota.Feedback = "";
                            rotas.Add(rota);
                        }
                    }


                }


                var result = await _medicationServices.Post(rotas);
                var content = await result.Content.ReadAsStringAsync();
                SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Staff Rota Successfully saved" : "An Error Occurred" });

                if (!result.IsSuccessStatusCode)
                {
                    _logger.LogError(content, "PreviewRota");
                    var staffs = await _staffService.GetStaffs();
                    model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                    return View(model);
                }


                string sdate = DateTime.TryParseExact(model.StartDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime sdatetime) ? sdatetime.ToString("yyyy-MM-dd") : model.StartDate;
                string edate = DateTime.TryParseExact(model.StopDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime edatetime) ? edatetime.ToString("yyyy-MM-dd") : model.StopDate;

                return RedirectToAction("MedTracker", "Medication", new { startDate = sdate, stopDate = edate });
            }
            else
            { 
                List<PostStaffRota> rotas = new List<PostStaffRota>();
                var rotaDayofWeeks = await _rotaDayofWeekService.Get();

                foreach (var day in model.RotaDays)
                {
                    string dayOfWeek = day.Date.DayOfWeek.ToString();
                    int? dayOfWeekId = rotaDayofWeeks.FirstOrDefault(d => d.DayofWeek.Equals(dayOfWeek, StringComparison.InvariantCultureIgnoreCase))?.RotaDayofWeekId;


                    var rotaId = day.RotaId;


                    foreach (var staff in day.SelectedStaffs)
                    {
                        if (dayOfWeekId.HasValue)
                        {
                            var rota = new PostStaffRota();
                            rota.Remark = day.Remark;
                            rota.ReferenceNumber = DateTime.Now.ToString("yyyyMMddhhmmssms") + day.SelectedStaffs.IndexOf(staff);
                            rota.RotaDate = day.Date;//.ToString("MM/dd/yyyy");
                            rota.RotaId = day.RotaId;
                            rota.RotaDayofWeekId = dayOfWeekId;

                            foreach (var rp in day.RotaTypes)
                            {
                                var attachedRotaClients = await rotaTaskService.GetAttachRotaClientAsync(rotaId, dayOfWeekId.Value, rp.ClientRotaTypeId);
                                foreach (var rotaClient in attachedRotaClients)
                                {
                                    rota.StaffRotaPeriods.Add(new PostStaffRotaPeriod
                                    {
                                        ClientRotaTypeId = rp.ClientRotaTypeId,
                                        ClientId = rotaClient.ClientId
                                    });
                                }
                            }


                            rota.Staff = staff;
                            rota.StaffRotaPartners = (from pt in day.SelectedStaffs
                                                      where pt != staff
                                                      select new PostStaffRotaPartner
                                                      {
                                                          StaffId = pt
                                                      }).ToList();

                            rota.StaffRotaItem = (from item in day.Items
                                                  select new PostStaffRotaItem
                                                  {
                                                      StaffRotaDynamicAdditionId = item
                                                  }).ToList();
                            rotas.Add(rota);
                        }
                    }


                }


                var result = await _staffService.CreateRota(rotas);
                var content = await result.Content.ReadAsStringAsync();
                SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Staff Rota Successfully saved" : "An Error Occurred" });

                if (!result.IsSuccessStatusCode)
                {
                    _logger.LogError(content, "PreviewRota");
                    var staffs = await _staffService.GetStaffs();
                    model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                    return View(model);
                }
            

                string sdate = DateTime.TryParseExact(model.StartDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime sdatetime) ? sdatetime.ToString("yyyy-MM-dd") : model.StartDate;
                string edate = DateTime.TryParseExact(model.StopDate, "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime edatetime) ? edatetime.ToString("yyyy-MM-dd") : model.StopDate;

                return RedirectToAction("RotaAdmin", "Rotering", new { startDate = sdate, stopDate = edate });
            }
        }

        [HttpGet("Profile/Edit", Name = "EditProfile")]
        public async Task<IActionResult> EditProfile(int staffId)
        {
            var staffInfo = await _staffService.GetStaff(staffId);
            var workTeams = await staffWorkTeamService.Get();
            var putProfile = Mapper.Map<UpdateStaffPersonalInfo>(staffInfo);

            putProfile.WorkTeams = workTeams.Select(w => new SelectListItem(w.WorkTeam, w.StaffWorkTeamId.ToString())).ToList();
            return View(putProfile);
        }

        [HttpPost("Profile/Edit", Name = "EditProfile")]
        public async Task<IActionResult> EditProfile(UpdateStaffPersonalInfo model)
        {

            var workTeams = await staffWorkTeamService.Get();
            if (!ModelState.IsValid)
            {
                model.WorkTeams = workTeams.Select(w => new SelectListItem(w.WorkTeam, w.StaffWorkTeamId.ToString())).ToList();
                return View(model);
            }

            if (model.ProfilePixFile != null && model.ProfilePixFile.Length > 0)
            {
                var profilePix = await UploadFile(profilePixFolder, string.Concat(profilePixFolder, "_", model.Telephone), true, model.ProfilePixFile.OpenReadStream());
                model.ProfilePix = profilePix;
            }

            if (model.DrivingLicenseFile != null && model.DrivingLicenseFile.Length > 0)
            {
                var drivingLicense = await UploadFile(drivingFolder, string.Concat(drivingFolder, "_", model.Telephone), false, model.DrivingLicenseFile.OpenReadStream());
                model.DrivingLicense = drivingLicense;
            }


            if (model.RightToWorkFile != null && model.RightToWorkFile.Length > 0)
            {
                var righttowork = await UploadFile(rightToFolder, string.Concat(rightToFolder, "_", model.Telephone), false, model.RightToWorkFile.OpenReadStream());
                model.RightToWorkAttachment = righttowork;
            }

            if (model.DbsFile != null && model.DbsFile.Length > 0)
            {

                var dbs = await UploadFile(dbsFolder, string.Concat(dbsFolder, "_", model.Telephone), false, model.DbsFile.OpenReadStream());
                model.DBSAttachment = dbs;
            }

            if (model.NiFile != null && model.NiFile.Length > 0)
            {
                var ni = await UploadFile(niFolder, string.Concat(niFolder, "_", model.Telephone), false, model.NiFile.OpenReadStream());
                model.NIAttachment = ni;
            }

            if (model.SelfPyeFile != null && model.SelfPyeFile.Length > 0)
            {
                var selfpye = await UploadFile(selfpyeFolder, string.Concat(selfpyeFolder, "_", model.Telephone), false, model.SelfPyeFile.OpenReadStream());
                model.SelfPYEAttachment = selfpye;
            }

            if (model.CoverLetterFile != null && model.CoverLetterFile.Length > 0)
            {
                var coverletter = await UploadFile(coverLetterFolder, string.Concat(coverLetterFolder, "_", model.Telephone), true, model.CoverLetterFile.OpenReadStream());
                model.CoverLetter = coverletter;
            }

            if (model.CvFile != null && model.CvFile.Length > 0)
            {
                var cv = await UploadFile(cvFolder, string.Concat(cvFolder, "_", model.Telephone), true, model.CvFile.OpenReadStream());
                model.CV = cv;
            }

            await EditEducation(model);

            await EditTraining(model);

            await EditReferee(model);

            if (model.StaffWorkTeamId == 0)
                model.StaffWorkTeamId = default(int?);

            var profile = Mapper.Map<PutStaffPersonalInfo>(model);

            var json = JsonConvert.SerializeObject(profile);
            var result = await _staffService.UpdateStaffPersonalProfile(profile);
            var content = await result.Content.ReadAsStringAsync();



            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Operation successful" : content });
            if (!result.IsSuccessStatusCode)
            {
                model.WorkTeams = workTeams.Select(w => new SelectListItem(w.WorkTeam, w.StaffWorkTeamId.ToString())).ToList();

                _logger.LogError(content);
                return View(model);
            }

            return RedirectToAction("Index");
        }


        public async Task EditEducation(UpdateStaffPersonalInfo model)
        {
            for (int i = 0; i < model.Education.Count; i++)
            {
                var edu = model.Education[i];
                if (edu.UploadCertificate != null && edu.UploadCertificate.Length > 0)
                {
                    var edufile = await UploadFile(educationFolder, string.Concat(educationFolder, "_", model.Telephone), false, edu.UploadCertificate.OpenReadStream());
                    edu.CertificateAttachment = edufile;
                };

            }
        }

        public async Task EditTraining(UpdateStaffPersonalInfo model)
        {
            for (int i = 0; i < model.Trainings.Count; i++)
            {
                var training = model.Trainings[i];
                if (training.UploadAttachment != null && training.UploadAttachment.Length > 0)
                {
                    var edufile = await UploadFile(trainingFolder, string.Concat(trainingFolder, "_", model.Telephone), false, training.UploadAttachment.OpenReadStream());
                    training.CertificateAttachment = edufile;
                };

            }
        }

        public async Task EditReferee(UpdateStaffPersonalInfo model)
        {
            for (int i = 0; i < model.References.Count; i++)
            {
                var referee = model.References[i];
                if (referee.UploadAttachment != null && referee.UploadAttachment.Length > 0)
                {
                    var refereefile = await UploadFile(refereeFolder, string.Concat(refereeFolder, "_", model.Telephone), false, referee.UploadAttachment.OpenReadStream());
                    referee.Attachment = refereefile;
                };

            }
        }

        async Task SetPreviewRota(PreviewStaffRota model, List<DateTime> dates)
        {
            var rotaTypes = await _clientRotaTypeService.Get();

            var excludeDates = dates.Select(d => d.ToString("yyyy-MM-dd")).ToList();
            var commaSeparatedDates = string.Join(",", excludeDates);

            var rotas = await _clientRotaNameService.GetByExcludeDate(commaSeparatedDates);

            foreach (var date in dates)
            {
                PreviewStaffRotaDate rotaDate = new PreviewStaffRotaDate();
                rotaDate.Date = date;
                rotaDate.RotaTypes = rotaTypes.Select(r => new CreateMedicationPeriod()
                {
                    RotaType = r.RotaType,
                    ClientRotaTypeId = r.ClientRotaTypeId,
                    IsSelected = true
                }).ToList();

                rotaDate.Rotas = rotas.Select(r => new SelectListItem(r.RotaName, r.RotaId.ToString())).ToList();

                model.RotaDays.Add(rotaDate);
            }

        }

        async Task<string> UploadFile(string folder, string filename, bool isPublic, Stream fileStream)
        {
            string path = await _fileUpload.UploadFile(folder, isPublic, filename, fileStream);
            return path;
        }

        public async Task<IActionResult> ChangeEmail(string userId)
        {
            var userProfile = await _staffService.GetChangeEmail(userId);
            var postChangeEmail = new PostChangeEmail
            {
                EmailAddress = userProfile.Email,
                UserId = userProfile.UserId
            };
            return View(postChangeEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(PostChangeEmail model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _staffService.PostChangeEmail(model);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Operation successful" : content });
            if (!result.IsSuccessStatusCode)
            {
                _logger.LogError(content);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string userId)
        {
            var userProfile = await _staffService.GetChangeEmail(userId);
            var postResetPassword = new PostResetPassord
            {
                Email = userProfile.Email
            };
            return View(postResetPassword);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PostResetPassord model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _staffService.ResetUserPassword(model);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Operation successful" : content });
            if (!result.IsSuccessStatusCode)
            {
                _logger.LogError(content);
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #region Client Feedback/Rating
        [HttpGet]
        public async Task<IActionResult> Feedback(int staffpersonalInfoId)
        {
            var model = new StaffFeedback();
            await FeedbackItems(model, staffpersonalInfoId);
            // var feedbacks = await _staffService.GetClientFeedback(staffpersonalInfoId);
            // model.StaffRatings = feedbacks;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Feedback(StaffFeedback model)
        {
            if (!ModelState.IsValid)
            {
                await FeedbackItems(model, model.StaffPersonalInfoId);
                return View(model);
            }

            var result = await _staffService.PostClientFeedback(model);
            var content = await result.Content.ReadAsStringAsync();
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Feedback Successfully saved" : "An Error Occurred" });

            if (!result.IsSuccessStatusCode)
            {
                await FeedbackItems(model, model.StaffPersonalInfoId);
                return View(model);
            }
            return RedirectToAction("Feedback", new { staffpersonalInfoId = model.StaffPersonalInfoId });
        }

        async Task FeedbackItems(StaffFeedback staffFeedback, int staffpersonalInfoId)
        {
            var clients = await _clientService.GetClients();
            var feedbacks = await _staffService.GetClientFeedback(staffpersonalInfoId);
            staffFeedback.StaffRatings = feedbacks;
            staffFeedback.ClientSelectLists = clients.Select(c => new SelectListItem(string.Concat(c.Firstname, " ", c.Middlename, " ", c.Surname), c.ClientId.ToString())).ToList();
        }
        #endregion

        #region StaffDetails
        [HttpGet]
        public JsonResult StaffDetailJson(int staffId)
        {
            var Staff = _staffService.Profile(staffId);
            return Json(Staff.Result);
        }
        [HttpGet]
        public JsonResult Staffholiday(int staffId)
        {
            var staff = _staffService.GetStaffs();
            var staffHoliday = _staffService.StaffHoliday(staffId);
            var holiday = staffHoliday.Result.GetStaffHoliday.FirstOrDefault();
            CreateStaffHoliday report = new CreateStaffHoliday();
                var Adays = Math.Round(holiday.AllocatedDays, 0);
                int Allocated = int.Parse(Adays.ToString());
                var setup = _setupStaff.Get();
                var days = setup.Result.Where(s => s.StaffPersonalInfoId == staffHoliday.Result.StaffPersonalInfoId && s.TypeOfHoliday == holiday.Class).FirstOrDefault();
                var day = days != null ? days.NumberOfDays : 0;
                report.StaffHolidayId = holiday.StaffHolidayId;
                report.StartDate = holiday.StartDate;
                report.EndDate = holiday.EndDate;
                report.AllocatedDays = holiday.AllocatedDays;
                report.Balance = (Allocated - day);
                report.StaffName = staff.Result.Where(s => s.StaffPersonalInfoId == staffHoliday.Result.StaffPersonalInfoId).FirstOrDefault().Fullname;
                report.ClassName = _baseService.GetBaseRecordItemById(holiday.Class).Result.ValueName;
            return Json(report);
        }

        [HttpGet]
        public JsonResult Staffeducation(int staffId)
        {
            var StaffEducation = _staffService.StaffEducation(staffId);
            return Json(StaffEducation.Result);
        }

        [HttpGet]
        public JsonResult Stafftrainingmatrix(int staffId)
        {
            var StaffTrainingMatrix = _staffService.StaffTrainingMatrix(staffId);
            return Json(StaffTrainingMatrix.Result);
        }

        [HttpGet]
        public JsonResult Stafftrainings(int staffId)
        {
            var StaffTraining = _staffService.StaffTraining(staffId);
            return Json(StaffTraining.Result);
        }

        [HttpGet]
        public JsonResult Staffreferee(int staffId)
        {
            var StaffReferee = _staffService.StaffReferee(staffId);
            return Json(StaffReferee.Result);
        }

        [HttpGet]
        public JsonResult StaffemergencyContact(int staffId)
        {
            var StaffEmergencyContact = _staffService.StaffEmergencyContact(staffId);
            return Json(StaffEmergencyContact.Result);
        }

        [HttpGet]
        public JsonResult Staffspotcheck(int staffId)
        {
            var StaffSpotCheck = _staffService.StaffSpotCheck(staffId);
            return Json(StaffSpotCheck.Result);
        }

        [HttpGet]
        public JsonResult Staffadlobs(int staffId)
        {
            var StaffAdlObs = _staffService.StaffAdlObs(staffId);
            return Json(StaffAdlObs.Result);
        }

        [HttpGet]
        public JsonResult Staffmedcomp(int staffId)
        {
            var StaffMedComp = _staffService.StaffMedComp(staffId);
            return Json(StaffMedComp.Result);
        }

        [HttpGet]
        public JsonResult Staffkeyworker(int staffId)
        {
            var StaffKeyWorker = _staffService.StaffKeyWorker(staffId);
            return Json(StaffKeyWorker.Result);
        }

        [HttpGet]
        public JsonResult Staffsurvey(int staffId)
        {
            var StaffSurvey = _staffService.StaffSurvey(staffId);
            return Json(StaffSurvey.Result);
        }

        [HttpGet]
        public JsonResult Staffonetoone(int staffId)
        {
            var StaffOneToOne = _staffService.StaffOneToOne(staffId);
            return Json(StaffOneToOne.Result);
        }

        [HttpGet]
        public JsonResult Staffsupervision(int staffId)
        {
            var StaffSupervision = _staffService.StaffSupervision(staffId);
            return Json(StaffSupervision.Result);
        }

        [HttpGet]
        public JsonResult Staffreference(int staffId)
        {
            var StaffReference = _staffService.StaffReference(staffId);
            return Json(StaffReference.Result);
        }

        [HttpGet]
        public JsonResult Staffpersonalitytest(int staffId)
        {
            var StaffPersonalityTest = _staffService.StaffPersonalityTest(staffId);
            List<GetStaffPersonalityTest> reports = new List<GetStaffPersonalityTest>();
            foreach (var item in StaffPersonalityTest.Result.GetStaffPersonalityTest)
            {
                GetStaffPersonalityTest report = new GetStaffPersonalityTest();
                    report.QuestionName = _baseService.GetBaseRecordItemById(item.Question).Result.ValueName;
                    report.AnswerName = _baseService.GetBaseRecordItemById(item.Answer).Result.ValueName;
                    reports.Add(report);

            }
            return Json(reports);
        }

        [HttpGet]
        public JsonResult Staffinfection(int staffId)
        {
            var StaffInfectionControl = _staffService.StaffInfectionControl(staffId);
            List<CreateStaffInfectionControl> reports = new List<CreateStaffInfectionControl>();
            foreach (var item in StaffInfectionControl.Result.GetStaffInfectionControl)
            {
                CreateStaffInfectionControl report = new CreateStaffInfectionControl();
                report.InfectionName = _baseService.GetBaseRecordItemById(item.Status).Result.ValueName;
                report.VaccName = _baseService.GetBaseRecordItemById(item.VaccStatus).Result.ValueName;
                report.TypeName = _baseService.GetBaseRecordItemById(item.Type).Result.ValueName;
                report.TestDate = item.TestDate;
                report.Guideline = item.Guideline;
                report.Remarks = item.Remarks;
                reports.Add(report);

            }
            return Json(reports);
        }

        [HttpGet]
        public JsonResult Staffcompetence(int staffId)
        {
            var StaffCompetenceTest = _staffService.StaffCompetenceTest(staffId);
            List<GetStaffCompetenceTestTask> reports = new List<GetStaffCompetenceTestTask>();
            foreach (var item in StaffCompetenceTest.Result.GetStaffCompetenceTest)
            {
                foreach (var task in item.GetStaffCompetenceTestTask)
                {
                    GetStaffCompetenceTestTask report = new GetStaffCompetenceTestTask();
                    report.AnswerName = _baseService.GetBaseRecordItemById(task.Answer).Result.ValueName;
                    report.TitleName = _baseService.GetBaseRecordItemById(task.Title).Result.ValueName;
                    report.Comment = task.Comment;
                    report.Point = task.Point;
                    report.Score = task.Score;
                    reports.Add(report);
                }
                
            }
            return Json(reports);
        }

        [HttpGet]
        public JsonResult Staffhealth(int staffId)
        {
            var StaffHealth = _staffService.StaffHealth(staffId);
            List<GetStaffHealthTask> reports = new List<GetStaffHealthTask>();
            foreach (var item in StaffHealth.Result.GetStaffHealth)
            {
                foreach (var task in item.GetStaffHealthTask)
                {
                    GetStaffHealthTask report = new GetStaffHealthTask();
                    report.AnswerName = _baseService.GetBaseRecordItemById(task.Answer).Result.ValueName;
                    report.TitleName = _baseService.GetBaseRecordItemById(task.Title).Result.ValueName;
                    report.Comment = task.Comment;
                    report.Point = task.Point;
                    report.Score = task.Score;
                    reports.Add(report);
                }

            }
            return Json(reports);
        }

        [HttpGet]
        public JsonResult Staffinterview(int staffId)
        {
            var StaffInterview = _staffService.StaffInterview(staffId);
            List<GetStaffInterviewTask> reports = new List<GetStaffInterviewTask>();
            foreach (var item in StaffInterview.Result.GetStaffInterview)
            {
                foreach (var task in item.GetStaffInterviewTask)
                {
                    GetStaffInterviewTask report = new GetStaffInterviewTask();
                    report.AnswerName = _baseService.GetBaseRecordItemById(task.Answer).Result.ValueName;
                    report.TitleName = _baseService.GetBaseRecordItemById(task.Title).Result.ValueName;
                    report.Comment = task.Comment;
                    report.Point = task.Point;
                    report.Score = task.Score;
                    reports.Add(report);
                }

            }
            return Json(reports);
        }

        [HttpGet]
        public JsonResult Staffshadowing(int staffId)
        {
            var StaffShadowing = _staffService.StaffShadowing(staffId);
            List<GetStaffShadowingTask> reports = new List<GetStaffShadowingTask>();
            foreach (var item in StaffShadowing.Result.GetStaffShadowing)
            {
                foreach (var task in item.GetStaffShadowingTask)
                {
                    GetStaffShadowingTask report = new GetStaffShadowingTask();
                    report.AnswerName = _baseService.GetBaseRecordItemById(task.Answer).Result.ValueName;
                    report.TitleName = _baseService.GetBaseRecordItemById(task.Title).Result.ValueName;
                    report.Comment = task.Comment;
                    report.Point = task.Point;
                    report.Score = task.Score;
                    reports.Add(report);
                }

            }
            return Json(reports);
        }

        [HttpGet]
        public JsonResult Staffsalaryallowance(int staffId)
        {
            var StaffAllowance = _staffService.StaffAllowance(staffId);
            return Json(StaffAllowance.Result);
        }

        [HttpGet]
        public JsonResult Staffsalarydeduction(int staffId)
        {
            var StaffDeduction = _staffService.StaffDeduction(staffId);
            return Json(StaffDeduction.Result);
        }

        [HttpGet]
        public JsonResult StaffTeamLead(int staffId)
        {
            var StaffTeamLead = _staffService.StaffTeamLead(staffId);
            return Json(StaffTeamLead.Result);
        }
        #endregion
    }
}