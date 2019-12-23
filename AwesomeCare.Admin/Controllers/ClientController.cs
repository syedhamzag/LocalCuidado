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

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IClientInvolvingParty _clientInvolvingPartyService;
        private readonly IClientRegulatoryContactService _clientRegulatoryContactService;
        private readonly IHostingEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;
        private const string cacheKey = "baserecord_key";
        private readonly QRCodeGenerator _qRCodeGenerator;
        public ClientController(QRCodeGenerator qRCodeGenerator, IMemoryCache cache, IClientRegulatoryContactService clientRegulatoryContactService, IClientInvolvingParty clientInvolvingPartyService, IClientService clientService, IHostingEnvironment env, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _clientInvolvingPartyService = clientInvolvingPartyService;
            _env = env;
            _logger = logger;
            _cache = cache;
            _clientRegulatoryContactService = clientRegulatoryContactService;
            _qRCodeGenerator = qRCodeGenerator;
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
            var client = new CreateClient();
            var involvingPartyItems = await _clientService.GetClientInvolvingPartyBase();
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
            HttpContext.Session.Set<List<ClientInvolvingParty>>("involvingPartyItems", clientInvolvingPartyItems);
            client.InvolvingParties = clientInvolvingPartyItems;
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
                if (_env.IsDevelopment())
                    model.StatusId = 14;
                else
                    model.StatusId = 8;


                if (model == null || !ModelState.IsValid)
                {
                    return View(model);
                }
                await model.SaveFileToDisk(_env);
                var result = await _clientService.PostClient(model);
                // var content = await result.Content.ReadAsStringAsync();

                SetOperationStatus(new OperationStatus { IsSuccessful = result != null ? true : false, Message = result != null ? "New Client successfully registered" : "An Error Occurred" });
                if (result == null)
                {
                    model.DeleteFileFromDisk(_env);
                    return View(model);
                }
                model.ActiveTab = "involvingparties";
                model.ClientId = result.ClientId;
                model.InvolvingParties = HttpContext.Session.Get<List<ClientInvolvingParty>>("involvingPartyItems");
                return View("HomeCareRegistration", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HomeCareRegistration", null);
                return View("HomeCareRegistration", model);
            }

            // return RedirectToAction("HomeCare");
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
                c.ClientId = 1004;// createClient.ClientId;
            });

            items.ForEach(async c =>
            {
                string filePath = await this.HttpContext.Request.UploadFileAsync(_env, c.EvidenceFile, "clientregulatorycontact", "");
                c.Evidence = filePath;
            });

            var regulatoryContacts = Mapper.Map<List<PostClientRegulatoryContact>>(items);
            var result = await _clientRegulatoryContactService.Post(regulatoryContacts);
            if (!result.IsSuccessStatusCode)
            {
                createClient.ActiveTab = "regulatorycontact";
                return View("HomeCareRegistration", createClient);
            }
            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "Regulotary Contact successfully added to Client" : "An Error Occurred" });

            return View("HomeCareRegistration", createClient);
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
            //  await putClient.SaveFileToDisk(_env);
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