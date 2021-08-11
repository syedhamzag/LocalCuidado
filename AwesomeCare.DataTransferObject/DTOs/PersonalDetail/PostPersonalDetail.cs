using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail
{
    class PostPersonalDetail
    {
        public PostPersonalDetail()
        {
            Capacity = new List<PostCapacity>();
            ConsentCare = new List<PostConsentCare>();
            ConsentData = new List<PostConsentData>();
            ConsentLandline = new List<PostConsentLandLine>();
            Equipment = new List<PostEquipment>();
            KeyIndicators = new List<PostKeyIndicators>();
            Personal = new List<PostPersonal>();
            PersonCentred = new List<PostPersonCentred>();
            Review = new List<PostReview>();
        }

        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }

        public List<PostCapacity> Capacity { get; set; }
        public List<PostConsentCare> ConsentCare { get; set; }
        public List<PostConsentData> ConsentData { get; set; }
        public List<PostConsentLandLine> ConsentLandline { get; set; }
        public List<PostEquipment> Equipment { get; set; }
        public List<PostKeyIndicators> KeyIndicators { get; set; }
        public List<PostPersonal> Personal { get; set; }
        public List<PostPersonCentred> PersonCentred { get; set; }
        public List<PostReview> Review { get; set; }
    }
}
