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
        [Required]
        public string Referee { get; set; }
        [Display(Name = "Company Name")]
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name ="Position of Referee")]
        [Required]
        public string PositionofReferee { get; set; }
        [Required]
        public IFormFile Attachment { get; set; }
    }
}
