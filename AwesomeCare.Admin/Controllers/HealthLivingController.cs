﻿using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.HealthAndLiving;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.ViewModels.CarePlan.Health;
using AwesomeCare.DataTransferObject.DTOs.Health.HealthAndLiving;
using AwesomeCare.Services.Services;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class HealthLivingController : BaseController
    {
        private IHealthAndLivingService _healthAndLivingService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;
        private IStaffService _staffService;

        public HealthLivingController(IHealthAndLivingService healthLivingService, IFileUpload fileUpload, IClientService clientService, IStaffService staffService,
                                      IBaseRecordService baseService) : base(fileUpload)
        {
            _healthAndLivingService = healthLivingService;
            _clientService = clientService;
            _baseService = baseService;
            _staffService = staffService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _healthAndLivingService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateHealthAndLiving> reports = new List<CreateHealthAndLiving>();
            foreach (GetHealthAndLiving item in entities)
            {
                var report = new CreateHealthAndLiving();
                report.HLId = item.HLId;
                report.ContinenceSource = item.ContinenceSource;
                report.EmailName = _baseService.GetBaseRecordItemById(item.Email).Result.ValueName;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.AbilityToReadName = _baseService.GetBaseRecordItemById(item.AbilityToRead).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateHealthAndLiving();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            var getHealth = await _healthAndLivingService.GetbyClient(clientId);
            if (getHealth != null)
            {
                model = GetHealth(getHealth);
            }
            return View(model);

        }
        public async Task<IActionResult> View(int clientId)
        {
            var getHealth = await _healthAndLivingService.GetbyClient(clientId);
            var putEntity = GetHealth(getHealth);
            return View(putEntity);
        }
        public async Task<IActionResult> Edit(int healthLivingId)
        {
            var getHealth = await _healthAndLivingService.Get(healthLivingId);
            var putEntity = GetHealth(getHealth);
            return View(putEntity);
        }
        public async Task<IActionResult> Delete(int clientId)
        {
            var sp = await _healthAndLivingService.GetbyClient(clientId);
            await _healthAndLivingService.Delete(sp.HLId);
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = clientId });
        }
        public CreateHealthAndLiving GetHealth(GetHealthAndLiving getHealth)
        {

            var putEntity = new CreateHealthAndLiving
            {
                AbilityToRead = getHealth.AbilityToRead,
                AlcoholicDrink = getHealth.AlcoholicDrink,
                AllowChats = getHealth.AllowChats,
                BriefHealth = getHealth.BriefHealth,
                CareSupport = getHealth.CareSupport,
                ConstraintAttachment = getHealth.ConstraintAttachment,
                ConstraintDetails = getHealth.ConstraintDetails,
                ConstraintRequired = getHealth.ConstraintRequired,
                ContinenceIssue = getHealth.ContinenceIssue,
                ContinenceNeeds = getHealth.ContinenceNeeds,
                ContinenceSource = getHealth.ContinenceSource,
                DehydrationRisk = getHealth.DehydrationRisk,
                EatingWithStaff = getHealth.EatingWithStaff,
                Email = getHealth.Email,
                FamilyUpdate = getHealth.FamilyUpdate,
                FinanceManagement = getHealth.FinanceManagement,
                ClientId = getHealth.ClientId,
                HLId = getHealth.HLId,
                LaundaryRequired = getHealth.LaundaryRequired,
                LetterOpening = getHealth.LetterOpening,
                LifeStyle = getHealth.LifeStyle,
                MeansOfComm = getHealth.MeansOfComm,
                MovingAndHandling = getHealth.MovingAndHandling,
                NeighbourInvolment = getHealth.NeighbourInvolment,
                ObserveHealth = getHealth.ObserveHealth,
                PostalService = getHealth.PostalService,
                PressureSore = getHealth.PressureSore,
                ShoppingRequired = getHealth.ShoppingRequired,
                Smoking = getHealth.Smoking,
                SpecialCaution = getHealth.SpecialCaution,
                SpecialCleaning = getHealth.SpecialCleaning,
                SupportToBed = getHealth.SupportToBed,
                TeaChocolateCoffee = getHealth.TeaChocolateCoffee,
                TextFontSize = getHealth.TextFontSize,
                TVandMusic = getHealth.TVandMusic,
                VideoCallRequired = getHealth.VideoCallRequired,
                WakeUp = getHealth.WakeUp,
                ActionName = "Update",
                Title = "Update Health And Living"
            };
            return putEntity;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateHealthAndLiving model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }
            PostHealthAndLiving post = new PostHealthAndLiving();

            #region Attachment
            if (model.ConstraintAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_ConstraintAttachment_", DateTime.Now.ToString());
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream(), model.Attach.ContentType);
                model.ConstraintAttachment = path;
            }
            else
            {
                model.ConstraintAttachment = "No Image";
            }
            #endregion

            post.AbilityToRead = model.AbilityToRead;
            post.AlcoholicDrink = model.AlcoholicDrink;
            post.AllowChats = model.AllowChats;
            post.BriefHealth = model.BriefHealth;
            post.CareSupport = model.CareSupport;
            post.ConstraintAttachment = model.ConstraintAttachment;
            post.ConstraintDetails = model.ConstraintDetails;
            post.ConstraintRequired = model.ConstraintRequired;
            post.ContinenceIssue = model.ContinenceIssue;
            post.ContinenceNeeds = model.ContinenceNeeds;
            post.ContinenceSource = model.ContinenceSource;
            post.DehydrationRisk = model.DehydrationRisk;
            post.EatingWithStaff = model.EatingWithStaff;
            post.Email = model.Email;
            post.FamilyUpdate = model.FamilyUpdate;
            post.FinanceManagement = model.FinanceManagement;
            post.ClientId = model.ClientId;
            post.HLId = model.HLId;
            post.LaundaryRequired = model.LaundaryRequired;
            post.LetterOpening = model.LetterOpening;
            post.LifeStyle = model.LifeStyle;
            post.MeansOfComm = model.MeansOfComm;
            post.MovingAndHandling = model.MovingAndHandling;
            post.NeighbourInvolment = model.NeighbourInvolment;
            post.ObserveHealth = model.ObserveHealth;
            post.PostalService = model.PostalService;
            post.PressureSore = model.PressureSore;
            post.ShoppingRequired = model.ShoppingRequired;
            post.Smoking = model.Smoking;
            post.SpecialCaution = model.SpecialCaution;
            post.SpecialCleaning = model.SpecialCleaning;
            post.SupportToBed = model.SupportToBed;
            post.TeaChocolateCoffee = model.TeaChocolateCoffee;
            post.TextFontSize = model.TextFontSize;
            post.TVandMusic = model.TVandMusic;
            post.VideoCallRequired = model.VideoCallRequired;
            post.WakeUp = model.WakeUp;
            
            var result = new HttpResponseMessage();
            if (post.HLId > 0)
            {
                result = await _healthAndLivingService.Put(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            else
            {
                result = await _healthAndLivingService.Post(post);
                var content = await result.Content.ReadAsStringAsync();
            }
            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Blood Pressure successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateHealthAndLiving model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            #region Status Attachment
            if (model.ConstraintAttachment != null)
            {
                string folder = "clientcomplain";
                string filename = string.Concat(folder, "_ConstraintAttachment_", DateTime.Now.ToString());
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Attach.OpenReadStream(), model.Attach.ContentType);
                model.ConstraintAttachment = path;
            }
            else
            {
                model.ConstraintAttachment = model.ConstraintAttachment;
            }
            #endregion
            PostHealthAndLiving put = new PostHealthAndLiving();
            
            put.AbilityToRead = model.AbilityToRead;
            put.AlcoholicDrink = model.AlcoholicDrink;
            put.AllowChats = model.AllowChats;
            put.BriefHealth = model.BriefHealth;
            put.CareSupport = model.CareSupport;
            put.ConstraintAttachment = model.ConstraintAttachment;
            put.ConstraintDetails = model.ConstraintDetails;
            put.ConstraintRequired = model.ConstraintRequired;
            put.ContinenceIssue = model.ContinenceIssue;
            put.ContinenceNeeds = model.ContinenceNeeds;
            put.ContinenceSource = model.ContinenceSource;
            put.DehydrationRisk = model.DehydrationRisk;
            put.EatingWithStaff = model.EatingWithStaff;
            put.Email = model.Email;
            put.FamilyUpdate = model.FamilyUpdate;
            put.FinanceManagement = model.FinanceManagement;
            put.ClientId = model.ClientId;
            put.HLId = model.HLId;
            put.LaundaryRequired = model.LaundaryRequired;
            put.LetterOpening = model.LetterOpening;
            put.LifeStyle = model.LifeStyle;
            put.MeansOfComm = model.MeansOfComm;
            put.MovingAndHandling = model.MovingAndHandling;
            put.NeighbourInvolment = model.NeighbourInvolment;
            put.ObserveHealth = model.ObserveHealth;
            put.PostalService = model.PostalService;
            put.PressureSore = model.PressureSore;
            put.ShoppingRequired = model.ShoppingRequired;
            put.Smoking = model.Smoking;
            put.SpecialCaution = model.SpecialCaution;
            put.SpecialCleaning = model.SpecialCleaning;
            put.SupportToBed = model.SupportToBed;
            put.TeaChocolateCoffee = model.TeaChocolateCoffee;
            put.TextFontSize = model.TextFontSize;
            put.TVandMusic = model.TVandMusic;
            put.VideoCallRequired = model.VideoCallRequired;
            put.WakeUp = model.WakeUp;

            var entity = await _healthAndLivingService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);

        }
        public async Task<IActionResult> Download(int id)
        {
            var entity = await GetDownload(id);
            var clients = await _clientService.GetClientDetail();
            var client = clients.Where(s => s.ClientId == entity.ClientId).FirstOrDefault();
            MemoryStream stream = _fileUpload.DownloadClientFile(entity);
            stream.Position = 0;
            string fileName = $"{client.FullName}.docx";
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
        public async Task<CreateHealthAndLiving> GetDownload(int Id)
        {
            var i = await _healthAndLivingService.Get(Id);
            var staff = await _staffService.GetStaffs();
            var client = await _clientService.GetClientDetail();

            var putEntity = new CreateHealthAndLiving
            {
                ClientId = i.ClientId,
                ClientName = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().FullName,
                IdNumber = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().IdNumber,
                DOB = client.Where(s => s.ClientId == i.ClientId).FirstOrDefault().DateOfBirth,
                BriefHealth = i.BriefHealth,
                ObserveHealth = i.ObserveHealth,
                WakeUp = i.WakeUp,
                CareSupport = i.CareSupport,
                MovingAndHandling = i.MovingAndHandling,
                SupportToBed = i.SupportToBed,
                ContinenceNeeds = i.ContinenceNeeds,
                ContinenceSource = i.ContinenceSource,
                ConstraintDetails = i.ConstraintDetails,
                ConstraintAttachment = i.ConstraintAttachment,
                SpecialCaution = i.SpecialCaution,
                EmailName = _baseService.GetBaseRecordItemById(i.Email).Result.ValueName,
                DehydrationRiskName = _baseService.GetBaseRecordItemById(i.DehydrationRisk).Result.ValueName,
                LifeStyleName = _baseService.GetBaseRecordItemById(i.LifeStyle).Result.ValueName,
                PressureSoreName = _baseService.GetBaseRecordItemById(i.PressureSore).Result.ValueName,
                ContinenceIssueName = _baseService.GetBaseRecordItemById(i.ContinenceIssue).Result.ValueName,
                ConstraintRequiredName = _baseService.GetBaseRecordItemById(i.ConstraintRequired).Result.ValueName,
                MeansOfCommName = _baseService.GetBaseRecordItemById(i.MeansOfComm).Result.ValueName,
                SmokingName = _baseService.GetBaseRecordItemById(i.Smoking).Result.ValueName,
                TextFontSizeName = _baseService.GetBaseRecordItemById(i.TextFontSize).Result.ValueName,
                FinanceManagementName = _baseService.GetBaseRecordItemById(i.FinanceManagement).Result.ValueName,
                PostalServiceName = _baseService.GetBaseRecordItemById(i.PostalService).Result.ValueName,
                LetterOpeningName = _baseService.GetBaseRecordItemById(i.LetterOpening).Result.ValueName,
                ShoppingRequiredName = _baseService.GetBaseRecordItemById(i.ShoppingRequired).Result.ValueName,
                SpecialCleaningName = _baseService.GetBaseRecordItemById(i.SpecialCleaning).Result.ValueName,
                LaundaryRequiredName = _baseService.GetBaseRecordItemById(i.LaundaryRequired).Result.ValueName,
                VideoCallRequiredName = _baseService.GetBaseRecordItemById(i.VideoCallRequired).Result.ValueName,
                EatingWithStaffName = _baseService.GetBaseRecordItemById(i.EatingWithStaff).Result.ValueName,
                AllowChatsName = _baseService.GetBaseRecordItemById(i.AllowChats).Result.ValueName,
                TeaChocolateCoffeeName = _baseService.GetBaseRecordItemById(i.TeaChocolateCoffee).Result.ValueName,
                NeighbourInvolmentName = _baseService.GetBaseRecordItemById(i.NeighbourInvolment).Result.ValueName,
                FamilyUpdateName = _baseService.GetBaseRecordItemById(i.FamilyUpdate).Result.ValueName,
                AlcoholicDrinkName = _baseService.GetBaseRecordItemById(i.AlcoholicDrink).Result.ValueName,
                TVandMusicName = _baseService.GetBaseRecordItemById(i.TVandMusic).Result.ValueName,
            };
            return putEntity;
        }
    }
}
