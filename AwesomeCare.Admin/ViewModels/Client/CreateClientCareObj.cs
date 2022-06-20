using AwesomeCare.DataTransferObject.DTOs.Client.CareObj;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class CreateClientCareObj
    {
        public int CareObjId { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }

        public string ClientName { get; set; }
        public string StatusName { get; set; }

        public List<int> PersonToAct { get; set; } = new List<int>();

        public List<SelectListItem> PersonToActList { get; set; } = new List<SelectListItem>();
    }
}
