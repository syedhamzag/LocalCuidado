using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Staff;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.Admin.Services.Client;
using AwesomeCare.Admin.Services.Review;
using Microsoft.AspNetCore.Http;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;
using AwesomeCare.Admin.ViewModels.PersonalDetail;

namespace AwesomeCare.Admin.Controllers
{
    public class ReviewController : BaseController
    {
        private IReviewService _ReviewService;
        private IStaffService _staffService;
        private IClientService _clientService;

        public ReviewController(IFileUpload fileUpload, IStaffService staffService, IClientService clientService, IReviewService ReviewService) : base(fileUpload)
        {
            _staffService = staffService;
            _clientService = clientService;
            _ReviewService = ReviewService;
        }

        public async Task<IActionResult> Index(int? clientId)
        {
            var client = await _clientService.GetClientDetail();
            var model = new CreateReview();
            model.ClientId = clientId.Value;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateReview model, IFormCollection formsCollection)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }

            PostReview postlog = new PostReview();

            postlog.ClientId = model.ClientId;
            postlog.ReviewId = model.ReviewId;
            postlog.CP_PreDate = model.CP_PreDate;
            postlog.CP_ReviewDate = model.CP_ReviewDate;
            postlog.RA_PreDate = model.RA_PreDate;
            postlog.RA_ReviewDate = model.RA_ReviewDate;

            var result = await _ReviewService.Create(postlog);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "New Review successfully registered" : "An Error Occurred" });
            return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });

        }
    }
}
