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

namespace AwesomeCare.Admin.Controllers
{
    public class ComplainController : BaseController
    {
        private IComplainService _complainService;
        private IClientService _clientService;
        private IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;

        public ComplainController(IComplainService complainService, IFileUpload fileUpload, 
            IClientService clientService, IStaffService staffService, IWebHostEnvironment env, 
            ILogger<ClientController> logger, IMemoryCache cache) : base(fileUpload)
        {
            _complainService = complainService;
            _clientService = clientService;
            _staffService = staffService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            var entities = await _complainService.Get();
            return View(entities);
        }

        public IActionResult Create()
        {
            return View();
        }

        #region Complain
        [Route("[Controller]/Complain/Create/{clientId}", Name = "CreateComplainRegister")]
        public async Task<IActionResult> CreateComplainRegister(int? clientId)
        {
            var model = new CreateComplainRegister();
            var client = await _clientService.GetClient(clientId.Value);
            var staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            model.ClientId = clientId.Value;
            model.ClientName = client.Firstname +" "+client.Middlename+" "+client.Surname ;
            //model.STAFFINVOLVED = (IEnumerable<GetStaffs>)staffNames.Select(s => new SelectListItem(s.Fullname.ToString(), s.ApplicationUserId)).ToList();
            //model.OFFICERTOACT = (IEnumerable<GetStaffs>)staffNames.Select(s => new SelectListItem(s.Fullname.ToString(), s.ApplicationUserId)).ToList();
            return View(model);
        }
        [HttpPost("[Controller]/Complain/Create/{clientId}", Name = "CreateComplainRegister")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComplainRegister(CreateComplainRegister model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    var staffNames = await _staffService.GetStaffs();
                    ViewBag.GetStaffs = staffNames;
                    ViewBag.Officer = new SelectList(staffNames, "StaffPersonalInfoId", "FullName", model.OFFICERTOACTId);
                    ViewBag.Staff = new SelectList(staffNames, "StaffPersonalInfoId", "FullName", model.STAFFId);
                    var client = await _clientService.GetClient(model.ClientId);
                    model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                    return View(model);
                }


                #region Evidence

                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_", model.IRFNUMBER);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());

                model.EvidenceFilePath = path;
                #endregion

                var postComplain = Mapper.Map<PostComplainRegister>(model);

                var json = JsonConvert.SerializeObject(postComplain);
                var result = await _complainService.PostComplainRegister(postComplain);
                var content = await result.Content.ReadAsStringAsync();

                SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Complain successfully registered" : "An Error Occurred" });
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
            var complain = await _complainService.Get(complainId);
            var staffNames = await _staffService.GetStaffs();
            ViewBag.GetStaffs = staffNames;
            var client = await _clientService.GetClient(complain.ClientId);
            if (complain == null) return NotFound();

            var putEntity = new CreateComplainRegister
            {
                ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname,
                ComplainId = complain.ComplainId,
                ClientId = complain.ClientId,
                ACTIONTAKEN = complain.ACTIONTAKEN,
                COMPLAINANTCONTACT = complain.COMPLAINANTCONTACT,
                CONCERNSRAISED = complain.CONCERNSRAISED,
                DATEOFACKNOWLEDGEMENT = complain.DATEOFACKNOWLEDGEMENT,
                DATERECIEVED = complain.DATERECIEVED,
                DUEDATE = complain.DUEDATE,
                EvidenceFilePath = complain.EvidenceFilePath,
                FINALRESPONSETOFAMILY = complain.FINALRESPONSETOFAMILY,
                INCIDENTDATE = complain.INCIDENTDATE,
                INVESTIGATIONOUTCOME = complain.INVESTIGATIONOUTCOME,
                IRFNUMBER = complain.IRFNUMBER,
                LETTERTOSTAFF = complain.LETTERTOSTAFF,
                LINK = complain.LINK,
                OFFICERTOACTId = complain.OFFICERTOACTId,
                REMARK = complain.REMARK,
                ROOTCAUSE = complain.ROOTCAUSE,
                SOURCEOFCOMPLAINTS = complain.SOURCEOFCOMPLAINTS,
                STAFFId = complain.STAFFId,
                StatusId = complain.StatusId,
                //STAFFINVOLVED = (IEnumerable<GetStaffs>)staffNames.Select(s => new SelectListItem(s.Fullname.ToString(), s.ApplicationUserId)).ToList(),
                //OFFICERTOACT = (IEnumerable<GetStaffs>)staffNames.Select(s => new SelectListItem(s.Fullname.ToString(), s.ApplicationUserId)).ToList()
            };
            return View(putEntity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateComplainRegister model)
        {
            if (!ModelState.IsValid)
            {
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
            var putComplain = Mapper.Map<PutComplainRegister>(model);
            var entity = await _complainService.Put(putComplain);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity != null,
                Message = entity != null ? "Successful" : "Operation failed"
            });
            if (entity != null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }
        #endregion
    }
}
