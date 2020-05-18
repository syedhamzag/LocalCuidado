using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Medication;
using AwesomeCare.DataTransferObject.DTOs.MedicationManufacturer;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.Admin.Controllers
{
    public class MedicationManufacturerController : BaseController
    {
        private IMedicationService _medicationService;

        public MedicationManufacturerController(IMedicationService medicationService, IFileUpload fileUpload) : base(fileUpload)
        {
            _medicationService = medicationService;
        }

        public async Task<IActionResult> Index()
        {
            var entities = await _medicationService.GetManufacturers();
            return View(entities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostMedicationManufacturer model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = await _medicationService.PostManufacturer(model);

            var content = await entity.Content.ReadAsStringAsync();


            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
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
            var entity = await _medicationService.GetManufacturer(id);
            if (entity == null) return NotFound();

            var putEntity = new PutMedicationManufacturer
            {
                Deleted = entity.Deleted,
                MedicationManufacturerId = entity.MedicationManufacturerId,
                Manufacturer = entity.Manufacturer,
            };
            return View(putEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PutMedicationManufacturer model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entity = await _medicationService.PutManufacturer(model);
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