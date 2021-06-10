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
        
        }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Attach { get; set; }
        #region DropDowns
        public ICollection<GetStaffs> OfficerToActList { get; set; }
        public ICollection<SelectListItem> StatusList { get; set; }
        #endregion
        public string ActiveTab { get; set; } = "program";
        public int ProgramId { get; set; }
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
        [MaxLength(255)]
        public string Observation { get; set; }
        [Required]
        [MaxLength(255)]
        public string ActionRequired { get; set; }
        [Required]
        public int OfficerToAct { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        [MaxLength(255)]
        public string Remarks { get; set; }
        [Required]
        [MaxLength(255)]
        public string URL { get; set; }
        public string Attachment { get; set; }
    }
}
