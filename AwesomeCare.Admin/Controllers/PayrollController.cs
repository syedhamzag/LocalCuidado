using AwesomeCare.Admin.Services.Payroll;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class PayrollController : BaseController
    {
        private IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService, IFileUpload fileUpload) : base(fileUpload)
        {
            _payrollService = payrollService;
        }

        public async Task<IActionResult> Reports()
        {
            var payroll = await _payrollService.Get();

            
            return View(payroll);
        }
        public async Task<IActionResult> View(int staffId)
        {
            var payroll = await _payrollService.Get(staffId);


            return View(payroll);
        }
    }
}
