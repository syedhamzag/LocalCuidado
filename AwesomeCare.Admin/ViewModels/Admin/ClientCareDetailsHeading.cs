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
        [Required]
        [MaxLength(250,ErrorMessage ="Max. Character is 250")]
        public string Heading { get; set; }
        public List<GetClientCareDetailsHeading> Headings { get; set; }
    }
}
