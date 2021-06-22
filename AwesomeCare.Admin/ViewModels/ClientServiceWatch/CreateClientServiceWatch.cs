using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.ClientServiceWatch
{
    public class CreateClientServiceWatch
    {
        public CreateClientServiceWatch()
        {
            OfficerToActList = new List<SelectListItem>();
        }
        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        [AllowedExtensions(new string[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile Attach { get; set; }

        #region DropDowns
        public ICollection<SelectListItem> OfficerToActList { get; set; }
        public ICollection<SelectListItem> StatusList { get; set; }
        #endregion
        public string ClientName { get; set; }
        public string StatusName { get; set; }
        public string ActiveTab { get; set; } = "servicewatch";
        public int WatchId { get; set; }
        public List<int> WatchIds { get; set; }
        public string Reference { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime NextCheckDate { get; set; }
        [Required]
        public int Incident { get; set; }
        [Required]
        public int Details { get; set; }
        [Required]
        public int PersonInvolved { get; set; }
        [Required]
        public int Contact { get; set; }
        [Required]
        [MaxLength(255)]
        public string Observation { get; set; }
        [Required]
        [MaxLength(255)]
        public string ActionRequired { get; set; }
        [Required]
        [Display(Name = "OFFICERTOACT")]
        public List<int> OfficerToAct { get; set; }
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
