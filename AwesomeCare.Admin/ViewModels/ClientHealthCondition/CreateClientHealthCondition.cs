using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AwesomeCare.Admin.ViewModels.ClientHealthCondition
{
    public class CreateClientHealthCondition
    {
        public int CHCId { get; set; }
        public int HCId { get; set; }
        public int ClientId { get; set; }
        public string Title { get; set; } = "Create Client Health Condition";
        public string Action { get; set; } = "Save";
        public List<int> Health { get; set; } = new List<int>();
        public List<SelectListItem> HealthList { get; set; } = new List<SelectListItem>();

    }
}
