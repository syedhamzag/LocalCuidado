using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
    public class StaffPersonalInfoComment
    {
        public int StaffPersonalInfoCommentId { get; set; }
        public int StaffPersonalInfoId { get; set; }
        public int? CommentBy_UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CommentOn { get; set; }

        public virtual StaffPersonalInfo StaffPersonalInfo { get; set; }
    }
}
