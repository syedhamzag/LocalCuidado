using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.ManagingTasks;
using AwesomeCare.Admin.ViewModels.CarePlan.PersonalHygiene;
using AwesomeCare.DataTransferObject.DTOs.CarePlanHygiene.ManagingTasks;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<IActionResult> Reports()
        {
            var entities = await _taskService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateManagingTasks> reports = new List<CreateManagingTasks>();
            foreach (GetManagingTasks item in entities)
            {
                var report = new CreateManagingTasks();


                if (reports.Count == 0)
                {
                    report.ClientId = item.ClientId;
                    report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                    report.Status = item.Status;
                    reports.Add(report);
                }
                else
                { 
                    if (reports.FirstOrDefault().ClientId != item.ClientId) 
                    { 
                        report.ClientId = item.ClientId;
                        report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                        report.Status = item.Status;
                        reports.Add(report);
                    }
                }
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateManagingTasks();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            var mtask = await _taskService.Get();
            model.GetManagingTasks = mtask;
            model.TaskCount = mtask.Count();
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateManagingTasks model, IFormCollection formcollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                var mtask = await _taskService.Get();
                model.ClientName = client.Where(s => s.ClientId == model.GetManagingTasks.FirstOrDefault().ClientId).Select(s => s.FullName).FirstOrDefault();
                
                List<CreateManagingTasks> reports = new List<CreateManagingTasks>();
                foreach (var item in mtask)
                {
                    var report = new CreateManagingTasks();
                    report.TaskId = item.TaskId;
                    report.ClientId = item.ClientId;
                    report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                    report.GetManagingTasks.FirstOrDefault().Help = item.Help;
                    report.GetManagingTasks.FirstOrDefault().ClientId = item.ClientId;
                    report.GetManagingTasks.FirstOrDefault().Task = item.Task;
                    report.GetManagingTasks.FirstOrDefault().Status = item.Status;
                    report.GetManagingTasks.FirstOrDefault().TaskId = item.TaskId;
                    reports.Add(report);
                }
                return View(reports);
            }

            List<PostManagingTasks> task = new List<PostManagingTasks>();

            for (int i = 0; i < model.TaskCount; i++)
            {
                var isChecked = formcollection[$"isChecked{i}"];
                if (isChecked.Count > 0 && isChecked[0].ToString().Equals("on", StringComparison.InvariantCultureIgnoreCase))
                { 
                
                    PostManagingTasks post = new PostManagingTasks();
                    var TaskId = int.Parse(formcollection["TaskId"][i]);
                    var Task = int.Parse(formcollection["Task"][i]);
                    var Status = int.Parse(formcollection["Status"][i]);
                    var Help = formcollection["Help"][i].ToString();
                    post.TaskId = TaskId;
                    post.ClientId = model.ClientId;
                    post.Task = Task;
                    post.Status = Status;
                    post.Help = Help;
                    task.Add(post);
                }
            }
            var result = new HttpResponseMessage();
            if (task.FirstOrDefault().TaskId > 0)
            {
                var json = JsonConvert.SerializeObject(task);
                result = await _taskService.Put(task);
                var content = await result.Content.ReadAsStringAsync(); 
            }
            else
            {
                var json = JsonConvert.SerializeObject(task);
                result = await _taskService.Create(task);
                var content = await result.Content.ReadAsStringAsync();
            }

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New ManagingTasks successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        public async Task<IActionResult> View(int clientId)
        {
            CreateManagingTasks model = new CreateManagingTasks();
            model = GetTask(clientId);
            return View(model);
        }

        public CreateManagingTasks GetTask(int clientId)
        {
            var client = _clientService.GetClient(clientId);
            var mtask =  _taskService.Get();

            var putEntity = new CreateManagingTasks
            {
                ClientId = clientId,
                GetManagingTasks = mtask.Result,
            };
            return putEntity;

        }
    }
}