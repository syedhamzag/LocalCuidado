using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
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

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IClientInvolvingParty _clientInvolvingPartyService;
        private readonly IHostingEnvironment _env;

        public ClientController(IClientInvolvingParty clientInvolvingPartyService, IClientService clientService, IHostingEnvironment env)
        {
            _clientService = clientService;
            _clientInvolvingPartyService = clientInvolvingPartyService;
            _env = env;
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
            return View(client);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HomeCareRegistration(CreateClient model)
        {
            model.StatusId = 14;


            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            await model.SaveFileToDisk(_env);
            var result = await _clientService.PostClient(model);
           // var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result != null? true:false, Message = result != null ? "New Client successfully registered" : "An Error Occurred" });
            if (result == null)
            {
                model.DeleteFileFromDisk(_env);
                return View(model);
            }
            model.ActiveTab = "involvingparties";
            model.ClientId =  result.ClientId;
            model.InvolvingParties = HttpContext.Session.Get<List<ClientInvolvingParty>>("involvingPartyItems");
            return View("HomeCareRegistration", model);

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
            createClient.ActiveTab = "caredetails";
            return View("HomeCareRegistration", createClient);
        }

    }
}