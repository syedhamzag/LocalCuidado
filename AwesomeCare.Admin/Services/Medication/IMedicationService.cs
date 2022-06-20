using AwesomeCare.DataTransferObject.DTOs.Medication;
using AwesomeCare.DataTransferObject.DTOs.MedicationManufacturer;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Services.Medication
{
   public interface IMedicationService
    {
        [Get("/Medication")]
        Task<List<GetMedication>> Get();

        [Post("/Medication")]
        Task<HttpResponseMessage> Post([Body]PostMedication model);

        [Get("/Medication/{id}")]
        Task<GetMedication> Get(int id);

        [Put("/Medication")]
        Task<GetMedication> Put([Body]PutMedication model);

        #region Manufacturer
        [Get("/Medication/Manufacturer")]
        Task<List<GetMedicationManufacturer>> GetManufacturers();

        [Post("/Medication/Manufacturer")]
        Task<HttpResponseMessage> PostManufacturer([Body]PostMedicationManufacturer model);

        [Get("/Medication/Manufacturer/{id}")]
        Task<GetMedicationManufacturer> GetManufacturer(int id);

        [Put("/Medication/Manufacturer")]
        Task<GetMedication> PutManufacturer([Body]PutMedicationManufacturer model);
        #endregion

        #region Tracker
        [Post("/Medication/Create")]
        Task<GetStaffMedTracker> Create([Body] PostStaffMedTracker model);

        [Get("/Medication/MedTracker/{startDate}/{stopDate}")]
        Task<List<MedTracker>> MedTracker(string startDate, string stopDate);
        #endregion
    }
}
