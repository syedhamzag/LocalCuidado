using AutoMapper;
using AwesomeCare.Admin.Services.Hobbies;
using AwesomeCare.Admin.ViewModels.Hobbies;
using AwesomeCare.DataTransferObject.DTOs.Hobbies;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class HobbiesController : BaseController
    {
        private IHobbiesServices _hobbiesService;

        public HobbiesController(IHobbiesServices hobbiesService, IFileUpload fileUpload) : base(fileUpload)
        {
            _hobbiesService = hobbiesService;
        }

        public async Task<IActionResult> Reports()
        {
            var entities = await _hobbiesService.Get();
            var putEntities = Mapper.Map<List<Createhobbies>>(entities);
            return View(putEntities);
        }

        public IActionResult Index()
        {
            var model = new Createhobbies();
            return View(model);
        }

        public async Task<IActionResult> View(int Id)
        {
            var entity = await _hobbiesService.Get(Id);
            var putEntity = Mapper.Map<Createhobbies>(entity);
            return View(putEntity);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var entity = await _hobbiesService.Get(Id);
            var putEntity = Mapper.Map<Createhobbies>(entity);
            return View(putEntity);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var entity = await _hobbiesService.Get(Id);
            await _hobbiesService.Delete(entity.HId);
            return RedirectToAction("Reports");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Createhobbies model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostHobbies entity = new PostHobbies();
            entity.Name = model.Name;
            entity.Description = model.Description;

            var result = await _hobbiesService.Post(entity);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Reports");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Createhobbies model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PutHobbies entity = new PutHobbies();
            entity.HId = model.HId;
            entity.Name = model.Name;
            entity.Description = model.Description;

            var result = await _hobbiesService.Put(entity);
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
