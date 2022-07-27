using AutoMapper;
using AwesomeCare.Admin.Services.HealthCondition;
using AwesomeCare.Admin.ViewModels.HealthCondition;
using AwesomeCare.DataTransferObject.DTOs.HealthCondition;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class HealthConditionController : BaseController
    {
        private IHealthConditionServices _healthConditionServices;

        public HealthConditionController(IHealthConditionServices healthConditionServices, IFileUpload fileUpload) : base(fileUpload)
        {
            _healthConditionServices = healthConditionServices;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _healthConditionServices.Get();
            var putEntities = Mapper.Map<List<CreateHealthCondition>>(entities);
            return View(putEntities);
        }

        public IActionResult Index()
        {
            var model = new CreateHealthCondition();
            return View(model);
        }

        public async Task<IActionResult> View(int Id)
        {
            var entity = await _healthConditionServices.Get(Id);
            var putEntity = Mapper.Map<CreateHealthCondition>(entity);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var entity = await _healthConditionServices.Get(Id);
            var putEntity = Mapper.Map<CreateHealthCondition>(entity);
            return View(putEntity);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _healthConditionServices.Get(Id);
            await _healthConditionServices.Delete(entity.HCId);
            return RedirectToAction("Reports");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateHealthCondition model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostHealthCondition entity = new PostHealthCondition();
            entity.Name = model.Name;
            entity.Description = model.Description;

            var result = await _healthConditionServices.Post(entity);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Duty On Call successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateHealthCondition model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PutHealthCondition entity = new PutHealthCondition();
            entity.HCId = model.HCId;
            entity.Name = model.Name;
            entity.Description = model.Description;

            var result = await _healthConditionServices.Put(entity);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = result.IsSuccessStatusCode,
                Message = result.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }
    }
}
