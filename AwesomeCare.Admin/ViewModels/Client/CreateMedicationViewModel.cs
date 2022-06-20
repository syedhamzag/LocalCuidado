using AwesomeCare.DataTransferObject.DTOs.ClientMedication;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaType;
using AwesomeCare.DataTransferObject.DTOs.Medication;
using AwesomeCare.DataTransferObject.DTOs.RotaDayofWeek;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateMedicationViewModel : PostClientMedication
    {
        public CreateMedicationViewModel()
        {

            Days = new List<CreateMedicationDay>();
           

           // WeekDays = new List<GetRotaDayofWeek>();
            Medications = new List<SelectListItem>();
            MedicationManufacturers = new List<SelectListItem>();
            Gaps = new List<SelectListItem>();
           // RotaTypes = new List<GetClientRotaType>();
            for (int i = 1; i < 24; i++)
            {
                if (i == 1)
                    Gaps.Add(new SelectListItem($"{i} Hour", i.ToString()));
                else
                    Gaps.Add(new SelectListItem($"{i} Hours", i.ToString()));
            }
        }
        [DataType(DataType.Upload)]
        public IFormFile Evidence { get; set; }
        // public List<GetRotaDayofWeek> WeekDays { get; set; }
        public List<SelectListItem> Medications { get; set; }
        public List<SelectListItem> MedicationManufacturers { get; set; }
        //public List<GetClientRotaType> RotaTypes { get; set; }

        [Display(Name = "Gap (hours)")]
        public List<SelectListItem> Gaps { get; set; }

        public List<CreateMedicationDay> Days { get; set; }
       
    }
}
