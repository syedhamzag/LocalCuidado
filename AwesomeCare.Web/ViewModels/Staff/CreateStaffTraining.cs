using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.ViewModels.Staff
{
    public class CreateStaffTraining
    {
        [Display(Name ="Training Name")]
        public string Training { get; set; }
        public string Certificate { get; set; }
        public string Location { get; set; }
        public string Trainer { get; set; }
        public string StartDate { get; set; }
        public string ExpiredDate { get; set; }
        public IFormFile CertificateAttachment { get; set; }
    }
}
