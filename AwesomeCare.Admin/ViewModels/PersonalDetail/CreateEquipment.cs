using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.PersonalDetail
{
    public class CreateEquipment
    {
        public CreateEquipment() 
        {
 
        }


        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
        public IFormFile Attach { get; set; }

        [Required]
        public int PersonalDetailId { get; set; }
        [Required]
        public int EquipmentId { get; set; }
        [Required]
        public int Name { get; set; }
        [Required]
        public int StaffId { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public int Location { get; set; }
        [Required]
        public DateTime ServiceDate { get; set; }
        [Required]
        public DateTime NextServiceDate { get; set; }
        [Required]
        public string Attachment { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int PersonToAct { get; set; }
    }
}
