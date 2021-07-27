using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreateConsentCare
    {
        [Required]
        public int CareId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int Signature { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
