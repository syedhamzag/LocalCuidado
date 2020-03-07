using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.Admin.ViewModels.RotaTask;
using AwesomeCare.DataTransferObject.DTOs.RotaTask;
using Microsoft.AspNetCore.Mvc;
using AwesomeCare.Services.Services;

namespace AwesomeCare.Admin.Controllers
{
    public class RotaTaskController : BaseController
    {

        private readonly IRotaTaskService _rotaTaskService;
        public RotaTaskController(IRotaTaskService rotaTaskService, IFileUpload fileUpload) :base(fileUpload)
        {
            _rotaTaskService = rotaTaskService;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var model = new RotaTaskViewModel();
            var rotaTasks = await _rotaTaskService.Get();
            if (id.HasValue)
            {
                model.SubTitle = "Update Task";
                var rota = await _rotaTaskService.Get(id.Value);
                model.TaskName = rota.TaskName;
                model.GivenAcronym = rota.GivenAcronym;
                model.NotGivenAcronym = model.NotGivenAcronym;
                model.RotaTaskId = rota.RotaTaskId;
                model.Remark = model.Remark;

            }

            model.RotaTasks = rotaTasks;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEdit(RotaTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var rotaTasks = await _rotaTaskService.Get();
                model.RotaTasks = rotaTasks;
                return View(model);
            }

            if (model.RotaTaskId == 0)
            {
                var postRota = new PostRotaTask
                {
                    Deleted = false,
                    Remark = model.Remark,
                    NotGivenAcronym = model.NotGivenAcronym,
                    GivenAcronym = model.GivenAcronym,
                    TaskName = model.TaskName
                };
                var result = await _rotaTaskService.Post(postRota);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Task successfully added" : "An Error Occurred" });

            }
            else
            {
                var putRota = new PutRotaTask
                {
                    Deleted = model.Deleted,
                    Remark = model.Remark,
                    NotGivenAcronym = model.NotGivenAcronym,
                    GivenAcronym = model.GivenAcronym,
                    TaskName = model.TaskName,
                    RotaTaskId = model.RotaTaskId
                };
                var result = await _rotaTaskService.Put(putRota);
                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "Task successfully updated" : "An Error Occurred" });

            }
            return RedirectToAction("Index");
        }

    }
}