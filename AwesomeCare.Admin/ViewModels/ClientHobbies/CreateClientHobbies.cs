using AwesomeCare.DataTransferObject.DTOs.ClientHobbies;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AwesomeCare.Admin.ViewModels.ClientHobbies
{
    public class CreateClientHobbies
    {
        public int CHId { get; set; }
        public int HId { get; set; }
        public int ClientId { get; set; }

        public string Title { get; set; } = "Create Client Hobbies";
        public string Action { get; set; } = "Save";

        public List<int> Hobbies { get; set; } = new List<int>();
        public List<SelectListItem> HobbyList { get; set; } = new List<SelectListItem>();
    }
}
