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
    public class PutPersonalDetail
    {
        public PutPersonalDetail()
        {
            Capacity = new List<PutCapacity>();
            ConsentCare = new List<PutConsentCare>();
            ConsentData = new List<PutConsentData>();
            ConsentLandline = new List<PutConsentLandLine>();
            Equipment = new List<PutEquipment>();
            KeyIndicators = new List<PutKeyIndicators>();
            Personal = new List<PutPersonal>();
            PersonCentred = new List<PutPersonCentred>();
            Review = new List<PutReview>();
        }

        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }

        public List<PutCapacity> Capacity { get; set; }
        public List<PutConsentCare> ConsentCare { get; set; }
        public List<PutConsentData> ConsentData { get; set; }
        public List<PutConsentLandLine> ConsentLandline { get; set; }
        public List<PutEquipment> Equipment { get; set; }
        public List<PutKeyIndicators> KeyIndicators { get; set; }
        public List<PutPersonal> Personal { get; set; }
        public List<PutPersonCentred> PersonCentred { get; set; }
        public List<PutReview> Review { get; set; }
    }
}
