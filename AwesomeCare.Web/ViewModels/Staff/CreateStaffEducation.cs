using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.ViewModels.Staff
{
    public class CreateStaffEducation
    {
        public string Institution { get; set; }
        public string Certificate { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IFormFile CertificateAttachment { get; set; }
    }
}
