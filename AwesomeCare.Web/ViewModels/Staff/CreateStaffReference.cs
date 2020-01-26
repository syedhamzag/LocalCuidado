using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Web.ViewModels.Staff
{
    public class CreateStaffReference
    {
        [Display(Name = "Name of Referee")]
        public string Referee { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Display(Name ="Position of Referee")]
        public string PositionofReferee { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
