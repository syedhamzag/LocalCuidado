using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.Admin.Services.Company;
using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace AwesomeCare.Admin.Controllers
{
    public class CompanyContactController : BaseController
    {
        private ICompanyContactService _companyContactService;

        public CompanyContactController(ICompanyContactService companyContactService)
        {
            _companyContactService = companyContactService;
        }
        public async Task<IActionResult> Index(int? companyId)
        {
            try
            {
                if (!companyId.HasValue)
                    return NotFound();

                var companyContact = await _companyContactService.GetCompanyContact(companyId.Value);
                if (companyContact == null)
                {
                    return RedirectToAction("Add", new { companyId = companyId });
                }
                return View(companyContact);
            }
            catch (ApiException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return RedirectToAction("Add", new { companyId = companyId });
                }
                return View();
            }
        }

        public IActionResult Add(int companyId)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(PostCompanyContactDto model, int companyId)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                    return View();

                var newCompanyContact = await _companyContactService.AddCompanyContact(model, companyId);
                if(newCompanyContact.IsSuccessStatusCode)
                    return RedirectToAction("Index", new { companyId = companyId });

                return View();
            }
            catch (ApiException ex)
            {

                throw;
            }
        }
    }
}