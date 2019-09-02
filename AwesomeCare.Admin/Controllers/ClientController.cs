using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.Admin.Models;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<IActionResult> HomeCare()
        {
            var result = await _clientService.GetClients();
            return View(result);
        }
        

        public IActionResult HomeCareRegistration()
        {
            var client = new CreateClient();

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

            var result = await _clientService.PostClient(model);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "New Client successfully registered" : "An Error Occurred" });
            if (!result.IsSuccessStatusCode)
                return View(model);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Test()
        {
            var client = new CreateClient();
            client.AreaCodeId = 4;
            return View(client);
        }
    }
}