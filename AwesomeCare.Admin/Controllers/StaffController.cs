using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffCommunication;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.Admin.ViewModels.StaffCommunication;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffCommunication;
using AwesomeCare.Services.Services;
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
        public StaffController(IStaffService staffService, ILogger<StaffController> logger, IFileUpload fileUpload, IStaffCommunication staffCommunication)
        {
            _staffService = staffService;
            _logger = logger;
            _fileUpload = fileUpload;
            _staffCommunication = staffCommunication;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var staffs = await _staffService.GetStaffs();

                return View(staffs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetStaffs");
                throw;
            }

        }

        public async Task<IActionResult> Details(int staffId)
        {
            var profile = await _staffService.Profile(staffId);
            profile.Statuses.ForEach(s =>
            {
                if (s.Value == profile.Status || s.Text == profile.Status)
                    s.Selected = true;
            });
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStaffRecord(StaffDetails staff)
        {
            if (!ModelState.IsValid)
            {
                return View(staff);
            }

            var postApproval = new PostStaffApproval
            {
                Comment = staff.Comment,
                Rate = staff.Rate,
                StaffPersonalInfoId = staff.StaffPersonalInfoId,
                Status = staff.Status
            };
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
            var details =await _staffCommunication.GetStaffCommunication(id);
            return View(details);
        }

        public async Task<IActionResult> DownloadFile(string file)
        {
            var filestream = await _fileUpload.DownloadFile(file);
            filestream.Item1.Position = 0;
            return File(filestream.Item1, filestream.Item2);
        }
    }
}