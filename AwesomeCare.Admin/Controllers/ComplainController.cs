using AwesomeCare.Admin.Services.ComplainRegister;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientComplainRegister;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Buffers;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.DataTransferObject.DTOs.ClientComplain;

namespace AwesomeCare.Admin.Controllers
{
    public class ComplainController : BaseController
    {
        private IComplainService _complainService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ComplainController(IComplainService complainService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache, IBaseRecordService baseService) : base(fileUpload)
        {
            _complainService = complainService;
            _clientService = clientService;
            _staffService = staffService;
            _baseService = baseService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            var entities = await _complainService.Get();
            var client = await _clientService.GetClients();
            List<CreateComplainRegister> reports = new List<CreateComplainRegister>();
            foreach (GetClientComplainRegister item in entities)
            {
                var report = new CreateComplainRegister();
                report.ComplainId = item.ComplainId;
                report.Reference = item.Reference;
                report.DUEDATE = item.DUEDATE;
                report.CONCERNSRAISED = item.CONCERNSRAISED;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.Firstname).FirstOrDefault();
                report.StatusName = _baseService.GetBaseRecordItemById(item.StatusId).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> View(int complainId)
        {
            var complain = _complainService.Get(complainId);
            var staffNames = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();
            if (complain == null) return NotFound();
            var putEntity = new CreateComplainRegister
            {
                ClientName = client.Where(s=>s.ClientId==complain.Result.ClientId).FirstOrDefault().FullName,
                Reference = complain.Result.Reference,
                ComplainId = complain.Result.ComplainId,
                ClientId = complain.Result.ClientId,
                ACTIONTAKEN = complain.Result.ACTIONTAKEN,
                COMPLAINANTCONTACT = complain.Result.COMPLAINANTCONTACT,
                CONCERNSRAISED = complain.Result.CONCERNSRAISED,
                DATEOFACKNOWLEDGEMENT = complain.Result.DATEOFACKNOWLEDGEMENT,
                DATERECIEVED = complain.Result.DATERECIEVED,
                DUEDATE = complain.Result.DUEDATE,
                EvidenceFilePath = complain.Result.EvidenceFilePath,
                FINALRESPONSETOFAMILY = complain.Result.FINALRESPONSETOFAMILY,
                INCIDENTDATE = complain.Result.INCIDENTDATE,
                INVESTIGATIONOUTCOME = complain.Result.INVESTIGATIONOUTCOME,
                IRFNUMBER = complain.Result.IRFNUMBER,
                LETTERTOSTAFF = complain.Result.LETTERTOSTAFF,
                LINK = complain.Result.LINK,
                REMARK = complain.Result.REMARK,
                ROOTCAUSE = complain.Result.ROOTCAUSE,
                SOURCEOFCOMPLAINTS = complain.Result.SOURCEOFCOMPLAINTS,
                StatusId = complain.Result.StatusId,
                OfficerName = complain.Result.OfficerToAct.Select(s => s.StaffName).ToList(),
                Staff_Name = complain.Result.StaffName.Select(s => s.StaffName).ToList()
            };
            return View(putEntity);
        }

        #region Complain
        public async Task<IActionResult> CreateComplainRegister(int? clientId)
        {
            var model = new CreateComplainRegister();
            var staffNames = await _staffService.GetStaffs();
            model.STAFFINVOLVED = staffNames.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId.Value).FirstOrDefault().FullName;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComplainRegister(CreateComplainRegister model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    var client = await _clientService.GetClientDetail();
                    model.ClientName = client.Where(s => s.ClientId == model.ClientId).FirstOrDefault().FullName;
                    var staffNames = await _staffService.GetStaffs();
                    model.STAFFINVOLVED = staffNames.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                    return View(model);
                }


                #region Evidence
                if (model.Evidence != null)
                {
                    string folder = "clientcomplain";
                    string filename = string.Concat(folder, "_", model.IRFNUMBER);
                    string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());

                    model.EvidenceFilePath = path;

                }
                else
                {
                    model.EvidenceFilePath = "No Image";
                }

                #endregion

                var post = new PostComplainRegister();
                post.ACTIONTAKEN = model.ACTIONTAKEN;
                post.ClientId = model.ClientId;
                post.COMPLAINANTCONTACT = model.COMPLAINANTCONTACT;
                post.CONCERNSRAISED = model.CONCERNSRAISED;
                post.DATEOFACKNOWLEDGEMENT = model.DATEOFACKNOWLEDGEMENT;
                post.DATERECIEVED = model.DATERECIEVED;
                post.DUEDATE = model.DUEDATE;
                post.FINALRESPONSETOFAMILY = model.FINALRESPONSETOFAMILY;
                post.INCIDENTDATE = model.INCIDENTDATE;
                post.INVESTIGATIONOUTCOME = model.INVESTIGATIONOUTCOME;
                post.IRFNUMBER = model.IRFNUMBER;
                post.LETTERTOSTAFF = model.LETTERTOSTAFF;
                post.LINK = model.LINK;
                post.OfficerToAct = model.OfficerToAct.Select(o => new PostComplainOfficerToAct { StaffPersonalInfoId = o, ComplainId = model.ComplainId}).ToList();
                post.Reference = model.Reference;
                post.REMARK = model.REMARK;
                post.ROOTCAUSE = model.ROOTCAUSE;
                post.SOURCEOFCOMPLAINTS = model.SOURCEOFCOMPLAINTS;
                post.StaffName = model.StaffName.Select(o => new PostComplainStaffName { StaffPersonalInfoId = o, ComplainId = model.ComplainId }).ToList();
                post.StatusId = model.StatusId;

                var result = await _complainService.Create(post);
                var content = await result.Content.ReadAsStringAsync();

                SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode , Message = result.Content.ReadAsStringAsync().Result != null ? "New Complain successfully registered" : "An Error Occurred" });
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId, ActiveTab = model.ActiveTab });
            }
            catch (Exception ex)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = "An Error Occurred" });
                _logger.LogError(ex, "CreateComplainRegister", null);
                return View("CreateComplainRegister", model);
            }
        }
        public async Task<IActionResult> Edit(int complainId)
        {
            var complain = _complainService.Get(complainId);
            var staffNames = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();
            if (complain == null) return NotFound();
        var putEntity = new CreateComplainRegister
            {
               
                Reference = complain.Result.Reference,
                ComplainId = complain.Result.ComplainId,
                ClientId = complain.Result.ClientId,
                ACTIONTAKEN = complain.Result.ACTIONTAKEN,
                COMPLAINANTCONTACT = complain.Result.COMPLAINANTCONTACT,
                CONCERNSRAISED = complain.Result.CONCERNSRAISED,
                DATEOFACKNOWLEDGEMENT = complain.Result.DATEOFACKNOWLEDGEMENT,
                DATERECIEVED = complain.Result.DATERECIEVED,
                DUEDATE = complain.Result.DUEDATE,
                EvidenceFilePath = complain.Result.EvidenceFilePath,
                FINALRESPONSETOFAMILY = complain.Result.FINALRESPONSETOFAMILY,
                INCIDENTDATE = complain.Result.INCIDENTDATE,
                INVESTIGATIONOUTCOME = complain.Result.INVESTIGATIONOUTCOME,
                IRFNUMBER = complain.Result.IRFNUMBER,
                LETTERTOSTAFF = complain.Result.LETTERTOSTAFF,
                LINK = complain.Result.LINK,
                REMARK = complain.Result.REMARK,
                ROOTCAUSE = complain.Result.ROOTCAUSE,
                SOURCEOFCOMPLAINTS = complain.Result.SOURCEOFCOMPLAINTS,
                StatusId = complain.Result.StatusId,
                OfficerToAct = complain.Result.OfficerToAct.Select(s => s.StaffPersonalInfoId).ToList(),
                StaffName = complain.Result.StaffName.Select(s => s.StaffPersonalInfoId).ToList(),
                STAFFINVOLVED = staffNames.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList()
        };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateComplainRegister model)
        {
            if (!ModelState.IsValid)
            {
                var staffNames = await _staffService.GetStaffs();
                model.STAFFINVOLVED = staffNames.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            #region Evidence
            if (model.Evidence != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_", model.IRFNUMBER);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());

                model.EvidenceFilePath = path;
            }
            else
            {
                model.EvidenceFilePath = model.EvidenceFilePath;
            }
            #endregion
            var put = new PutComplainRegister();
            put.ComplainId = model.ComplainId;
            put.ACTIONTAKEN = model.ACTIONTAKEN;
            put.ClientId = model.ClientId;
            put.COMPLAINANTCONTACT = model.COMPLAINANTCONTACT;
            put.CONCERNSRAISED = model.CONCERNSRAISED;
            put.DATEOFACKNOWLEDGEMENT = model.DATEOFACKNOWLEDGEMENT;
            put.DATERECIEVED = model.DATERECIEVED;
            put.DUEDATE = model.DUEDATE;
            put.FINALRESPONSETOFAMILY = model.FINALRESPONSETOFAMILY;
            put.INCIDENTDATE = model.INCIDENTDATE;
            put.INVESTIGATIONOUTCOME = model.INVESTIGATIONOUTCOME;
            put.IRFNUMBER = model.IRFNUMBER;
            put.LETTERTOSTAFF = model.LETTERTOSTAFF;
            put.LINK = model.LINK;
            put.OfficerToAct = model.OfficerToAct.Select(o => new PutComplainOfficerToAct { StaffPersonalInfoId = o, ComplainId = model.ComplainId }).ToList();
            put.Reference = model.Reference;
            put.REMARK = model.REMARK;
            put.ROOTCAUSE = model.ROOTCAUSE;
            put.SOURCEOFCOMPLAINTS = model.SOURCEOFCOMPLAINTS;
            put.StaffName = model.StaffName.Select(o => new PutComplainStaffName { StaffPersonalInfoId = o, ComplainId = model.ComplainId }).ToList();
            put.StatusId = model.StatusId;
            
            var putComplain = Mapper.Map<PutComplainRegister>(put);
            var entity = await _complainService.Put(putComplain);
            
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode == true ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode != false)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var staffNames = await _staffService.GetStaffs();
                model.STAFFINVOLVED = staffNames.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                return View(model);
            }
            

        }
        #endregion
    }
}
