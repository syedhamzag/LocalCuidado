using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.StaffBlackLIst;
using AwesomeCare.Admin.ViewModels.BlackList;
using AwesomeCare.DataTransferObject.DTOs.StaffBlackList;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffBlackListController : BaseController
    {
        private readonly IStaffBlackListService _staffBlackListService;
        private readonly IStaffService _staffService;
        private readonly IClientService _clientService;


        public StaffBlackListController(IStaffBlackListService staffBlackListService, IStaffService staffService,
            IClientService clientService, IFileUpload fileUpload) : base(fileUpload)
        {
            _staffBlackListService = staffBlackListService;
            _staffService = staffService;
            _clientService = clientService;
        }
        public async Task<IActionResult> Index()
        {
            var staffBlackLists = await _staffBlackListService.Get();
            return View(staffBlackLists);
        }

        public async Task<IActionResult> Add()
        {
            var model = new CreateStaffBlackList();
            var staffs = await _staffService.GetStaffs();
            var clients = await _clientService.GetClientDetail();
            model.Staffs = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
            model.Clients = clients.Select(c => new SelectListItem(c.FullName, c.ClientId.ToString())).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateStaffBlackList model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _staffBlackListService.Post(model);
            string msg = "";
            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    msg = content;
                }
                else
                {
                    msg = "An error occurred";
                }
            }

            SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Operation successful" : msg });
            if (!result.IsSuccessStatusCode)
                return View(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var blackList = await _staffBlackListService.Get(id);

            return View(blackList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(GetStaffBlackList model)
        {
            var result = await _staffBlackListService.Delete(model.StaffBlackListId);
            string msg = "";
            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    msg = content;
                }
                else
                {
                    msg = "An error occurred";
                }
            }

            SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Operation successful" : msg });
            if (!result.IsSuccessStatusCode)
                return View(model);

            return RedirectToAction("Index");
            
        }
    }
}