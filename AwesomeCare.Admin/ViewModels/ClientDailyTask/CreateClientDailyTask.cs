using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.Admin.ViewModels.ClientDailyTask
{
    public class CreateClientDailyTask
    {
        [DataType(DataType.Upload)]
        public IFormFile Attach { get; set; }
        public int DailyTaskId { get; set; }
        public int ClientId { get; set; }
        public string DailyTaskName { get; set; }
        public DateTime Date { get; set; }
        public DateTime AmendmentDate { get; set; }
        public string Attachment { get; set; }
        public string ClientName { get; set; }
    }
}
