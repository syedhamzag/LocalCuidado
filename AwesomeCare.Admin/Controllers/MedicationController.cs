using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Medication;
using AwesomeCare.DataTransferObject.DTOs.Medication;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.Admin.Controllers
{
    public class MedicationController : BaseController
    {
        private IMedicationService _medicationService;

        public MedicationController(IMedicationService medicationService, IFileUpload fileUpload) : base(fileUpload)
        {
            _medicationService = medicationService;
        }

        public async Task<IActionResult> Index()
        {
            var entities = await _medicationService.Get();
            return View(entities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostMedication model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = await _medicationService.Post(model);

            var content = await entity.Content.ReadAsStringAsync();


            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful =entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : content
            });
            if (entity != null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _medicationService.Get(id);
            if (entity == null) return NotFound();

            var putEntity = new PutMedication
            {
                Deleted = entity.Deleted,
                MedicationId = entity.MedicationId,
                MedicationName = entity.MedicationName,
                Strength = entity.Strength
            };
            return View(putEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PutMedication model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = await _medicationService.Put(model);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity != null,
                Message = entity != null ? "Successful" : "Operation failed"
            });
            if (entity != null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }
    }
}