using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ManagingTasks;
using AwesomeCare.Admin.ViewModels.CarePlan.PersonalHygiene;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class ManagingTasksController : BaseController
    {
        private IManagingTasksService _taskService;
        private IClientService _clientService;

        public ManagingTasksController(IManagingTasksService ManagingTasksService, IFileUpload fileUpload, IClientService clientService) : base(fileUpload)
        {
            _taskService = ManagingTasksService;
            _clientService = clientService;
        }

        //public async Task<IActionResult> Reports()
        //{
        //    var entities = await _taskService.Get();

        //    var client = await _clientService.GetClientDetail();
        //    List<CreateManagingTasks> reports = new List<CreateManagingTasks>();
        //    foreach (GetManagingTasks item in entities)
        //    {
        //        var report = new CreateManagingTasks();
        //        report.TaskId = item.TaskId;
        //        report.Task = item.Task;
        //        report.Help = item.Help;
        //        report.Status = item.Status;
        //        report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
        //        report.Status = item.Status;
        //        reports.Add(report);
        //    }
        //    return View(reports);
        //}

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateManagingTasks();
            model.GetManagingTasks.FirstOrDefault().ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);

        }

        //public async Task<IActionResult> View(int ManagingTasksId)
        //{
        //    var putEntity = await GetManagingTasks(ManagingTasksId);
        //    return View(putEntity);
        //}

        //public async Task<IActionResult> Edit(int ManagingTasksId)
        //{
        //    var putEntity = await GetManagingTasks(ManagingTasksId);
        //    return View(putEntity);
        //}

        //public async Task<CreateManagingTasks> GetManagingTasks(int ManagingTasksId)
        //{
        //    var ManagingTasks = await _taskService.Get(ManagingTasksId);
        //    var putEntity = new CreateManagingTasks
        //    {
        //        Task = ManagingTasks.Task,
        //        Help = ManagingTasks.Help,
        //        TaskId = ManagingTasks.TaskId,
        //        ClientId = ManagingTasks.ClientId,
        //        Status = ManagingTasks.Status,
        //    };
        //    return putEntity;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateManagingTasks model, IFormCollection formcollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.GetManagingTasks.FirstOrDefault().ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }

            List<PostManagingTasks> task = new List<PostManagingTasks>();

            for (int i = 0; i < model.TaskCount; i++)
            {
                PostManagingTasks post = new PostManagingTasks();
                var Task = int.Parse(formcollection["Task"][i]);

                var Status = int.Parse(formcollection["Status"][i]);
                var Help = int.Parse(formcollection["Help"][i]);

                post.Task = Task;
                post.Status = Status;
                //post.Help = Help;
                task.Add(post);
            }

            //var result = await _taskService.Create(task);
            //var content = await result.Content.ReadAsStringAsync();

            //SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New ManagingTasks successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.GetManagingTasks.FirstOrDefault().ClientId });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(CreateManagingTasks model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var client = await _clientService.GetClient(model.ClientId);
        //        model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
        //        return View(model);
        //    }

        //    PutManagingTasks put = new PutManagingTasks();

        //    put.TaskId = model.TaskId;
        //    put.ClientId = model.ClientId;
        //    put.Help = model.Help;
        //    put.Task = model.Task;
        //    put.Status = model.Status;

        //    var entity = await _taskService.Put(put);
        //    SetOperationStatus(new Models.OperationStatus
        //    {
        //        IsSuccessful = entity.IsSuccessStatusCode,
        //        Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
        //    });
        //    if (entity.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Reports");
        //    }
        //    return View(model);

        //}
    }
}
