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

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IClientInvolvingParty _clientInvolvingPartyService;
        private readonly IClientRegulatoryContactService _clientRegulatoryContactService;
        private readonly IClientCareDetails _clientCareDetails;
        private readonly IHostingEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;
        private const string cacheKey = "baserecord_key";
        private readonly QRCodeGenerator _qRCodeGenerator;
        private readonly DropboxClient _dropboxClient;

        public ClientController(DropboxClient dropboxClient, QRCodeGenerator qRCodeGenerator, IMemoryCache cache,
            IClientRegulatoryContactService clientRegulatoryContactService, IClientInvolvingParty clientInvolvingPartyService,
            IClientService clientService, IHostingEnvironment env, ILogger<ClientController> logger,
            IClientCareDetails clientCareDetails)
        {
            _dropboxClient = dropboxClient;
            _clientService = clientService;
            _clientInvolvingPartyService = clientInvolvingPartyService;
            _env = env;
            _logger = logger;
            _cache = cache;
            _clientRegulatoryContactService = clientRegulatoryContactService;
            _qRCodeGenerator = qRCodeGenerator;
            _clientCareDetails = clientCareDetails;
        }
        public async Task<IActionResult> HomeCare()
        {
            var result = await _clientService.GetClients();
            return View(result);
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
                string folder = $"ClientPassport/{model.Telephone}";
                string filename = string.Concat(model.Firstname, "_", model.Surname, Path.GetExtension(model.ClientImage.FileName));
                // string path = await this.HttpContext.Request.UploadFileToDropboxAsync(_dropboxClient, model.ClientImage, folder, filename);
                model.PassportFilePath = "path";// path;
                #endregion

                #region Involving Parties
                var involvingParties = InvolvingParty(model);
                model.InvolvingParties = involvingParties;
                #endregion

                #region Regulatory Contact
                var regulatoryContact = await RegulatoryContact(model);
                model.RegulatoryContacts = regulatoryContact;
                #endregion

                var postClient =   Mapper.Map<PostClient>(model);
                #region CareDetails
                var careDetails = CareDetails(model);
                postClient.CareDetails = careDetails;
                #endregion
                var json = JsonConvert.SerializeObject(postClient);
                var result = await _clientService.PostClient(postClient);
                // var content = await result.Content.ReadAsStringAsync();

                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Client successfully registered" : "An Error Occurred" });
                return RedirectToAction("HomeCareDetails", new { clientId = result.ClientId });
            }
            catch(Refit.ApiException e)
            {
                var error = e.Content;
                if(e.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    SetOperationStatus(new OperationStatus { IsSuccessful =  false, Message = error });
                }else if(e.StatusCode == System.Net.HttpStatusCode.BadRequest)
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
                //string folder = $"ClientRegulatoryContact/{createClient.Telephone}";
                //string filename = string.Concat(c.RegulatoryContact.Replace(" ", ""), "_", createClient.Firstname, "_", createClient.Surname, Path.GetExtension(c.EvidenceFile.FileName));
                //string path = await this.HttpContext.Request.UploadFileToDropboxAsync(_dropboxClient, c.EvidenceFile, folder, filename);
                c.Evidence = "hello";// path;
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
                string folder = $"ClientRegulatoryContact/{createClient.Telephone}";
                string filename = string.Concat(c.RegulatoryContact.Replace(" ", ""), "_", createClient.Firstname, "_", createClient.Surname, Path.GetExtension(c.EvidenceFile.FileName));
                string path = await this.HttpContext.Request.UploadFileToDropboxAsync(_dropboxClient, c.EvidenceFile, folder, filename);
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
                string folder = $"ClientPassport/{model.Telephone}";
                string filename = string.Concat(model.Firstname, "_", model.Surname, Path.GetExtension(model.ClientImage.FileName));
                await this.HttpContext.Request.UpdateDropboxFileAsync(_dropboxClient, model.ClientImage, folder, filename);
            }



            var putClient = Mapper.Map<PutClient>(model);
            var result = await _clientService.PutClient(putClient, model.ClientId);
            // var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result >= 1 ? true : false, Message = result >= 1 ? "Client Personal Information successfully updated" : "An Error Occurred" });
            if (result < 1)
            {
                // model.DeleteFileFromDisk(_env);
                return View("EditRegistration", model);
            }
            return RedirectToAction("EditRegistration", new { clientId = model.ClientId });
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
            return View(result);
        }
        #endregion
    }
}