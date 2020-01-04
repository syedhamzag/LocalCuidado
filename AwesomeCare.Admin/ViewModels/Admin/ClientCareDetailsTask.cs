using AwesomeCare.Admin.Models;
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
        public int TaskId { get; set; }
        [Required]
        [MaxLength(250,ErrorMessage ="Max. Character is 250")]
        public string Task { get; set; }
        public bool Delete { get; set; }
        public int SelectedTaskId { get; set; }
        public ActionType ActionType { get; set; }
        public string Heading { get; set; }
        public List<GetClientCareDetailsTask> Tasks { get; set; }
    }
}
