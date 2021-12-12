using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.BestInterestAssessment;
using AwesomeCare.DataTransferObject.DTOs.BestInterestAssessment;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Staff;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Models;
using Microsoft.AspNetCore.Http;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.Services.Services;

namespace AwesomeCare.Admin.Controllers
{
    public class BestInterestAssessmentController : BaseController
    {
        private IBestInterestAssessmentService _BestInterestAssessment;
        private IClientService _clientService;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public BestInterestAssessmentController(IStaffService staffService, IBestInterestAssessmentService BestInterestAssessment, IFileUpload fileUpload, IClientService clientService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _BestInterestAssessment = BestInterestAssessment;
            _clientService = clientService;
            _baseRecord = baseRecord;
            _staffService = staffService;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _BestInterestAssessment.Get();

            var client = await _clientService.GetClientDetail();
            var staff = await _staffService.GetStaffs();
            List<CreateBestInterestAssessment> reports = new List<CreateBestInterestAssessment>();
            foreach (GetBestInterestAssessment item in entities)
            {
                var report = new CreateBestInterestAssessment();
                report.BestId = item.BestId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).FirstOrDefault().FullName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateBestInterestAssessment();

            var client = await _clientService.GetClientDetail();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "MCA_Care_Issues").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            model.ClientId = clientId;
            return View(model);

        }
        public async Task<IActionResult> View(int teamleadId)
        {
            var model = new CreateBestInterestAssessment();

            var client = await _clientService.GetClientDetail();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "MCA_Care_Issues").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBestInterestAssessment model, IFormCollection formcollection)
        {
            var client = await _clientService.GetClientDetail();
            var staff = await _staffService.GetStaffs();
            if (model == null || !ModelState.IsValid)
            {
                model.ClientList = client.Select(s => new SelectListItem(s.FullName, s.ClientId.ToString())).ToList();
                return View(model);

            }
            List<PostBelieveTask> tasks = new List<PostBelieveTask>();
            for (int i = 0; i < model.BelieveTaskCount; i++)
            {
                PostBelieveTask task = new PostBelieveTask();
                var TaskId = int.Parse(formcollection["BelieveTaskId"][i]);
                var Title = formcollection["ReasonableBelieve"][i];
                var Comment = formcollection["Comments"][i];
                var Status = formcollection["Status"][i];
                task.BestId = model.BestId;
                task.BelieveTaskId = TaskId;
                task.ReasonableBelieve = Title;
                tasks.Add(task);
            }
            PostBestInterestAssessment post = new PostBestInterestAssessment();
            post.BestId = model.BestId;
            post.ClientId = model.ClientId;
            post.Date = model.Date;
            post.Name = model.Name;
            post.Position = model.Position;
            post.Signature = model.Signature;
            post.PostBelieveTask = tasks;

            var result = await _BestInterestAssessment.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "BestInterestAssessment successfully added" : "An Error Occurred" });

            return RedirectToAction("Reports");
        }
    }
}
