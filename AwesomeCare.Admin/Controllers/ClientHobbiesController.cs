using AutoMapper;
using AwesomeCare.Admin.Services.ClientHobbies;
using AwesomeCare.Admin.Services.Hobbies;
using AwesomeCare.Admin.ViewModels.ClientHobbies;
using AwesomeCare.DataTransferObject.DTOs.ClientHobbies;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientHobbiesController : BaseController
    {
        private IClientHobbiesServices _clientHobbiesServices;
        private IHobbiesServices _hobbiesServices;

        public ClientHobbiesController(IClientHobbiesServices clientHobbiesServices, IFileUpload fileUpload, IHobbiesServices hobbiesServices) : base(fileUpload)
        {
            _clientHobbiesServices = clientHobbiesServices;
            _hobbiesServices = hobbiesServices;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientHobbiesServices.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateClientHobbies();
            var hobby = await _hobbiesServices.Get();
            model.HobbyList = hobby.Select(s => new SelectListItem(s.Name, s.HId.ToString())).ToList();
            model.ClientId = clientId;
            var hobbies = await _clientHobbiesServices.Get();
            var clientHobby = hobbies.Where(s => s.ClientId == clientId).ToList();
            if (clientHobby.Count > 0)
            {
                model.Hobbies = clientHobby.Select(s => s.HId).ToList();
                model.Title = "Update Client Hobbies";
                model.Action = "Update";
            }
            return View(model);
        }

        public async Task<IActionResult> View(int hobbiesId)
        {
            var entity = await _clientHobbiesServices.Get(hobbiesId);
            var putEntity = Mapper.Map<CreateClientHobbies>(entity);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int clientId)
        {
            var entity = await _clientHobbiesServices.Get(clientId);
            var putEntity = Mapper.Map<CreateClientHobbies>(entity);
            return View(putEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientHobbies model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            List<PutClientHobbies> entity = model.Hobbies.Select(o => new PutClientHobbies { HId = o,ClientId=model.ClientId}).ToList();

            var result = await _clientHobbiesServices.Put(entity);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails","Client", new { clientId = model.ClientId});
            }
            return View(model);
        }
    }
}
