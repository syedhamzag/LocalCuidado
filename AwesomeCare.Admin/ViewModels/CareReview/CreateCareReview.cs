using System;

namespace AwesomeCare.Admin.ViewModels.CareReview
{
    public class CreateCareReview
    {
        public int CareReviewId { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Action { get; set; }
        public int ClientId { get; set; }

    }
}
