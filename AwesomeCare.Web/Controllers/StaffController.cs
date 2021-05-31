using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Services.Services;
using AwesomeCare.Web.Services.Admin;
using AwesomeCare.Web.Services.Staff;
using AwesomeCare.Web.ViewModels.Staff;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AwesomeCare.Web.Controllers
{
    public class StaffController : BaseController
    {
        private IStaffService _staffService;
        private ILogger<StaffController> _logger;

        private IBaseRecordService _baseRecordService;
        private readonly IWebHostEnvironment _env;
        private readonly IMemoryCache _cache;

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

        public StaffController(IFileUpload fileUpload,
            IMemoryCache cache, IWebHostEnvironment env, IBaseRecordService baseRecordService, IStaffService staffService, ILogger<StaffController> logger) : base(fileUpload)
        {
            _staffService = staffService;
            _logger = logger;
            _cache = cache;
            _baseRecordService = baseRecordService;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Registration()
        {
            var model = new CreateStaff();
            model.Email = HttpContext.User.Claims.GetClaimValue("name");
            model.Education = new List<CreateStaffEducation>();
            model.Education.Add(new CreateStaffEducation());

            model.Trainings = new List<CreateStaffTraining>();
            model.Trainings.Add(new CreateStaffTraining());

            model.References = new List<CreateStaffReference>();
            model.References.Add(new CreateStaffReference());

            model.EmergencyContacts = new List<CreateStaffEmergencyContact>();
            model.EmergencyContacts.Add(new CreateStaffEmergencyContact());

            #region Regulatory Contact
            model.RegulatoryContacts = new List<CreateStaffRegulatoryContact>();
            if (_cache.TryGetValue(cacheKey, out List<GetBaseRecordWithItems> baseRecords))
            {

                model.RegulatoryContacts = (from rec in baseRecords
                                            where rec.KeyName == "Staff_RegulatoryContact"
                                            from recItem in rec.BaseRecordItems
                                            select new CreateStaffRegulatoryContact
                                            {
                                                BaseRecordItemId = recItem.BaseRecordItemId,
                                                RegulatoryContact = recItem.ValueName
                                            }).ToList();
            }
            else
            {
                var records = await _baseRecordService.GetBaseRecordsWithItems();

                model.RegulatoryContacts = (from rec in records
                                            where rec.KeyName == "Staff_RegulatoryContact"
                                            from recItem in rec.BaseRecordItems
                                            select new CreateStaffRegulatoryContact
                                            {
                                                BaseRecordItemId = recItem.BaseRecordItemId,
                                                RegulatoryContact = recItem.ValueName
                                            }).ToList();


                //Save BaseRecords to Cache
                _cache.Set(cacheKey, records, DateTimeOffset.Now.AddMinutes(60));
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(CreateStaff model)
        {

            model.ApplicationUserId = User.Claims.GetClaimValue("sub");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ensure to re-upload required files");
                return View(model);
            }

            var staffinfo = Mapper.Map<PostStaffFullInfo>(model);


            var profilePix = await UploadFile(profilePixFolder, string.Concat(profilePixFolder, "_", model.Telephone), true, model.ProfilePix.OpenReadStream());
            staffinfo.ProfilePix = profilePix;

            if (model.CanDrive.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
            {

                var drivingLicense = await UploadFile(drivingFolder, string.Concat(drivingFolder, "_", model.Telephone), false, model.DrivingLicense.OpenReadStream());
                staffinfo.DrivingLicense = drivingLicense;
            }

            if (model.RightToWork.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
            {

                var righttowork = await UploadFile(rightToFolder, string.Concat(rightToFolder, "_", model.Telephone), false, model.RightToWorkAttachment.OpenReadStream());
                staffinfo.RightToWorkAttachment = righttowork;
            }

            if (model.DBS.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
            {

                var dbs = await UploadFile(dbsFolder, string.Concat(dbsFolder, "_", model.Telephone), false, model.DBSAttachment.OpenReadStream());
                staffinfo.DBSAttachment = dbs;
            }

            if (model.NI.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
            {

                var ni = await UploadFile(niFolder, string.Concat(niFolder, "_", model.Telephone), false, model.NIAttachment.OpenReadStream());
                staffinfo.NIAttachment = ni;
            }

            if (model.SelfPYE.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
            {

                var selfpye = await UploadFile(selfpyeFolder, string.Concat(selfpyeFolder, "_", model.Telephone), false, model.Self_PYEAttachment.OpenReadStream());
                staffinfo.Self_PYEAttachment = selfpye;
            }


            var coverletter = await UploadFile(coverLetterFolder, string.Concat(coverLetterFolder, "_", model.Telephone), true, model.CoverLetter.OpenReadStream());
            staffinfo.CoverLetter = coverletter;


            var cv = await UploadFile(cvFolder, string.Concat(cvFolder, "_", model.Telephone), true, model.CV.OpenReadStream());
            staffinfo.CV = cv;

            #region RegulatoryContact
            var regulatoryContact = await RegulatoryContact(model);
            staffinfo.StaffRegulatoryContacts = regulatoryContact;
            #endregion

            var json = JsonConvert.SerializeObject(staffinfo);
            var result = await _staffService.PostStaffFullInfo(staffinfo);

            var content = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = true, Message = "Your registration was successful" });
                return RedirectToAction("Profile", "Staff", new { id = content });
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                _logger.LogWarning(content, new[] { "Staff Registration" });

                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = _env.IsDevelopment() ? content : "Some validation error occurred, please check and try again" });
            }
            else
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = "An error occurred" });
            }
            return View(model);
        }

        async Task<List<PostStaffRegulatoryContact>> RegulatoryContact(CreateStaff createStaff)
        {
            var items = createStaff.RegulatoryContacts.Where(s => s.IsSelected).ToList();
            foreach (var c in items)
            {

                string folder = "staffregulatorycontact";
                string filename = string.Concat(folder, "_", c.RegulatoryContact, "_", createStaff.Telephone);
                string path = await UploadFile(folder, filename, true, c.EvidenceFile.OpenReadStream());

                c.Evidence = path;
            }

            var regulatoryContact = Mapper.Map<List<PostStaffRegulatoryContact>>(items);
            return regulatoryContact;
        }


        async Task<string> UploadFile(string folder, string filename, bool isPublic, Stream fileStream)
        {
            string path = await _fileUpload.UploadFile(folder, isPublic, filename, fileStream);
            return path;
        }

        public async Task<IActionResult> Profile()
        {
            var profile = await _staffService.MyProfile();

            if (profile == null)
            {
                return RedirectToAction("Registration");
            }

            return View(profile);
        }

        [HttpGet("Profile/Edit", Name = "EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            var profile = await _staffService.EditMyProfile();
            var putProfile = Mapper.Map<UpdatePersonalInfo>(profile);
            return View(putProfile);
        }

        [HttpPost("Profile/Edit", Name = "EditProfile")]
        public async Task<IActionResult> EditProfile(UpdatePersonalInfo model)
        {

            if (!ModelState.IsValid)
            {
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
            var result = await _staffService.UpdateMyPersonalInfo(profile);
            var content = await result.Content.ReadAsStringAsync();



            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Operation successful" : "An Error Occurred" });
            if (!result.IsSuccessStatusCode)
            {
                _logger.LogError(content);
                return View(model);
            }

            return RedirectToAction("Profile");
        }


        public async Task EditEducation(UpdatePersonalInfo model)
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

        public async Task EditTraining(UpdatePersonalInfo model)
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

        public async Task EditReferee(UpdatePersonalInfo model)
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
    }
}