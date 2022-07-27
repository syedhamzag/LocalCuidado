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
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Admin.Services.ClientRotaName;
using AwesomeCare.Admin.Services.RotaTask;
using AwesomeCare.Admin.Services.ClientRota;
using AwesomeCare.Admin.Services.HealthCondition;
using AwesomeCare.Admin.Services.Hobbies;
using AwesomeCare.DataTransferObject.DTOs.ClientHealthCondition;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private IClientInvolvingParty _clientInvolvingPartyService;
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
        IClientRotaNameService _clientRotaNameService;
        IRotaTaskService _rotaTaskService;
        IClientRotaService _clientRotaService;
        private IHealthConditionServices _healthConService;
        private IHobbiesServices _hobbyService;
        private IStaffService _staffService;

        public ClientController(DropboxClient dropboxClient, IFileUpload fileUpload, QRCodeGenerator qRCodeGenerator, IMemoryCache cache, IHobbiesServices hobbyService, IHealthConditionServices healthConService,
        IClientRegulatoryContactService clientRegulatoryContactService, IClientInvolvingParty clientInvolvingPartyService, IStaffService staffService, IClientRotaService clientRotaService,
        IClientService clientService, IWebHostEnvironment env, ILogger<ClientController> logger, IBaseRecordService baseRecordService, IRotaTaskService rotaTaskService, IClientRotaNameService clientRotaNameService,
            IClientCareDetails clientCareDetails, IMedicationService medicationService, IClientRotaTypeService clientRotaTypeService, IRotaDayofWeekService rotaDayofWeekService) : base(fileUpload)
        {
            _dropboxClient = dropboxClient;
            _clientRotaService = clientRotaService;
            _clientRotaNameService = clientRotaNameService;
            _rotaTaskService = rotaTaskService;
            _baseRecordService = baseRecordService;
            _clientService = clientService;
            _clientInvolvingPartyService = clientInvolvingPartyService;
            _healthConService = healthConService;
            _hobbyService = hobbyService;
            _env = env;
            _logger = logger;
            _cache = cache;
            _clientRegulatoryContactService = clientRegulatoryContactService;
            _qRCodeGenerator = qRCodeGenerator;
            _clientCareDetails = clientCareDetails;
            _medicationService = medicationService;
            _clientRotaTypeService = clientRotaTypeService;
            _rotaDayofWeekService = rotaDayofWeekService;
            _staffService = staffService;
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
            var staffs = await _staffService.GetStaffs();
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
            client.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
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
                var staffs = await _staffService.GetStaffs();
                if (model == null || !ModelState.IsValid)
                {
                    //model.InvolvingParties = HttpContext.Session.Get<List<ClientInvolvingParty>>("involvingPartyItems");
                    model = new CreateClient();
                    model.StaffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString())).ToList();
                    return View(model);
                }


                #region PersonalInfo
                string folder = "clientpassport";
                string filename = string.Concat(folder, "_", model.Telephone);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.ClientImage.OpenReadStream());

                model.PassportFilePath = path;
                #endregion

               

                var postClient = Mapper.Map<PostClient>(model);
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
            var staffs = await _staffService.GetStaffs();
            var result = await _clientService.GetClientForEdit(clientId.Value);
            result.InvolvingPartyCount = result.InvolvingParties.Count;
            ViewBag.staffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString()));
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
        public async Task<IActionResult> _EditInvolvingParties(GetClientForEdit model, IFormCollection formcollection)
        {

            if (model == null || !ModelState.IsValid)
            {
                return View("EditRegistration", model);
            }
            List<PutClientInvolvingParty> puts = new List<PutClientInvolvingParty>();
            for (int i = 0; i < model.InvolvingPartyCount; i++)
            {
                
                PutClientInvolvingParty put = new PutClientInvolvingParty();

                int item = int.Parse(formcollection["ClientInvolvingPartyItemId"][i]);
                int party = int.Parse(formcollection["ClientInvolvingPartyId"][i]);

                var tel = formcollection["Telephone"][i];
                var address = formcollection["Address"][i];
                var email = formcollection["Email"][i];
                var name = formcollection["Name"][i];
                var relation = formcollection["Relationship"][i];
                put.ClientInvolvingPartyId = party;
                put.ClientId = model.ClientId;
                put.Address = address;
                put.Email = email;
                put.Name = name;
                put.Relationship = relation;
                put.Telephone = tel;
                put.ClientInvolvingPartyItemId = item;
                puts.Add(put);
            }
            var json = JsonConvert.SerializeObject(puts);
            var result = await _clientInvolvingPartyService.Put(puts);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "Client Involving Party successfully updated" : "An Error Occurred" });
            if (!result.IsSuccessStatusCode )
            {
                model.InvolvingParties = Mapper.Map<List<GetClientInvolvingPartyForEdit>>(puts);
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
            var staffs = await _staffService.GetStaffs();
            var clients = await _clientService.GetClients();
            GetClient result = await _clientService.GetClient(clientId.Value);
            var baserecords = await _baseRecordService.GetBaseRecordItem();
            QRCodeData qrCodeData = _qRCodeGenerator.CreateQrCode(result.UniqueId, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            result.QRCode = qrCodeImage.ToByteArray();
            result.GetBaseRecords = baserecords;
            result.Status = baserecords.Where(s => s.BaseRecordItemId == result.StatusId).FirstOrDefault().ValueName;
            result.Gender = baserecords.Where(s => s.KeyName == "Gender" && s.BaseRecordItemId == result.GenderId).FirstOrDefault().ValueName;
            ViewBag.staffList = staffs.Select(s => new SelectListItem(s.Fullname, s.StaffPersonalInfoId.ToString()));
            ViewBag.Clients = clients.ToList();
            return View(result);
        }
        //public List<GetClient> GetCarePlan(int clientId)
        //{
        //    var client = _clientService.GetClientCarePlan(clientId);
        //}
        #endregion

        #region Medication

        [Route("[Controller]/[action]/{clientId}", Name = "Medication")]
        public async Task<IActionResult> Medications(int clientId)
        {
            ViewBag.ClientId = clientId;
            var med = await _clientService.GetMedications(clientId);
            return View(med);
        }


        [Route("[Controller]/Medication/Create/{clientId}", Name = "CreateMedication")]
        public async Task<IActionResult> CreateMedication(int? clientId)
        {
            var model = new CreateMedicationViewModel();
            var medNames = await _medicationService.Get();
            var medManufacturers = await _medicationService.GetManufacturers();
            var weekdays = await _rotaDayofWeekService.Get();
            var rotaTypes = await _clientRotaTypeService.Get();
            var rotas = await _clientRotaNameService.Get();
            var rotaTasks = await _rotaTaskService.Get();
            var clientRotas = await _clientRotaService.GetForEdit(clientId.Value);
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClient(model.ClientId);
            model.ClientName = client.Firstname + " " + client.Middlename + " " + client.Surname;
            model.Rotas = rotas.Select(r => new SelectListItem { Text = r.RotaName, Value = r.RotaId.ToString() }).ToList();
            model.ClientRotas = clientRotas;
            model.Medications = medNames.Select(s => new SelectListItem(string.Concat(s.MedicationName, " (", s.Strength, ")"), s.MedicationId.ToString())).ToList();
            model.MedicationManufacturers = medManufacturers.Select(s => new SelectListItem(s.Manufacturer, s.MedicationManufacturerId.ToString())).ToList();
            model.Days = weekdays.Select(d => new CreateMedicationDay()
            {
                DayOfWeek = d.DayofWeek,
                RotaDayofWeekId = d.RotaDayofWeekId,
                RotaTypes = rotaTypes.Select(r => new CreateMedicationPeriod()
                {
                    RotaType = r.RotaType,
                    ClientRotaTypeId = r.ClientRotaTypeId,
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
                        ClientRotaTypeId = n.ClientRotaTypeId,
                        RotaId = n.CRotaId,
                        StartTime = n.CStartTime,
                        StopTime = n.CStopTime
                    }).ToList()
                });
            }
            if (model.Evidence != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Evidence.FileName);
                string folder = "clientmedication";
                string filename = string.Concat(folder, "_Evidence_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.ClientMedImage = path;
            }
            else
            {
                model.ClientMedImage = "No Image";
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

        public async Task<IActionResult> EditMedication(int clientId, int id)
        {
            var medication = await _clientService.GetMedication(clientId, id);
            var model = new EditMedicationViewModel();
            var medNames = await _medicationService.Get();
            var medManufacturers = await _medicationService.GetManufacturers();
            var weekdays = await _rotaDayofWeekService.Get();
            var rotaTypes = await _clientRotaTypeService.Get();
            var rotas = await _clientRotaNameService.Get();
            if (medication != null)
            {
                model.Days = weekdays.Select(d => new CreateMedicationDay()
                {
                    DayOfWeek = d.DayofWeek,
                    RotaDayofWeekId = d.RotaDayofWeekId,
                    RotaTypes = rotaTypes.Select(r => new CreateMedicationPeriod()
                    {
                        RotaType = r.RotaType,
                        ClientRotaTypeId = r.ClientRotaTypeId,
                    }).ToList()
                }).ToList();

                model.ClientId = clientId;
                model.ClientMedicationId = medication.ClientMedicationId;
                model.TimeCritical = medication.TimeCritical;
                model.Dossage = medication.Dossage;
                model.ExpiryDate = medication.ExpiryDate;
                model.Frequency = medication.Frequency;
                model.Gap_Hour = medication.Gap_Hour;
                model.Means = medication.Means;
                model.MedicationId = medication.MedicationId;
                model.StopDate = medication.StopDate;
                model.MedicationManufacturerId = medication.MedicationManufacturerId;
                model.Type = medication.Type;
                model.Status = medication.Status;
                model.StartDate = medication.StartDate;
                model.Route = medication.Route;
                model.Remark = medication.Remark;
                model.ClientMedImage = medication.ClientMedImage;
                model.Rotas = rotas.Select(r => new SelectListItem { Text = r.RotaName, Value = r.RotaId.ToString() }).ToList();
                var daysPeriods = medication.ClientMedicationDay.ToList();
                foreach (var item in daysPeriods)
                {
                    
                    model.ClientMedicationDay.Add(new PutClientMedicationDay()
                    {
                        RotaDayofWeekId = item.RotaDayofWeekId,
                        ClientMedicationId = item.ClientMedicationId,
                        ClientMedicationDayId = item.ClientMedicationDayId,
                        ClientMedicationPeriod = item.ClientMedicationPeriod.Select(s => new PutClientMedicationPeriod
                        {
                            ClientMedicationDayId = s.ClientMedicationDayId,
                            ClientMedicationPeriodId = s.ClientMedicationPeriodId,
                            ClientRotaTypeId = s.ClientRotaTypeId,
                            RotaId = s.RotaId,
                            StartTime = s.StartTime,
                            StopTime = s.StopTime

                        }).ToList()
                });
                }
            }

            model.Medications = medNames.Select(s => new SelectListItem(string.Concat(s.MedicationName, " (", s.Strength, ")"), s.MedicationId.ToString())).ToList();
            model.MedicationManufacturers = medManufacturers.Select(s => new SelectListItem(s.Manufacturer, s.MedicationManufacturerId.ToString())).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMedication(EditMedicationViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var daysPeriods = model.Days.Where(d => d.IsSelected).ToList();
            //foreach (var item in daysPeriods)
            //{
            //    model.ClientMedicationDay.Add(new PutClientMedicationDay()
            //    {
            //        RotaDayofWeekId = item.RotaDayofWeekId,
            //        ClientMedicationDayId = item.RotaTypes.Where(s => s.IsSelected).Select(n => n.ClientMedicationDayId).FirstOrDefault(),
            //        ClientMedicationId = model.ClientMedicationId,
            //        ClientMedicationPeriod = item.RotaTypes.Where(s => s.IsSelected).Select(n => new PutClientMedicationPeriod
            //        {
            //            ClientRotaTypeId = n.ClientRotaTypeId,
            //            RotaId = n.RotaId,
            //            StartTime = n.StartTime,
            //            StopTime = n.StopTime,
            //            ClientMedicationDayId = n.ClientMedicationDayId,
            //            ClientMedicationPeriodId = n.
            //        }).ToList()
            //    }); 
            //}
            if (model.Evidence != null)
            {
                string extention = model.ClientId + System.IO.Path.GetExtension(model.Evidence.FileName);
                string folder = "clientmedication";
                string filename = string.Concat(folder, "_Evidence_", extention);
                string path = await _fileUpload.UploadFile(folder, true, filename, model.Evidence.OpenReadStream());
                model.ClientMedImage = path;
            }
            else
            {
                model.ClientMedImage = model.ClientMedImage;
            }
            var postClientMed = Mapper.Map<PutClientMedication>(model);
            //   var j = JsonConvert.SerializeObject(postClientMed);
            var result = await _clientService.PutMedication(postClientMed);
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

        #region Client_Details
        [HttpGet]
        public JsonResult personalInfo(int clientId)
        {
            var getClient = _clientService.GetHealthHobby(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult involvingparties(int clientId)
        {
            var getClient = _clientService.GetInvolvingParty(clientId);
            return Json(getClient.Result);
        }

        [HttpGet]
        public JsonResult dutyoncall(int clientId)
        {
            var getClient = _clientService.GetDutyOnCall(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult hospitalentry(int clientId)
        {
            var getClient = _clientService.GetHospitalEntry(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult hospitalexit(int clientId)
        {
            var getClient = _clientService.GetHospitalExit(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult filesrecord(int clientId)
        {
            var getClient = _clientService.GetFilesAndRecord(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult complaintregister(int clientId)
        {
            var getClient = _clientService.GetComplain(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult logaudit(int clientId)
        {
            var getClient = _clientService.GetLogAudit(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult medaudit(int clientId)
        {
            var getClient = _clientService.GetMedAudit(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult voice(int clientId)
        {
            var getClient = _clientService.GetVoice(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult mgtvisit(int clientId)
        {
            var getClient = _clientService.GetMgtVisit(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult program(int clientId)
        {
            var getClient = _clientService.GetProgram(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult servicewatch(int clientId)
        {
            var getClient = _clientService.GetServiceWatch(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult dailytask(int clientId)
        {
            var getClient = _clientService.GetDailyTask(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult mcabest(int clientId)
        {
            var getClient = _clientService.GetBestInterest(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult careobj(int clientId)
        {
            var getClient = _clientService.GetCarObj(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult bloodcoag(int clientId)
        {
            var getClient = _clientService.GetBloodCoag(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult bloodpressure(int clientId)
        {
            var getClient = _clientService.GetPressure(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult bmichart(int clientId)
        {
            var getClient = _clientService.GetBMIChart(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult bodytemp(int clientId)
        {
            var getClient = _clientService.GetBodyTemp(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult bowel(int clientId)
        {
            var getClient = _clientService.GetBowel(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult eyehealth(int clientId)
        {
            var getClient = _clientService.GetEyeHealth(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult foodintake(int clientId)
        {
            var getClient = _clientService.GetFoodIntake(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult heartrate(int clientId)
        {
            var getClient = _clientService.GetHeartRate(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult oxygen(int clientId)
        {
            var getClient = _clientService.GetOxygenLvl(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult painchart(int clientId)
        {
            var getClient = _clientService.GetPainChart(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult pulserate(int clientId)
        {
            var getClient = _clientService.GetPulseRate(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult seizure(int clientId)
        {
            var getClient = _clientService.GetSeizure(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult woundcare(int clientId)
        {
            var getClient = _clientService.GetWoundCare(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult personaldetail(int clientId)
        {
            var getClient = _clientService.GetReview(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult healthliving(int clientId)
        {
            var getClient = _clientService.GetHealthAndLiving(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult specialhealthmed(int clientId)
        {
            var getClient = _clientService.GetHealthAndMed(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult balance(int clientId)
        {
            var getClient = _clientService.GetBalance(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult physicalability(int clientId)
        {
            var getClient = _clientService.GetPhysicalAbility(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult specialhealthcond(int clientId)
        {
            var getClient = _clientService.GetHealthCondition(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult historyoffall(int clientId)
        {
            var getClient = _clientService.GetHistoryOfFall(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult nutrition(int clientId)
        {
            var getClient = _clientService.GetCarePlanNut(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult personalhygiene(int clientId)
        {
            var getClient = _clientService.GetPersonalHyg(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult infectioncontrol(int clientId)
        {
            var getClient = _clientService.GetInfectionControl(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult mtask(int clientId)
        {
            var getClient = _clientService.GetManagingTask(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult intandobj(int clientId)
        {
            var getClient = _clientService.GetInterestAndObj(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult pets(int clientId)
        {
            var getClient = _clientService.GetPets(clientId);
            return Json(getClient.Result);
        }
        [HttpGet]
        public JsonResult homerisk(int clientId)
        {
            var getClient = _clientService.GetHomeRisk(clientId);
            return Json(getClient.Result);
        }
        #endregion
    }
}