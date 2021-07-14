using Microsoft.AspNetCore.Mvc.Rendering;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreatePersonalDetail
    {
        public CreatePersonalDetail()
        {
            Capacity = new List<GetCapacity>();
            ConsentCare = new List<GetConsentCare>();
            ConsentData = new List<GetConsentData>();
            ConsentLandLine = new List<GetConsentLandLine>();
            Equipment = new List<GetEquipment>();
            KeyIndicators = new List<GetKeyIndicators>();
            Personal = new List<GetPersonal>();
            PersonCentred = new List<GetPersonCentred>();
            Review = new List<GetReview>();
            StaffList = new List<SelectListItem>();
        }

        public List<SelectListItem> StaffList { get; set; }
        public List<GetCapacity> Capacity { get; set; }
        public List<GetConsentCare> ConsentCare { get; set; }
        public List<GetConsentData> ConsentData { get; set; }
        public List<GetConsentLandLine> ConsentLandLine { get; set; }
        public List<GetEquipment> Equipment { get; set; }
        public List<GetKeyIndicators> KeyIndicators { get; set; }
        public List<GetPersonal> Personal { get; set; }
        public List<GetPersonCentred> PersonCentred { get; set; }
        public List<GetReview> Review { get; set; }

        public int PersonalDetailId { get; set; }
        public int ClientId { get; set; }
    }
}
