using AutoMapper;
using AwesomeCare.Admin.Services.ClientHealthCondition;
using AwesomeCare.Admin.Services.HealthCondition;
using AwesomeCare.Admin.ViewModels.ClientHealthCondition;
using AwesomeCare.DataTransferObject.DTOs.ClientHealthCondition;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class ClientHealthConditionController : BaseController
    {
        private IClientHealthConditionServices _clientHealthConditionServices;
        private IHealthConditionServices _healthConditionServices;

        public ClientHealthConditionController(IClientHealthConditionServices clientHealthConditionServices, IFileUpload fileUpload, IHealthConditionServices healthConditionServices) : base(fileUpload)
        {
            _clientHealthConditionServices = clientHealthConditionServices;
            _healthConditionServices = healthConditionServices;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _clientHealthConditionServices.Get();
            return View(entities);
        }

        public async Task<IActionResult> Index(int clientId)
        {
            var model = new CreateClientHealthCondition();
            var hobby = await _healthConditionServices.Get();
            model.HealthList = hobby.Select(s => new SelectListItem(s.Name, s.HCId.ToString())).ToList();
            model.ClientId = clientId;
            var allHealth = await _clientHealthConditionServices.Get();
            var clientHealth = allHealth.Where(s=>s.ClientId==clientId).ToList();
            if (clientHealth.Count > 0)
            {
                model.Health = clientHealth.Select(s => s.HCId).ToList();
                model.Title = "Update Client Halth Condition";
                model.Action = "Update";
            }
                
            return View(model);
        }

        public async Task<IActionResult> View(int hobbiesId)
        {
            var entity = await _clientHealthConditionServices.Get(hobbiesId);
            var putEntity = Mapper.Map<CreateClientHealthCondition>(entity);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int hobbiesId)
        {
            var entity = await _clientHealthConditionServices.Get(hobbiesId);
            var putEntity = Mapper.Map<CreateClientHealthCondition>(entity);
            return View(putEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateClientHealthCondition model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            List<PutClientHealthCondition> entity = model.Health.Select(o => new PutClientHealthCondition { HCId = o, ClientId = model.ClientId }).ToList();

            var result = await _clientHealthConditionServices.Put(entity);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);
        }
    }
}
