using AwesomeCare.Admin.Services.OfficeLocation;
using AwesomeCare.DataTransferObject.DTOs.OfficeLocation;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class OfficeLocationController : BaseController
    {
        private readonly ILogger<OfficeLocationController> logger;
        private readonly IOfficeLocationService officeLocationService;

        public OfficeLocationController(ILogger<OfficeLocationController> logger,
            IFileUpload fileUpload,
            IOfficeLocationService officeLocationService) : base(fileUpload)
        {
            this.logger = logger;
            this.officeLocationService = officeLocationService;
        }
        public async Task<IActionResult> Index()
        {
            var offices = await officeLocationService.GetAsync();
            return View(offices);
        }

        public IActionResult NewOffice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewOffice(PostOfficeLocation model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                var result = await officeLocationService.PostAsync(model);

                SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null, Message = result != null ? "New Office Location successfully added" : "An error occurred, please try again" });

                if (result == null)
                {
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = "An error occurred, please try again" });

                logger.LogError(ex, "");
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int officeId)
        {
            var office = await officeLocationService.GetAsync(officeId);
            return View(office);
        }

        public async Task<IActionResult> Edit(int officeId)
        {
            var office = await officeLocationService.GetAsync(officeId);
            var putOffice = new PutOfficeLocation
            {
                UniqueId = office.UniqueId,
                OfficeLocationId = office.OfficeLocationId,
                Address = office.Address,
                ContactPersonEmail = office.ContactPersonEmail,
                ContactPersonFullName = office.ContactPersonFullName,
                ContactPersonPhoneNumber = office.ContactPersonPhoneNumber,
                Latitude = office.Latitude,
                Longitude = office.Longitude
            };
            return View(putOffice);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PutOfficeLocation model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);

                var result = await officeLocationService.PutAsync(model);

                SetOperationStatus(new Models.OperationStatus { IsSuccessful = result != null, Message = result != null ? "Office Location successfully updated" : "An error occurred, please try again" });

                if (result == null)
                {
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                SetOperationStatus(new Models.OperationStatus { IsSuccessful = false, Message = "An error occurred, please try again" });

                logger.LogError(ex, "");
                return View(model);
            }
        }
        }
}
