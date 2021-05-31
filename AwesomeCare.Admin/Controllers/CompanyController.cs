using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.Admin.Services.Company;
using AwesomeCare.DataTransferObject.DTOs.Company;
using Microsoft.AspNetCore.Mvc;
using AwesomeCare.Services.Services;
namespace AwesomeCare.Admin.Controllers
{
    public class CompanyController : BaseController
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService,IFileUpload fileUpload):base(fileUpload)
        {
            _companyService = companyService;
        }
        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetCompanies();
          
            return View(companies);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateCompanyDto model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View();
            }
            //Send to API
            var result = await _companyService.AddCompany(model);
            if (result.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "An error occurred while registering company");
                return View();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? companyId)
        {
            if (!companyId.HasValue)
                return NotFound();

            var company = await _companyService.GetCompany(companyId.Value);
            var updateCompany = Mapper.Map<UpdateCompanyDto>(company);
            return View(updateCompany);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCompanyDto model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return View();
            }
            var result = await _companyService.UpdateCompany(model,model.CompanyId);
           
           return RedirectToAction("Details", new { companyId = result.CompanyId });
        }

        public async Task<IActionResult> Details(int? companyId)
        {
            if (!companyId.HasValue)
                return NotFound();

            var getCompany = await _companyService.GetCompany(companyId.Value);
            var company = Mapper.Map<GetCompanyDto>(getCompany);
            return View(company);
        }
    }
}