using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace AwesomeCare.Admin.ViewModels.Rotering
{
    public class ClientInvoice
    {
        public List<LiveTracker> Invoices { get; set; } = new List<LiveTracker>();
        public List<int> ClientIds { get; set; } = new List<int>();

        public List<SelectListItem> ClientList { get; set; } = new List<SelectListItem>();
        public List<GetClient> Clients { get; set; } = new List<GetClient>();
        public string startDate { get; set; }
        public string stopDate { get; set; }

        public decimal? TotalAmount { get; set; }
    }
}
