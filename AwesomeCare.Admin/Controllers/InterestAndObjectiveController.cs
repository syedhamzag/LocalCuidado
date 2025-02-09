﻿using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.InterestAndObjective;
using AwesomeCare.Admin.ViewModels.CarePlan.InterestObjective;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.PersonalityTest;
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
    public class InterestAndObjectiveController : BaseController
    { 
        private IClientService _clientService;
        private IInterestAndObjectiveService _interestService;
        public InterestAndObjectiveController(IFileUpload fileUpload, IClientService clientService, IInterestAndObjectiveService interestService) : base(fileUpload)
        {
            _clientService = clientService;
            _interestService = interestService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _interestService.Get();
            var client = await _clientService.GetClientDetail();

            List<CreateInterestAndObjective> reports = new List<CreateInterestAndObjective>();
            foreach (GetInterestAndObjective item in entities)
            {
                var report = new CreateInterestAndObjective();
                report.GoalId = item.GoalId;
                report.ClientId = item.ClientId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var client = await _clientService.GetClientDetail();
            var interest = await _interestService.Get(clientId);
            var model = new CreateInterestAndObjective();
            model.GetInterest = new List<GetInterest>();
            model.GetPersonalityTest = new List<GetPersonalityTest>();
            model.ClientId = clientId;
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            if (interest != null) 
            { 
                model = Get(clientId);
            }
            return View(model);
        }
        [HttpGet]
        public JsonResult Delete(int Id, string name)
        {
                var delEntity = _interestService.Delete(Id,name);
                return Json(delEntity.Result);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateInterestAndObjective model, IFormCollection formcollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }


            PostInterestAndObjective post = new PostInterestAndObjective();

            post.GoalId = model.GoalId;
            post.ClientId = model.ClientId;
            post.CareGoal = model.CareGoal;
            post.Brief = model.Brief;

            List<PostInterest> obj1 = new List<PostInterest>();
            List<PostPersonalityTest> ptest = new List<PostPersonalityTest>();

            #region Interest

            for (int i = 0; i < model.InterestCount; i++)
            {
                PostInterest eq = new PostInterest();
                var interest = int.Parse(formcollection["InterestId"][i]);
                var Leisure = int.Parse(formcollection["LeisureActivity"][i]);
                var Informal = int.Parse(formcollection["InformalActivity"][i]);
                var Contact = int.Parse(formcollection["MaintainContact"][i]);
                var Community = int.Parse(formcollection["CommunityActivity"][i]);
                var Event = int.Parse(formcollection["EventAwarness"][i]);
                var Objective = int.Parse(formcollection["GoalAndObjective"][i]);

                eq.GoalId = model.GoalId;
                eq.InterestId = interest;
                eq.LeisureActivity = Leisure;
                eq.InformalActivity = Informal;
                eq.MaintainContact = Contact;
                eq.CommunityActivity = Community;
                eq.EventAwarness = Event;
                eq.GoalAndObjective = Objective;

                obj1.Add(eq);
            }
            #endregion

            #region PersonalityTest
            for (int i = 0; i < model.PersonalityCount; i++)
            {
                PostPersonalityTest test = new PostPersonalityTest();

                var TestId = int.Parse(formcollection["TestId"][i]);
                var Question = int.Parse(formcollection["Question"][i]);
                var Answr = int.Parse(formcollection["Answer"][i]);

                test.TestId = TestId;
                test.Question = Question;
                test.Answer = Answr;
                test.GoalId = model.GoalId;

                ptest.Add(test);
            }
            #endregion

            post.Interest = obj1;
            post.PersonalityTest = ptest;

            var result = new HttpResponseMessage();
            if (obj1.FirstOrDefault().GoalId > 0)
            {
                var json = JsonConvert.SerializeObject(post);
                result = await _interestService.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                var json = JsonConvert.SerializeObject(post);
                result = await _interestService.Create(post);
                var content = await result.Content.ReadAsStringAsync();
            }

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Balance successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        public IActionResult View(int clientId)
        {
            var model = Get(clientId);
            return View(model);
        }

        public CreateInterestAndObjective Get(int clientId)
        {
            var obj =  _interestService.Get(clientId);
            var client = _clientService.GetClient(clientId);

            var putEntity = new CreateInterestAndObjective
            {
                Title = "Update Interest And Objective",
                ActionName = "Update",
                #region Personal Details
                GoalId = obj.Result.GoalId,
                ClientId = clientId,
                CareGoal = obj.Result.CareGoal,
                Brief = obj.Result.Brief,
                #endregion

                #region Lists
                GetInterest = obj.Result.Interest,
                GetPersonalityTest = obj.Result.PersonalityTest,
                PersonalityCount = obj.Result.PersonalityTest.Count,
                InterestCount = obj.Result.Interest.Count,
                #endregion

            };
            return putEntity;

        }


    }
}
