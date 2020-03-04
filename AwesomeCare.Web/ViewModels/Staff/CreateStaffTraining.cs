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
        [Required]
        public string Training { get; set; }
        [Required]
        public string Certificate { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Trainer { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string ExpiredDate { get; set; }
        [Required]
        public IFormFile CertificateAttachment { get; set; }
    }
}
