using AwesomeCare.DataTransferObject.DTOs.ClientRotaTask;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientRotaDays
{
   public class CreateClientRotaDays
    {
        public CreateClientRotaDays()
        {
            RotaTasks = new List<CreateClientRotaTask>();
        }
        public int ClientRotaDaysId { get; set; }
        [Required(ErrorMessage = "please provide  ClientRota")]
        public int ClientRotaId { get; set; }
        [Required(ErrorMessage = "Please provide Day of Week")]
        public int RotaDayofWeekId { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string StopTime { get; set; }
        [Required]
        public int RotaId { get; set; }
        public string WeekDay { get; set; }
        public int? ClientId { get; set; }
        public int? ClientRotaTypeId { get; set; }
        public List<CreateClientRotaTask> RotaTasks { get; set; }
    }
}
