using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreateConsentData
    {
        [Required]
        public int DataId { get; set; }
        [Required]
        public int PersonalDetailId { get; set; }
        [Required]
        public int Signature { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
