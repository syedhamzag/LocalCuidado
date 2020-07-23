using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AwesomeCare.Services.Services;
using AwesomeCare.Web.Models;
using AwesomeCare.Web.Services.Communication;
using AwesomeCare.Web.Services.Staff;
using AwesomeCare.Web.ViewModels.Communication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AwesomeCare.Web.Controllers
{
    public class CommunicationController : BaseController
    {
        private ICommunicationService _communicationService;
        private IStaffService _staffService;

        public CommunicationController(ICommunicationService communicationService, IStaffService staffService, IFileUpload fileUpload):base(fileUpload) 
        {
            _communicationService = communicationService;
            _staffService = staffService;
        }
        public async Task<IActionResult> Index()
        {
            var request = await _communicationService.Get();
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Compose()
        {
            var model = new ComposeMessage();
            await LoadData(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Compose(ComposeMessage model)
        {

            if (!ModelState.IsValid)
            {
                await LoadData(model);
                return View(model);
            }

            var result = await _communicationService.Post(model);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Message Sent" : "An error occurred, please try again" });

            if (!result.IsSuccessStatusCode)
            {
                await LoadData(model);
                return View(model);
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Inbox(int messageId)
        {
            var messageInbox = await _communicationService.GetInbox(messageId);
            return View(messageInbox);
        }

        [HttpGet]
        public async Task<IActionResult> Sent(int messageId)
        {
            var messageSent = await _communicationService.GetSent(messageId);
            return View(messageSent);
        }
        async Task LoadData(ComposeMessage model)
        {
            string currentUser = User.SubClaim();
            var staffs = await _staffService.GetStaffs();
            model.Staffs = staffs.Where(u => u.ApplicationUserId != currentUser && !string.IsNullOrEmpty(u.ApplicationUserId)).Select(s => new SelectListItem(s.Fullname, s.ApplicationUserId)).ToList();
        }
    }
}
