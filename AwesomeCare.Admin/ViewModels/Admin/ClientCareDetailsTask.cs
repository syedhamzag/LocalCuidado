using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsTask;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Admin
{
    public class ClientCareDetailsTask
    {
        public ClientCareDetailsTask()
        {
            Tasks = new List<GetClientCareDetailsTask>();
        }
        public int HeaderId { get; set; }
        [Required]
        [MaxLength(250,ErrorMessage ="Max. Character is 250")]
        public string Task { get; set; }
        public string Heading { get; set; }
        public List<GetClientCareDetailsTask> Tasks { get; set; }
    }
}
