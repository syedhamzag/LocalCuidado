using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreatePersonal
    {
        [Required]
        public int PersonalId { get; set; }
        [Required]
        public int PersonalDetailId { get; set; }
        [Required]
        public int Smoking { get; set; }
        [Required]
        public int DNR { get; set; }
    }
}
