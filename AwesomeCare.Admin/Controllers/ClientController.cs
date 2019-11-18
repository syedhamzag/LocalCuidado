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

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IClientInvolvingParty _clientInvolvingPartyService;
        private readonly IHostingEnvironment _env;
        private ILogger<ClientController> _logger;
        private readonly IMemoryCache _cache;
        private const string cacheKey = "baserecord_key";
        public ClientController(IMemoryCache cache,IClientInvolvingParty clientInvolvingPartyService, IClientService clientService, IHostingEnvironment env, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _clientInvolvingPartyService = clientInvolvingPartyService;
            _env = env;
            _logger = logger;
            _cache = cache;
        }
        public async Task<IActionResult> HomeCare()
        {
            var result = await _clientService.GetClients();
            return View(result);
        }


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

            createClient.ActiveTab = "caredetails";
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
            
            return View("HomeCareRegistration", createClient);
        }
        
    }
}