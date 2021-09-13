using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class TestController : BaseController
    {
        private readonly IEmailService emailService;

        public TestController(IFileUpload fileUpload,
            IEmailService emailService) :base(fileUpload)
        {
            this.emailService = emailService;
        }
        public async Task<IActionResult> Index()
        {
            await emailService.SendAsync(new List<string> { "olamidejames007@gmail.com" }, "SendGrid from Cuidado", "Testing SendGrid in Cuidado");

            return View();
        }
    }
}
