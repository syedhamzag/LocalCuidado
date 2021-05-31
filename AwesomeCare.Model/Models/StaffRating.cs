using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class StaffRating
    {
        public int StaffRatingId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int ClientId { get; set; }
        public string Comment { get; set; }
        public DateTime? CommentDate { get; set; }
        public int Rating { get; set; }
        /// <summary>
        /// ApplicationUserId
        /// </summary>
        public int SubmittedBy { get; set; }
        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
