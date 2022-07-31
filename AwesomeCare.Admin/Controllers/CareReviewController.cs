using AutoMapper;
using AwesomeCare.Admin.Services.CareReview;
using AwesomeCare.Admin.ViewModels.CareReview;
using AwesomeCare.DataTransferObject.DTOs.CareReview;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class CareReviewController : BaseController
    {
        private ICareReviewService _careReviewService;
        public CareReviewController(IFileUpload fileUpload,ICareReviewService careReviewService) : base(fileUpload)
        {
            _careReviewService = careReviewService;
        }

        public IActionResult Index(int clientId)
        {
            var model = new CreateCareReview();
            model.ClientId = clientId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreateCareReview model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            var entity = Mapper.Map<PostCareReview>(model);

            var result = await _careReviewService.Post(entity);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode == true ? "successfully registered" : "An Error Occurred" });
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("HomeCareDetails", "Client", new { clientId = model.ClientId });
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var entity = await _careReviewService.Get(Id);
            var model = new CreateCareReview 
            {
                Action = entity.Action,
                CareReviewId = entity.CareReviewId,
                ClientId = entity.ClientId,
                Date = entity.Date,
                Name = entity.Name,
                Note = entity.Note,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateCareReview model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View(model);
            }
            var entity = Mapper.Map<PutCareReview>(model);

            var result = await _careReviewService.Put(entity);
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
