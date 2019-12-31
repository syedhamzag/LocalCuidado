using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.RotaTask
{
   public class PutRotaTask:BaseDTO
    {
        [Required]
        public int RotaTaskId { get; set; }
        [Required]
        [MaxLength(125)]
        public string TaskName { get; set; }
        [Required]
        [MaxLength(50)]
        public string GivenAcronym { get; set; }
        [Required]
        [MaxLength(50)]
        public string NotGivenAcronym { get; set; }
        [Required]
        [MaxLength(125)]
        public string Remark { get; set; }
    }
}
