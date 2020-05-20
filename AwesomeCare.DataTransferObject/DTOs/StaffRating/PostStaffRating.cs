using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRating
{
    public class PostStaffRating
    {
        [Required]
        [Display(Name = "Staff")]
        public int StaffPersonalInfoId { get; set; }
        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime? CommentDate { get; set; }
        public int Rating { get; set; }
        /// <summary>
        /// ApplicationUserId
        /// </summary>
        public int SubmittedBy { get; set; }
    }
}
