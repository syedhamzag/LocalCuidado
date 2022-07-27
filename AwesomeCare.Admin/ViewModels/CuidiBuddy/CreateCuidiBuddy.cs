using AwesomeCare.DataTransferObject.DTOs.Client;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AwesomeCare.Admin.ViewModels.CuidiBuddy
{
    public class CreateCuidiBuddy
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CuidiBuddyId { get; set; }

        public int Gender { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public int Location { get; set; }
        public List<SelectListItem> LocationList  { get; set; }
        public List<GetClient> getClients { get; set; } = new List<GetClient>();
    }
}
