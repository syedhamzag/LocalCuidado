using AwesomeCare.Admin.Services.Admin;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.SpecialHealthAndMedication;
using AwesomeCare.Admin.ViewModels.CarePlan.Health;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthAndMedication;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class SpecialHealthAndMedicationController : BaseController
    {
        private ISpecialHealthAndMedicationService _spmedsService;
        private IClientService _clientService;
        private IBaseRecordService _baseService;

        public SpecialHealthAndMedicationController(ISpecialHealthAndMedicationService spmedsService, IFileUpload fileUpload, IClientService clientService,
            IBaseRecordService baseService) : base(fileUpload)
        {
            _spmedsService = spmedsService;
            _clientService = clientService;
            _baseService = baseService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _spmedsService.Get();

            var client = await _clientService.GetClientDetail();
            List<CreateSpecialHealthAndMedication> reports = new List<CreateSpecialHealthAndMedication>();
            foreach (GetSpecialHealthAndMedication item in entities)
            {
                var report = new CreateSpecialHealthAndMedication();
                report.SHMId = item.SHMId;
                report.ClientName = client.Where(s => s.ClientId == item.ClientId).Select(s => s.FullName).FirstOrDefault();
                report.WhoAdminName = _baseService.GetBaseRecordItemById(item.WhoAdminister).Result.ValueName;
                report.MedicationAllergyName = _baseService.GetBaseRecordItemById(item.MedicationAllergy).Result.ValueName;
                reports.Add(report);
            }
            return View(reports);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateSpecialHealthAndMedication();
            model.ClientId = clientId;
            var client = await _clientService.GetClientDetail();
            model.ClientName = client.Where(s => s.ClientId == clientId).FirstOrDefault().FullName;
            return View(model);

        }
        public async Task<IActionResult> View(int SpecialHealthAndMedicationId)
        {
            var putEntity = await GetSpecialHealthAndMedication(SpecialHealthAndMedicationId);
            return View(putEntity);
        }
        public async Task<IActionResult> Edit(int SpecialHealthAndMedicationId)
        {
            var putEntity = await GetSpecialHealthAndMedication(SpecialHealthAndMedicationId);
            return View(putEntity);
        }
        public async Task<CreateSpecialHealthAndMedication> GetSpecialHealthAndMedication(int SpecialHealthAndMedicationId)
        {
            var sp = await _spmedsService.Get(SpecialHealthAndMedicationId);
            var putEntity = new CreateSpecialHealthAndMedication
            {
                AccessMedication = sp.AccessMedication,
                AdminLvl = sp.AdminLvl,
                By = sp.By,
                ClientId = sp.ClientId,
                Consent = sp.Consent,
                Date = sp.Date,
                FamilyMeds = sp.FamilyMeds,
                MedKeyCode = sp.MedKeyCode,
                MedicationAllergy = sp.MedicationAllergy,
                SHMId = sp.SHMId,
                LeftoutMedicine = sp.LeftoutMedicine,
                MedAccessDenial = sp.MedAccessDenial,
                MedicationStorage = sp.MedicationStorage,
                NameFormMedicaiton = sp.NameFormMedicaiton,
                PharmaMARChart = sp.PharmaMARChart,
                PNRDoses = sp.PNRDoses,
                PNRMedsMissing = sp.PNRMedsMissing,
                SpecialStorage = sp.SpecialStorage,
                NoMedAccess = sp.NoMedAccess,
                MedsGPOrder = sp.MedsGPOrder,
                WhoAdminister = sp.WhoAdminister,
                PNRMedsAdmin = sp.PNRMedsAdmin,
                PNRMedList = sp.PNRMedList,
                OverdoseContact = sp.OverdoseContact,
                TempMARChart = sp.TempMARChart,
                FamilyReturnMed = sp.FamilyReturnMed,
                PNRMedReq = sp.PNRMedReq,
                Type = sp.Type,
            };
            return putEntity;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateSpecialHealthAndMedication model)
        {
            if (model == null || !ModelState.IsValid)
            {
                var client = await _clientService.GetClientDetail();
                model.ClientName = client.Where(s => s.ClientId == model.ClientId).Select(s => s.FullName).FirstOrDefault();
                return View(model);
            }
            PostSpecialHealthAndMedication post = new PostSpecialHealthAndMedication();

            post.AccessMedication = model.AccessMedication;
            post.AdminLvl = model.AdminLvl;
            post.By = model.By;
            post.ClientId = model.ClientId;
            post.Consent = model.Consent;
            post.Date = model.Date;
            post.FamilyMeds = model.FamilyMeds;
            post.MedKeyCode = model.MedKeyCode;
            post.MedicationAllergy = model.MedicationAllergy;
            post.SHMId = model.SHMId;
            post.LeftoutMedicine = model.LeftoutMedicine;
            post.MedAccessDenial = model.MedAccessDenial;
            post.MedicationStorage = model.MedicationStorage;
            post.NameFormMedicaiton = model.NameFormMedicaiton;
            post.PharmaMARChart = model.PharmaMARChart;
            post.PNRDoses = model.PNRDoses;
            post.PNRMedsMissing = model.PNRMedsMissing;
            post.SpecialStorage = model.SpecialStorage;
            post.NoMedAccess = model.NoMedAccess;
            post.MedsGPOrder = model.MedsGPOrder;
            post.WhoAdminister = model.WhoAdminister;
            post.PNRMedsAdmin = model.PNRMedsAdmin;
            post.PNRMedList = model.PNRMedList;
            post.OverdoseContact = model.OverdoseContact;
            post.TempMARChart = model.TempMARChart;
            post.FamilyReturnMed = model.FamilyReturnMed;
            post.PNRMedReq = model.PNRMedReq;
            post.Type = model.Type;

            var result = await _spmedsService.Create(post);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Blood Pressure successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateSpecialHealthAndMedication model)
        {
            if (!ModelState.IsValid)
            {
                var client = await _clientService.GetClient(model.ClientId);
                model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
                return View(model);
            }

            PutSpecialHealthAndMedication put = new PutSpecialHealthAndMedication();

            put.AccessMedication = model.AccessMedication;
            put.AdminLvl = model.AdminLvl;
            put.By = model.By;
            put.ClientId = model.ClientId;
            put.Consent = model.Consent;
            put.Date = model.Date;
            put.FamilyMeds = model.FamilyMeds;
            put.MedKeyCode = model.MedKeyCode;
            put.MedicationAllergy = model.MedicationAllergy;
            put.SHMId = model.SHMId;
            put.LeftoutMedicine = model.LeftoutMedicine;
            put.MedAccessDenial = model.MedAccessDenial;
            put.MedicationStorage = model.MedicationStorage;
            put.NameFormMedicaiton = model.NameFormMedicaiton;
            put.PharmaMARChart = model.PharmaMARChart;
            put.PNRDoses = model.PNRDoses;
            put.PNRMedsMissing = model.PNRMedsMissing;
            put.SpecialStorage = model.SpecialStorage;
            put.NoMedAccess = model.NoMedAccess;
            put.MedsGPOrder = model.MedsGPOrder;
            put.WhoAdminister = model.WhoAdminister;
            put.PNRMedsAdmin = model.PNRMedsAdmin;
            put.PNRMedList = model.PNRMedList;
            put.OverdoseContact = model.OverdoseContact;
            put.TempMARChart = model.TempMARChart;
            put.FamilyReturnMed = model.FamilyReturnMed;
            put.PNRMedReq = model.PNRMedReq;
            put.Type = model.Type;

            var entity = await _spmedsService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }
    }
}
