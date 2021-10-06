using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AwesomeCare.Admin.Extensions;
using AwesomeCare.Admin.Services.ClientInvolvingParty;
using AwesomeCare.DataTransferObject.DTOs.ClientInvolvingParty;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.RegulatoryContact;
using AwesomeCare.Admin.Services.ClientRegulatoryContact;
using QRCoder;
using System.Drawing;
using Dropbox.Api;
using System.IO;
using AwesomeCare.Admin.Services.ClientCareDetails;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetails;
using Newtonsoft.Json;
using AwesomeCare.Services.Services;
using AwesomeCare.Admin.Services.Medication;
using AwesomeCare.DataTransferObject.DTOs.Medication;
using AwesomeCare.Admin.Services.ClientRotaType;
using AwesomeCare.Admin.Services.RotaDayofWeek;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.DataTransferObject.DTOs.MedicationManufacturer;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationDay;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationPeriod;
using AwesomeCare.DataTransferObject.DTOs.ClientMedication;
using AwesomeCare.Admin.Services.Admin;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IClientInvolvingParty _clientInvolvingPartyService;
        private readonly IClientRegulatoryContactService _clientRegulatoryContactService;
        private readonly IClientCareDetails _clientCareDetails;
        private readonly IWebHostEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;
        private readonly QRCodeGenerator _qRCodeGenerator;
        private readonly DropboxClient _dropboxClient;
        private readonly IMedicationService _medicationService;
        private readonly IClientRotaTypeService _clientRotaTypeService;
        private readonly IRotaDayofWeekService _rotaDayofWeekService;
        private readonly IBaseRecordService _baseRecordService;

        public ClientController(DropboxClient dropboxClient, IFileUpload fileUpload, QRCodeGenerator qRCodeGenerator, IMemoryCache cache,
            IClientRegulatoryContactService clientRegulatoryContactService, IClientInvolvingParty clientInvolvingPartyService,
            IClientService clientService, IWebHostEnvironment env, ILogger<ClientController> logger, IBaseRecordService baseRecordService,
            IClientCareDetails clientCareDetails, IMedicationService medicationService, IClientRotaTypeService clientRotaTypeService, IRotaDayofWeekService rotaDayofWeekService) : base(fileUpload)
        {
            _dropboxClient = dropboxClient;
            _baseRecordService = baseRecordService;
            _clientService = clientService;
            _clientInvolvingPartyService = clientInvolvingPartyService;
            _env = env;
            _logger = logger;
            _cache = cache;
            _clientRegulatoryContactService = clientRegulatoryContactService;
            _qRCodeGenerator = qRCodeGenerator;
            _clientCareDetails = clientCareDetails;
            _medicationService = medicationService;
            _clientRotaTypeService = clientRotaTypeService;
            _rotaDayofWeekService = rotaDayofWeekService;
        }
        public async Task<IActionResult> HomeCare()
        {
            var result = await _clientService.GetClients();
            var active = result.Where(s => s.Status == "Active").OrderBy(s=>s.ClientId).ToList();
            return View(active);
        }
        public async Task<IActionResult> HomeCareOther()
        {
            var result = await _clientService.GetClients();
            var active = result.Where(s => s.Status != "Active").OrderBy(s => s.ClientId).ToList();
            return View(active);
        }
        #region Registration


        public async Task<IActionResult> HomeCareRegistration()
        {
            List<ClientInvolvingParty> clientInvolvingPartyItems = new List<ClientInvolvingParty>();
            List<ClientCareDetailsHeading> careDetailsItems = new List<ClientCareDetailsHeading>();

            var client = new CreateClient();
            var involvingPartyItems = await _clientService.GetClientInvolvingPartyBase();
            var clientCareDetails = await _clientCareDetails.GetHeadingsWithTasks();
            foreach (var item in involvingPartyItems)
            {
                clientInvolvingPartyItems.Add(new ClientInvolvingParty
                {
                    ClientInvolvingPartyItemId = item.ClientInvolvingPartyItemId,
                    ItemName = item.ItemName,
                    Description = item.Description,
                    Deleted = item.Deleted
                });
            }
            var careDetails = clientCareDetails.Where(c => c.Tasks.Count > 0).ToList();
            foreach (var item in careDetails)
            {
                careDetailsItems.Add(new ClientCareDetailsHeading
                {
                    CareDetailsHeadingId = item.ClientCareDetailsHeadingId,
                    Heading = item.Heading,
                    // Header = item.Heading.Replace(" ",""),
                    Tasks = (from tk in item.Tasks
                             select new ClientCareDetailsTask
                             {
                                 Task = tk.Task,
                                 CareDetailsTaskId = tk.ClientCareDetailsTaskId,
                                 CareDetailsHeadingId = item.ClientCareDetailsHeadingId
                             }).ToList()
                });
            }
            HttpContext.Session.Set<List<ClientInvolvingParty>>("involvingPartyItems", clientInvolvingPartyItems);
            HttpContext.Session.Set<List<ClientCareDetailsHeading>>("caredetailsItems", careDetailsItems);
            client.InvolvingParties = clientInvolvingPartyItems;
            client.CareDetails = careDetailsItems;
            #region Regulatory Contact
            if (_cache.TryGetValue(cacheKey, out List<GetBaseRecordWithItems> baseRecords))
            {

                client.RegulatoryContacts = (from rec in baseRecords
                                             where rec.KeyName == "Client_RegulatoryContact"
                                             from recItem in rec.BaseRecordItems
                                             select new ClientRegulatoryContact
                                             {
                                                 BaseRecordItemId = recItem.BaseRecordItemId,
                                                 RegulatoryContact = recItem.ValueName
                                             }).ToList();
            }
            #endregion
            return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HomeCareRegistration(CreateClient model)
        {
            try
            {
                model.StatusId = 14;

                if (model == null || !ModelState.IsValid)
                {
                    //model.InvolvingParties = HttpContext.Session.Get<List<ClientInvolvingParty>>("involvingPartyItems");

                    return View(model);
                }


                #region PersonalInfo
                string folder = "clientpassport";
                string filename = string.Concat(folder, "_", model.Telephone);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.ClientImage.OpenReadStream());

                model.PassportFilePath = path;
                #endregion

                #region Involving Parties
                var involvingParties = InvolvingParty(model);
                model.InvolvingParties = involvingParties;
                #endregion

                #region Regulatory Contact
                var regulatoryContact = await RegulatoryContact(model);
                model.RegulatoryContacts = regulatoryContact;
                #endregion

                var postClient = Mapper.Map<PostClient>(model);
                #region CareDetails
                var careDetails = CareDetails(model);
                postClient.CareDetails = careDetails;
                #endregion
                var json = JsonConvert.SerializeObject(postClient);
                var result = await _clientService.PostClient(postClient);


                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Client successfully registered" : "An Error Occurred" });
                return RedirectToAction("HomeCareDetails", new { clientId = result.ClientId });
            }
            catch (Refit.ApiException e)
            {
                var error = e.Content;
                if (e.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    SetOperationStatus(new OperationStatus { IsSuccessful = false, Message = error });
                }
                else if (e.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    SetOperationStatus(new OperationStatus { IsSuccessful = false, Message = error });
                }
                _logger.LogError(e, "HomeCareRegistration", null);
                return View("HomeCareRegistration", model);
            }
            catch (Exception ex)
            {
                SetOperationStatus(new OperationStatus { IsSuccessful = false, Message = "An Error Occurred" });
                _logger.LogError(ex, "HomeCareRegistration", null);
                return View("HomeCareRegistration", model);
            }

            // return RedirectToAction("HomeCare");
        }

        List<ClientInvolvingParty> InvolvingParty(CreateClient model)
        {
            var items = model.InvolvingParties.Where(s => s.IsSelected).ToList();
            //  var involvingParties = Mapper.Map<List<PostClientInvolvingParty>>(items);

            return items;
        }
        async Task<List<ClientRegulatoryContact>> RegulatoryContact(CreateClient createClient)
        {
            var items = createClient.RegulatoryContacts.Where(s => s.IsSelected).ToList();
            foreach (var c in items)
            {

                string folder = "clientregulatorycontact";
                string filename = string.Concat(folder, "_", c.RegulatoryContact, "_", createClient.Telephone);
                string path = await _fileUpload.UploadFile(folder, true, filename, c.EvidenceFile.OpenReadStream());

                c.Evidence = path;
            }


            //  var regulatoryContacts = Mapper.Map<List<PostClientRegulatoryContact>>(items);

            return items;
        }

        List<PostClientCareDetails> CareDetails(CreateClient createClient)
        {
            var clientCareDetails = (from cl in createClient.CareDetails
                                     from tk in cl.Tasks
                                     where tk.IsSelected
                                     select new PostClientCareDetails
                                     {
                                         ClientCareDetailsTaskId = tk.CareDetailsTaskId,
                                         Description = tk.Description,
                                         Location = tk.Location,
                                         Mitigation = tk.Mitigation,
                                         Remark = tk.Remark,
                                         Risk = tk.Risk
                                     }).ToList();

            return clientCareDetails;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _InvolvingParty(CreateClient createClient)
        {
            var items = createClient.InvolvingParties.Where(s => s.IsSelected).ToList();
            items.ForEach(c =>
            {
                c.ClientId = createClient.ClientId;
            });
            var involvingParties = Mapper.Map<List<PostClientInvolvingParty>>(items);
            var result = await _clientInvolvingPartyService.Post(involvingParties);
            if (!result.IsSuccessStatusCode)
            {
                createClient.ActiveTab = "involvingparties";
                return View("HomeCareRegistration", createClient);
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Involving Parties successfully added to Client" : "An Error Occurred" });

            createClient.ActiveTab = "regulatorycontact";
            #region Regulatory Contact
            if (_cache.TryGetValue(cacheKey, out List<GetBaseRecordWithItems> baseRecords))
            {

                createClient.RegulatoryContacts = (from rec in baseRecords
                                                   where rec.KeyName == "Client_RegulatoryContact"
                                                   from recItem in rec.BaseRecordItems
                                                   select new ClientRegulatoryContact
                                                   {
                                                       BaseRecordItemId = recItem.BaseRecordItemId,
                                                       RegulatoryContact = recItem.ValueName
                                                   }).ToList();
            }
            #endregion
            return View("HomeCareRegistration", createClient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _RegulatoryContact(CreateClient createClient)
        {

            var items = createClient.RegulatoryContacts.Where(s => s.IsSelected).ToList();
            items.ForEach(c =>
            {
                c.ClientId = createClient.ClientId;
            });

            foreach (var c in items)
            {
                string folder = "ClientRegulatoryContact".ToLower();
                string filename = string.Concat(c.RegulatoryContact.Replace(" ", ""), "_", createClient.Firstname, "_", createClient.Surname, Path.GetExtension(c.EvidenceFile.FileName));
               // string path = await this.HttpContext.Request.UploadFileToDropboxAsync(_dropboxClient, c.EvidenceFile, folder, filename);
                string path = await _fileUpload.UploadFile(folder, true, filename, c.EvidenceFile.OpenReadStream());

                _logger.LogInformation("Uploaded file to Azure Blob Storage: {0}", path);
                c.Evidence = path;
            }


            var regulatoryContacts = Mapper.Map<List<PostClientRegulatoryContact>>(items);
            var result = await _clientRegulatoryContactService.Post(regulatoryContacts);
            if (!result.IsSuccessStatusCode)
            {
                createClient.ActiveTab = "regulatorycontact";
                return View("HomeCareRegistration", createClient);
            }

            createClient.ActiveTab = "caredetails";
            createClient.CareDetails = HttpContext.Session.Get<List<ClientCareDetailsHeading>>("caredetailsItems");
            return View("HomeCareRegistration", createClient);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _CareDetails(CreateClient createClient)
        {

            var clientCareDetails = (from cl in createClient.CareDetails
                                     from tk in cl.Tasks
                                     where tk.IsSelected
                                     select new PostClientCareDetails
                                     {
                                         ClientCareDetailsTaskId = tk.CareDetailsTaskId,
                                         ClientId = createClient.ClientId,
                                         Description = tk.Description,
                                         Location = tk.Location,
                                         Mitigation = tk.Mitigation,
                                         Remark = tk.Remark,
                                         Risk = tk.Risk
                                     }).ToList();

            var result = await _clientCareDetails.PostClientDetails(clientCareDetails);
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                createClient.ActiveTab = "caredetails";
                return View("HomeCareRegistration", createClient);
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "CareDetails successfully added to Client" : "An Error Occurred" });

            return RedirectToAction("HomeCareDetails", new { clientId = createClient.ClientId });
        }
        #endregion

        #region Edit
        public async Task<IActionResult> EditRegistration(int? clientId)
        {
            var result = await _clientService.GetClientForEdit(clientId.Value);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _EditPersonalInfo(GetClientForEdit model)
        {

            if (model == null || !ModelState.IsValid)
            {
                return View("EditRegistration", model);
            }


            if (model.ClientImage != null)
            {
              //  string folder = $"ClientPassport/{model.Telephone}";
                string filename = string.Concat(model.Firstname, "_", model.Surname, Path.GetExtension(model.ClientImage.FileName));
               // await this.HttpContext.Request.UpdateDropboxFileAsync(_dropboxClient, model.ClientImage, folder, filename);
                var clientProfilePicture = await _fileUpload.UploadFile("clientpassport", true, filename, model.ClientImage.OpenReadStream());
                model.PassportFilePath = clientProfilePicture;
            }



            var putClient = Mapper.Map<PutClient>(model);
            var result = await _clientService.PutClient(putClient, model.ClientId);
            // var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result >= 1, Message = result >= 1 ? "Client Personal Information successfully updated" : "An Error Occurred" });
            if (result < 1)
            {
                // model.DeleteFileFromDisk(_env);
                return View("EditRegistration", model);
            }
            return RedirectToAction("HomeCareDetails", new { clientId = model.ClientId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _EditInvolvingParties(GetClientForEdit model)
        {

            if (model == null || !ModelState.IsValid)
            {
                return View("EditRegistration", model);
            }
            var putClient = Mapper.Map<List<PutClientInvolvingParty>>(model.InvolvingParties);
            var result = await _clientInvolvingPartyService.Put(putClient);

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "Client Involving Party successfully updated" : "An Error Occurred" });
            if (!result.IsSuccessStatusCode )
            {
                // model.DeleteFileFromDisk(_env);
                return View("EditRegistration", model);
            }
            return RedirectToAction("HomeCareDetails", new { clientId = model.ClientId });
        }
        #endregion

        #region Details
        public async Task<IActionResult> HomeCareDetails(int? clientId)
        {
            if (!clientId.HasValue)
            {
                return NotFound();
            }

            var result = await _clientService.GetClient(clientId.Value);
            QRCodeData qrCodeData = _qRCodeGenerator.CreateQrCode(result.UniqueId, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            result.QRCode = qrCodeImage.ToByteArray();
            result.GetBaseRecords = await _baseRecordService.GetBaseRecordItem();
            return View(result);
        }
        #endregion

        #region Medication

        [Route("[Controller]/[action]/{clientId}", Name = "Medication")]
        public async Task<IActionResult> Medications(int clientId)
        {
            ViewBag.ClientId = clientId;
            var medications = await _clientService.GetMedications(clientId);
            return View(medications);
        }


        [Route("[Controller]/Medication/Create/{clientId}", Name = "CreateMedication")]
        public async Task<IActionResult> CreateMedication(int? clientId)
        {
            var model = new CreateMedicationViewModel();
            var medNames = await _medicationService.Get();
            var medManufacturers = await _medicationService.GetManufacturers();
            var weekdays = await _rotaDayofWeekService.Get();
            var rotaTypes = await _clientRotaTypeService.Get();
            model.ClientId = clientId.Value;

            model.Medications = medNames.Select(s => new SelectListItem(string.Concat(s.MedicationName, " (", s.Strength, ")"), s.MedicationId.ToString())).ToList();
            model.MedicationManufacturers = medManufacturers.Select(s => new SelectListItem(s.Manufacturer, s.MedicationManufacturerId.ToString())).ToList();
            model.Days = weekdays.Select(d => new CreateMedicationDay()
            {
                DayOfWeek = d.DayofWeek,
                RotaDayofWeekId = d.RotaDayofWeekId,
                RotaTypes = rotaTypes.Select(r => new CreateMedicationPeriod()
                {
                    RotaType = r.RotaType,
                    ClientRotaTypeId = r.ClientRotaTypeId
                }).ToList()
            }).ToList();


            //HttpContext.Session.Set<List<GetMedication>>("medNames", medNames);
            //HttpContext.Session.Set<List<GetRotaDayofWeek>>("weekdays", weekdays);
            //HttpContext.Session.Set<List<GetClientRotaType>>("rotaTypes", rotaTypes);
            //HttpContext.Session.Set<List<GetMedicationManufacturer>>("medManufacturers", medManufacturers);


            return View(model);
        }

        [HttpPost("[Controller]/Medication/Create/{clientId}", Name = "CreateMedication")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMedication(CreateMedicationViewModel model)
        {

            if (!ModelState.IsValid)
            {
                await ResetModel(model);
                return View(model);
            }

            var daysPeriods = model.Days.Where(d => d.IsSelected).ToList();
            foreach (var item in daysPeriods)
            {
                model.ClientMedicationDay.Add(new PostClientMedicationDay()
                {
                    RotaDayofWeekId = item.RotaDayofWeekId,
                    ClientMedicationPeriod = item.RotaTypes.Where(s => s.IsSelected).Select(n => new PostClientMedicationPeriod
                    {
                        ClientRotaTypeId = n.ClientRotaTypeId
                    }).ToList()
                });
            }
            var postClientMed = Mapper.Map<PostClientMedication>(model);
            //   var j = JsonConvert.SerializeObject(postClientMed);
            var result = await _clientService.PostMedication(postClientMed);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus
            {
                IsSuccessful = result.IsSuccessStatusCode,
                Message = result.IsSuccessStatusCode ? "Operation Successful" : "Operation Failed"
            });

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Medications", new { clientId = model.ClientId });
            }
            else
            {
                _logger.LogError(content, new[] { $"{this.HttpContext.Request.Path.ToUriComponent()}" });
                await ResetModel(model);
                return View(model);
            }

        }

        [Route("[Controller]/Medication/Details/{clientId}/{id}", Name = "MedicationDetails")]
        public async Task<IActionResult> MedicationDetails(int clientId, int id)
        {
            ViewBag.ClientId = clientId;
            ViewBag.Id = id;
            var medication = await _clientService.GetMedication(clientId, id);
            if (medication == null)
                return NotFound();

            return View(medication);
        }

        async Task ResetModel(CreateMedicationViewModel model)
        {
            var medNames = await _medicationService.Get();
            var medManufacturers = await _medicationService.GetManufacturers();
            var weekdays = await _rotaDayofWeekService.Get();
            var rotaTypes = await _clientRotaTypeService.Get();

            model.Medications = medNames.Select(s => new SelectListItem(string.Concat(s.MedicationName, " (", s.Strength, ")"), s.MedicationId.ToString())).ToList();
            model.MedicationManufacturers = medManufacturers.Select(s => new SelectListItem(s.Manufacturer, s.MedicationManufacturerId.ToString())).ToList();
            model.Days = weekdays.Select(d => new CreateMedicationDay()
            {
                DayOfWeek = d.DayofWeek,
                RotaDayofWeekId = d.RotaDayofWeekId,
                RotaTypes = rotaTypes.Select(r => new CreateMedicationPeriod()
                {
                    RotaType = r.RotaType,
                    ClientRotaTypeId = r.ClientRotaTypeId
                }).ToList()
            }).ToList();

        }
        #endregion
    }
}