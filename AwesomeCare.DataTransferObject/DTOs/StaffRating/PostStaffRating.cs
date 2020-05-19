using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffRating
{
    public class PostStaffRating
    {
       
        public int StaffPersonalInfoId { get; set; }
        public int ClientId { get; set; }
        public string Comment { get; set; }
        public DateTime? CommentDate { get; set; }
        public int Rating { get; set; }
        /// <summary>
        /// ApplicationUserId
        /// </summary>
        public int SubmittedBy { get; set; }
    }
}
