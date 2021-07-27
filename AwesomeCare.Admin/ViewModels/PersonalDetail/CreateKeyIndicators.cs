using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreateKeyIndicators
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int KeyId { get; set; }
        [Required]
        public string AboutMe { get; set; }
        [Required]
        public string FamilyRole { get; set; }
        [Required]
        public int LivingStatus { get; set; }
        [Required]
        public int Debture { get; set; }
        [Required]
        public string ThingsILike { get; set; }
        [Required]
        public int LogMethod { get; set; }
    }
}
