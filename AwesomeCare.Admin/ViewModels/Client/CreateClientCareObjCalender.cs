using AwesomeCare.DataTransferObject.DTOs.Client.CareObj;
using System.Collections.Generic;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateClientCareObjCalender
    {
        public string Year { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public List<GetClientCareObj> GetClientCareObj { get; set; } = new List<GetClientCareObj>();
    }
}
