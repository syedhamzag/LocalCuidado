using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreateReview
    {
        [Required]
        public int PersonalDetailId { get; set; }
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public DateTime CP_PreDate { get; set; }
        [Required]
        public DateTime CP_ReviewDate { get; set; }
        [Required]
        public DateTime RA_PreDate { get; set; }
        [Required]
        public DateTime RA_ReviewDate { get; set; }
    }
}
