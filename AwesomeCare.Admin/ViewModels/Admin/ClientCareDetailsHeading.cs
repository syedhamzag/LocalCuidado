using AwesomeCare.Admin.Models;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsHeading;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Admin
{
    public class ClientCareDetailsHeading
    {
        public ClientCareDetailsHeading()
        {
            Headings = new List<GetClientCareDetailsHeading>();
        }
        [Required]
        [MaxLength(250,ErrorMessage ="Max. Character is 250")]
        public string Heading { get; set; }
        public bool Delete { get; set; }
        public int ClientCareDetailsHeadingId { get; set; }
        public ActionType ActionType { get; set; }
        public List<GetClientCareDetailsHeading> Headings { get; set; }
    }
}
