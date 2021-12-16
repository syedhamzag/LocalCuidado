using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffTrainingMatrix;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class StaffTrainingMatrixController : BaseController
    {
        private IStaffTrainingMatrixService _staffTrainingMatrix;
        private IStaffService _staffService;
        private IBaseRecordService _baseRecord;

        public StaffTrainingMatrixController(IStaffTrainingMatrixService staffTrainingMatrix, IFileUpload fileUpload, IStaffService staffService, IBaseRecordService baseRecord) : base(fileUpload)
        {
            _staffTrainingMatrix = staffTrainingMatrix;
            _staffService = staffService;
            _baseRecord = baseRecord;
        }
        public async Task<IActionResult> Reports()
        {
            var entities = await _staffTrainingMatrix.Get();

            var staff = await _staffService.GetStaffs();
            List<CreateStaffTrainingMatrix> reports = new List<CreateStaffTrainingMatrix>();
            foreach (GetStaffTrainingMatrix item in entities)
            {
                var report = new CreateStaffTrainingMatrix();
                report.MatrixId = item.MatrixId;
                report.StaffName = staff.Where(s => s.StaffPersonalInfoId == item.StaffPersonalInfoId).FirstOrDefault().Fullname;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int staffId)
        {
            var model = new CreateStaffTrainingMatrix();
            var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
            var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Staff_Training_Matrix").Select(s => s.BaseRecordItems).FirstOrDefault();
            model.baseRecordList = filterBaseRecord.ToList();
            
            model.StaffPersonalInfoId = staffId;
            model.ListCount = model.baseRecordList.Count;
            var staff = await _staffService.GetStaffs();
            model.StaffName = staff.Where(s => s.StaffPersonalInfoId == staffId).FirstOrDefault().Fullname;
            return View(model);

        }
        //public async Task<IActionResult> View(int homeId)
        //{
        //    var model = await GetstaffTrainingMatrix(homeId);
        //    return View(model);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateStaffTrainingMatrix model, IFormCollection form)
        {
            if(model == null || !ModelState.IsValid)
            {
                var baseRecord = await _baseRecord.GetBaseRecordsWithItems();
                var filterBaseRecord = baseRecord.Where(s => s.KeyName == "Staff_Training_Matrix").Select(s => s.BaseRecordItems).FirstOrDefault();
                model.baseRecordList = filterBaseRecord.ToList();
                return View(model);
            }
            PostStaffTrainingMatrix post = new PostStaffTrainingMatrix();
            List<PostTrainingMatrixList> lists = new List<PostTrainingMatrixList>();

            post.MatrixId = model.MatrixId;
            post.StaffPersonalInfoId = model.StaffPersonalInfoId;

            for (int i = 1; i <= model.ListCount; i++)
            {
                var check = form["chkTraining-" + i];
                if (check.Count > 0)
                { 
                    if (check[0].ToString().Equals("on", StringComparison.InvariantCultureIgnoreCase))
                    { 
                        var date = DateTime.Parse(form["Date-" + i]);
                        var trainingId = int.Parse(form["TrainingId" + i]);
                        PostTrainingMatrixList list = new PostTrainingMatrixList();
                        list.Date = date;
                        list.MatrixId = model.MatrixId;
                        list.TrainingId = trainingId;
                        lists.Add(list);
                    }
                }
            }
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            if (lists.Count > 0)
            {
                result = await _staffTrainingMatrix.Create(post);
                var content = await result.Content.ReadAsStringAsync();
            }

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "StaffTrainingMatrix successfully added to staff" : "An Error Occurred" });
            if(result.IsSuccessStatusCode)
                return RedirectToAction("Details", "Staff", new { staffId = model.StaffPersonalInfoId });
            return RedirectToAction("Index", new { staffId = model.StaffPersonalInfoId });
        }
    }
}
