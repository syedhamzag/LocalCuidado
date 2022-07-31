using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.CareReview
{
    public class PutCareReview
    {
        public int CareReviewId { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Action { get; set; }
        public int ClientId { get; set; }

    }
}
