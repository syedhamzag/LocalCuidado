using AwesomeCare.Admin.ViewModels.Client;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.Admin.ViewModels.Staff
{
    public class PreviewStaffRota 
    {
        public PreviewStaffRota()
        {
            RotaDays = new List<PreviewStaffRotaDate>();
            Staffs = new List<SelectListItem>();
            Selections = new List<SelectListItem>();
        }

        public List<PreviewStaffRotaDate> RotaDays { get; set; }
        public List<SelectListItem> Staffs { get; set; }
        public List<SelectListItem> Selections { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public bool IsMedication { get; set; }
    }

    public class PreviewStaffRotaDate
    {
        public PreviewStaffRotaDate()
        {
            SelectedStaffs = new List<int>();
            Rotas = new List<SelectListItem>();
            Items = new List<int>();
            RotaTypes = new List<CreateMedicationPeriod>();
           
        }
        public DateTime Date { get; set; }
       
        public List<SelectListItem> Rotas { get; set; }
       
        public List<CreateMedicationPeriod> RotaTypes { get; set; }
        public string Remark { get; set; }
        public int RotaId { get; set; }
        [Display(Name = "Select Staff")]
        public int Staff { get; set; }
        public List<int> SelectedStaffs { get; set; }
       public List<int> Items { get; set; }
    }
}
