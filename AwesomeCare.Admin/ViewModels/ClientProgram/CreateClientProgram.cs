using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ClientProgram
{
    public class CreateClientProgram
    {
        public CreateClientProgram()
        {
                OfficerToActList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile Attach { get; set; }
        #region DropDowns
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> StatusList { get; set; }
        #endregion
        public string ActiveTab { get; set; } = "program";
        public int ProgramId { get; set; }
        public string Reference { get; set; }
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]
        public int ProgramOfChoice { get; set; }
        [Required]
        public int DaysOfChoice { get; set; }
        [Required]
        public int PlaceLocationProgram { get; set; }
        [Required]
        public int DetailsOfProgram { get; set; }
        [Required]
        public string Observation { get; set; }
        [Required]
        public string ActionRequired { get; set; }
        [Required]
        [Display(Name = "OFFICERTOACT")]
        public List<int> OfficerToTakeAction { get; set; }
        public List<string> OfficerName { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string Remarks { get; set; }
        [Required]
        public string URL { get; set; }
        public string Attachment { get; set; }
    }
}
