using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review
{
    public class PutReview
    {
        public int ClientId { get; set; }
        public int ReviewId { get; set; }
        public DateTime CP_PreDate { get; set; }
        public DateTime CP_ReviewDate { get; set; }
        public DateTime RA_PreDate { get; set; }
        public DateTime RA_ReviewDate { get; set; }
    }
}
