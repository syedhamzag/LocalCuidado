using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCareDetails
{
   public class GetClientCareDetails
    {
        public int ClientCareDetailsId { get; set; }
        public int ClientCareDetailsTaskId { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public string Risk { get; set; }
        public string Mitigation { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
    }
}
