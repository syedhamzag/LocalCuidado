using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientCareDetailsTask
{
    public class GetClientCareDetailsTask
    {
        public int ClientCareDetailsTaskId { get; set; }
        public int ClientCareDetailsHeadingId { get; set; }
        public string Task { get; set; }
    }
}
