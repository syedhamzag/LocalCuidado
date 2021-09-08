using AwesomeCare.Admin.Services.Client;
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

        public async Task<IActionResult> Index(int clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreateInterestAndObjective();
            model.GetInterest = new List<GetInterest>();
            model.GetPersonalityTest = new List<GetPersonalityTest>();
            model.ClientId = clientId;
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);
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

            List<PostInterest> obj = new List<PostInterest>();
            List<PostPersonalityTest> ptest = new List<PostPersonalityTest>();

            #region Interest

            for (int i = 0; i < model.InterestCount; i++)
            {
                PostInterest eq = new PostInterest();

                var Leisure = int.Parse(formcollection["LeisureActivity"][i]);
                var Informal = int.Parse(formcollection["InformalActivity"][i]);
                var Contact = int.Parse(formcollection["MaintainContact"][i]);
                var Community = int.Parse(formcollection["CommunityActivity"][i]);
                var Event = int.Parse(formcollection["EventAwarness"][i]);
                var Objective = int.Parse(formcollection["GoalAndObjective"][i]);
                var Brief = formcollection["Brief"][i].ToString();

                eq.GoalId = model.GoalId;
                eq.LeisureActivity = Leisure;
                eq.InformalActivity = Informal;
                eq.MaintainContact = Contact;
                eq.CommunityActivity = Community;
                eq.EventAwarness = Event;
                eq.GoalAndObjective = Objective;

                obj.Add(eq);
            }
            #endregion

            #region PersonalityTest
            for (int i = 0; i < model.PersonalityCount; i++)
            {
                PostPersonalityTest test = new PostPersonalityTest();
                var Question = int.Parse(formcollection["Question"][i]);
                var Answr = int.Parse(formcollection["Answer"]);

                test.Question = Question;
                test.Answer = Answr;
                test.GoalId = model.GoalId;

                ptest.Add(test);
            }
            #endregion

            post.Interest = obj;
            post.PersonalityTest = ptest;

            var json = JsonConvert.SerializeObject(post);
            var result = await _interestService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Balance successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }


    }
}
