﻿using System;
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
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IHostingEnvironment _env;

        public ClientController(IClientService clientService, IHostingEnvironment env)
        {
            _clientService = clientService;
            _env = env;
        }
        public async Task<IActionResult> HomeCare()
        {
            var result = await _clientService.GetClients();
            return View(result);
        }


        public async Task<IActionResult> HomeCareRegistration()
        {
            var client = new CreateClient();
            var involvingPartyItems = await _clientService.GetClientInvolvingPartyBase();
            foreach (var item in involvingPartyItems)
            {
                client.InvolvingPartyItems.Add(new ClientInvolvingPartyItem
                {
                    ClientInvolvingPartyItemId = item.ClientInvolvingPartyItemId,
                    ItemName = item.ItemName,
                    Description = item.Description,
                    Deleted = item.Deleted
                });
            }
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
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New Client successfully registered" : "An Error Occurred" });
            if (!result.IsSuccessStatusCode)
            {
                model.DeleteFileFromDisk(_env);
                return View(model);
            }


            return RedirectToAction("HomeCare");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult _InvolvingParty(List<ClientInvolvingPartyItem> model)
        {
            var items = model.Where(s => s.IsSelected).ToList();
            return PartialView();
        }

    }
}