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
    public class PostPersonalDetail
    {
        public PostPersonalDetail()
        {
            Equipment = new List<PostEquipment>();
            PersonCentred = new List<PostPersonCentred>();
        }

        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }

        public PostCapacity Capacity { get; set; }
        public PostConsentCare ConsentCare { get; set; }
        public PostConsentData ConsentData { get; set; }
        public PostConsentLandLine ConsentLandline { get; set; }       
        public PostKeyIndicators KeyIndicators { get; set; }
        public PostPersonal Personal { get; set; }
        public List<PostPersonCentred> PersonCentred { get; set; }
        public PostReview Review { get; set; }
        public List<PostEquipment> Equipment { get; set; }
    }
}
